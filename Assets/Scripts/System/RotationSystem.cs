using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSystem : BaseSystem,IOnUpdateSystem
{
    Dictionary<string, RotationPreset> preset;
    RotationPreset[] queue;
    bool hasQueue = false;
    bool isMoving = false;
    int index;

    public override void SetEvent()
    {
        base.SetEvent();
        _gameState.rotation = new RotationElement(_gameState.center, _gameState.rotationTime);
        _gameState.rotation.finMove += FinMove;

        _gameEvent.startRotation += StartRotation;
        SetupPreset();
        
    }
    private void SetupPreset()
    {
        preset = new Dictionary<string, RotationPreset>();
        RotationPreset tmp = new RotationPreset(Vector3.forward, -90);
        preset.Add("F", tmp);
        tmp = new RotationPreset(Vector3.forward, 90);
        preset.Add("F'", tmp);
        tmp = new RotationPreset(Vector3.back, -90);
        preset.Add("B", tmp);
        tmp = new RotationPreset(Vector3.back, 90);
        preset.Add("B'", tmp);
        tmp = new RotationPreset(Vector3.left, -90);
        preset.Add("R", tmp);
        tmp = new RotationPreset(Vector3.left, 90);
        preset.Add("R'", tmp);
        tmp = new RotationPreset(Vector3.right, -90);
        preset.Add("L", tmp);
        tmp = new RotationPreset(Vector3.right, 90);
        preset.Add("L'", tmp);
        tmp = new RotationPreset(Vector3.down, -90);
        preset.Add("U", tmp);
        tmp = new RotationPreset(Vector3.down, 90);
        preset.Add("U'", tmp);
        tmp = new RotationPreset(Vector3.up, -90);
        preset.Add("D", tmp);
        tmp = new RotationPreset(Vector3.up, 90);
        preset.Add("D'", tmp);
    }
    
    public void OnUpdate()
    {
        OnMove();
        QueueUpdate();
    }

    private void OnMove()
    {
        _gameState.rotation.OnUpdate();
    }

    private void StartRotation(string text)
    {
        if (hasQueue) return;
        string[] operation = text.Split(' ');
        List<RotationPreset> list = new List<RotationPreset>();
        foreach(string input in operation)
        {
            if(input.Length == 0)
            {
                if(preset.ContainsKey(input))
                {
                    list.Add(preset[input]);
                }
                continue;
            }

            string key;
            string value = string.Empty;
            if(input.Contains('\''))
            {
                key = input.Substring(0, 2);
                if (input.Length >= 3) {
                    value = input.Substring(2, input.Length-2);
                }
            }
            else
            {
                key = input.Substring(0, 1);
                if (input.Length >= 2)
                {
                    value = input.Substring(1, input.Length-1);
                }
            }
            if (preset.ContainsKey(key) == false) continue;
            int number = 1;
            if (value != string.Empty)
            {
                int.TryParse(value, out number);
            }
            
            for(int i=0;i<number;i++)
            {
                list.Add(preset[key]);
            }
        }
        queue = list.ToArray();
        hasQueue = true;
        index = -1;
    }

    private void QueueUpdate()
    {
        if (hasQueue == false) return;
        if (isMoving) return;
        index++;
        if (index >= queue.Length)
        {
            queue = null;
            hasQueue = false;
            return;
        }

        SetupCenter(queue[index]);
        _gameState.rotation.StartRotation(queue[index].axis, queue[index].angle);
        isMoving = true;
    }

    private void SetupCenter(RotationPreset preset)
    {
        Debug.Log(preset.axis);
        _gameState.center.DetachChildren();
        _gameState.center.transform.rotation = Quaternion.identity;
        Vector3 start = preset.axis * -3;
        if(preset.axis.x!=0)
        {
            for(int i=-1;i<=1;i++)
            {
                for(int j=-1;j<=1;j++)
                {
                    SetChild(start + new Vector3(0,i,j), preset.axis);
                }
            }
            return;
        }

        if (preset.axis.y != 0)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    SetChild(start + new Vector3(i, 0, j), preset.axis);
                }
            }
            return;
        }

        if (preset.axis.z != 0)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    SetChild(start + new Vector3(i, j, 0), preset.axis);
                }
            }
            return;
        }
    }

    private void SetChild(Vector3 start,Vector3 direction)
    {
        RaycastHit hit;
        if(Physics.Raycast(start, direction, out hit))
        {
            hit.collider.gameObject.transform.parent = _gameState.center;
        }
        //Debug.DrawLine(start, start + direction,);
    }

    private void FinMove()
    {
        _gameEvent.finMove?.Invoke();
        isMoving = false;
    }
}
