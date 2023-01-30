using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPreset
{
    public Vector3 axis;
    public float angle;

    public RotationPreset(Vector3 axis = default,float angle = 90)
    {
        this.axis = axis;
        this.angle = angle;
    }
}

public class RotationElement
{
    Transform center;
    float moveTime = 1.5f;
    public event Action finMove;
    private float timer;
    private bool isMove;
    private Vector3 axis;
    private float moveAngle;
    private Quaternion startAngle;
    
    // Start is called before the first frame update
    public RotationElement(Transform center,float moveTime)
    {
        isMove = false;
        this.center = center;
        this.moveTime = moveTime;
    }

    // Update is called once per frame
    public void OnUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!isMove) return;
        timer += Time.deltaTime;
        center.rotation = GetQuaternion();
        if (timer < moveTime) return;
        isMove = false;
        finMove?.Invoke();
    }

    public void StartRotation(Vector3 axis,float angle)
    {
        if (isMove) return;
        this.axis = axis;
        this.moveAngle =  (angle / 360f) * 2 * Mathf.PI;//angle;
        isMove = true;
        timer = 0;
        startAngle = center.rotation;
    }

    private Quaternion GetQuaternion()
    {
        float angle = GetAngle(moveAngle, timer / moveTime)/ 2.0f;
        float sin = Mathf.Sin(angle);
        Quaternion q =new Quaternion( axis.x * sin, axis.y * sin, axis.z * sin, Mathf.Cos(angle));// Quaternion.AngleAxis(angle, axis);//
        return q;
    }

    private float GetAngle(float angle,float value)
    {
        return Mathf.Lerp(0, angle, value);
    }
}
