using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField] private Transform target;
    private Vector3 vel = Vector3.zero;
    private float damping;
    
    private void FixedUpdate()
    {



        this.transform.position = Vector3.SmoothDamp(this.transform.position,
            new Vector3(target.transform.position.x, target.transform.position.y, -10), ref vel, damping);
    }
}
