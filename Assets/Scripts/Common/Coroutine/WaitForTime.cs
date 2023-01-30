using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForTime : IEnumerator
{
    float _time;
    float _timer;

    //public IEnumerator current;
    public WaitForTime(float time)
    {
        _time = time;
    }

    public bool MoveNext()
    {
        _timer += Time.deltaTime;
        return _time>_timer;
    }

    public void Reset()
    {

    }

    object IEnumerator.Current
    {
        get
        {
            return null;
        }
    }
}
