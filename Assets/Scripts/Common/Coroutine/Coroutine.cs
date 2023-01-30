using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coroutine
{
    public Stack<object> stack = new Stack<object>();
    public Exception Error { get; set; }
    public Coroutine(IEnumerator routine)
    {
        stack.Push(routine);
    }
}
