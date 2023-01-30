using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoroutineSystem : BaseSystem,IOnUpdateSystem
{
    public override void SetEvent()
    {
        _gameState.coroutineList = new List<Coroutine>();
    }

    public void OnUpdate()
    {
        for (int i = _gameState.coroutineList.Count - 1; i >= 0; i--)
        {
            UpdateCoroutine(_gameState.coroutineList[i]);
        }
    }

    void UpdateCoroutine(Coroutine coroutine)
    {

        if (coroutine.stack.Count == 0)
        {
            _gameState.coroutineList.Remove(coroutine);
            return;
        }

        var peek = coroutine.stack.Peek();
        if (peek == null)
        {
            coroutine.stack.Pop();
        }
        else if (peek is IEnumerator)
        {
            var e = (IEnumerator)peek;
            try
            {
                if (e.MoveNext())
                {
                    coroutine.stack.Push(e.Current);
                    UpdateCoroutine(coroutine);
                }
                else
                {
                    coroutine.stack.Pop();
                    UpdateCoroutine(coroutine);
                }
            }
            catch (Exception error)
            {

            }
        }
    }
}
