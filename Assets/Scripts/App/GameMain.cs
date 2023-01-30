using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField] GameState _gameState;
    GameEvent _gameEvent;

    List<BaseSystem> _systemList;

    List<IPreUpdateSystem> _preUpdateSystemList;
    List<IOnUpdateSystem> _onUpdateSystemList;
    List<IOnCollisionSystem> _onCollisionSystemList;

    private void Awake()
    {
        _systemList = new List<BaseSystem>(){
            new GameRule(),
            new InputSystem(),
            new CoroutineSystem(),
            new RotationSystem(),
            new UISystem()
        };

        _gameEvent = new GameEvent();
        _preUpdateSystemList = new List<IPreUpdateSystem>();
        _onUpdateSystemList = new List<IOnUpdateSystem>();
        _onCollisionSystemList = new List<IOnCollisionSystem>();

        foreach(BaseSystem system in _systemList)
        {
            system.Init(_gameState, _gameEvent);
            system.SetEvent();
            if (system is IPreUpdateSystem) _preUpdateSystemList.Add(system as IPreUpdateSystem);
            if (system is IOnUpdateSystem) _onUpdateSystemList.Add(system as IOnUpdateSystem);
            if (system is IOnCollisionSystem) _onCollisionSystemList.Add(system as IOnCollisionSystem);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        foreach(var system in _preUpdateSystemList) system.PreUpdate();
        foreach(var system in _onCollisionSystemList) system.OnCollision();
        foreach(var system in _onUpdateSystemList) system.OnUpdate();
    }
}
