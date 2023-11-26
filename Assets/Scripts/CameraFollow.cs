using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField] private Transform target;
    private Vector3 vel = Vector3.zero;
    private float damping;

    [SerializeField] private Vector2 leftBottom;
    [SerializeField] private Vector2 rightTop;
    
    private void Update()
    {
        var newPos = new Vector3(Math.Min(Math.Max(target.transform.position.x,leftBottom.x),rightTop.x),
            Math.Min(Math.Max(target.transform.position.y, leftBottom.y), rightTop.y),-10);
        this.transform.position = Vector3.SmoothDamp(this.transform.position,
            newPos, ref vel, damping);
    }
}
