using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class CloneTrigger : MonoBehaviour
{
    [SerializeField] private GravityVector outputGravity;
    [SerializeField] private GameObject clone;
    //Куда выходят копии объектов
    [SerializeField] private GameObject leadsTo;
    private GravityVector gravityOnAnotherEnd;
    private int boxLayer;

    void Start()
    {
        boxLayer = LayerMask.NameToLayer("Box");
        gravityOnAnotherEnd = leadsTo.GetComponent<CloneTrigger>().outputGravity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Entered trigger");
        if (other.gameObject.layer == boxLayer && other.gameObject.transform.parent == null && other.GetComponent<Rigidbody2D>() != null)
        {
            var newobject = other.gameObject;
            newobject.GetComponent<Rigidbody2D>().velocity = UnityEngine.Vector2.zero;
            newobject.transform.position = leadsTo.transform.position - 8f * leadsTo.transform.right;
            var boxgr = newobject.GetComponent<BoxGravityController>();
            boxgr.RotateBox(gravityOnAnotherEnd);
        }
    }
}
