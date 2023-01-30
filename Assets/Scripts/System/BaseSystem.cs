using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSystem
{
    protected GameState _gameState;
    protected GameEvent _gameEvent;

    public void Init(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;
    }

    public virtual void SetEvent() { }
}
