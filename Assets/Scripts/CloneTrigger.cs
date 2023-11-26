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
        Debug.Log("Entered trigger");
        if (other.gameObject.layer == boxLayer && other.gameObject.transform.parent == null && other.GetComponent<Rigidbody2D>() != null)
        {
            var newobject = other.gameObject;
            newobject.transform.position = leadsTo.transform.position - 8f * leadsTo.transform.right;
           // newobject.transform.rotation = new UnityEngine.Quaternion(0,0,0,0);
            var boxgr = newobject.GetComponent<BoxGravityController>();
            boxgr.RotateBox(gravityOnAnotherEnd);
            /*ChangeConstantForce(newobject, boxgr.GetComponent<ConstantForce2D>(), gravityOnAnotherEnd, boxgr.GetComponent<Rigidbody2D>());
            boxgr.ChangeGravityVector(gravityOnAnotherEnd);*/
        }
    }

    void ChangeConstantForce(GameObject newobject,ConstantForce2D constantForce2D, GravityVector gravityVector, Rigidbody2D rbody)
    {
        switch (gravityVector)
        {
            case GravityVector.Down:
                constantForce2D.force = UnityEngine.Vector2.down * 9.81f * rbody.mass * 2;
                break;

            case GravityVector.Left:
                newobject.transform.Rotate(UnityEngine.Vector3.forward, -90f);
                constantForce2D.force = UnityEngine.Vector2.left * 9.81f * rbody.mass * 2;
                break;

            case GravityVector.Right:
                newobject.transform.Rotate(UnityEngine.Vector3.forward, 90f);
                constantForce2D.force = UnityEngine.Vector2.right * 9.81f * rbody.mass * 2;
                break;

            case GravityVector.Up:
                newobject.transform.Rotate(UnityEngine.Vector3.forward, 180f);
                constantForce2D.force = UnityEngine.Vector2.up * 9.81f * rbody.mass * 2;
                break;
        }
    }
}
