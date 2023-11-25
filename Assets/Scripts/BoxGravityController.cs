using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGravityController : MonoBehaviour
{
    // Start is called before the first frame update
    private Quaternion originRotation;
    private ConstantForce2D forse;
    private Rigidbody2D rbody;
    [SerializeField] private GravityVector gravityVector;
    private float G = 9.81f;

    void Start()
    {
        originRotation = transform.rotation;
        forse = GetComponent<ConstantForce2D>();
        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 0;
        RotateBox(gravityVector);
    }

    public void RotateBox(GravityVector gravityVector)
    {
        switch (gravityVector)
        {
            case GravityVector.Down:
                RotateDown();
                break;
            case GravityVector.Up:
                RotateUp();
                break;
            case GravityVector.Left:
                RotateLeft();
                break;
            case GravityVector.Right:
                RotateRight();
                break;
        }
    }

    private void RotateDown()
    {
        transform.rotation = originRotation;
        forse.force = Vector2.down * G * rbody.mass * 2;
    }

    private void RotateLeft()
    {
        transform.rotation = originRotation;
        transform.Rotate(Vector3.forward, -90f);
        forse.force = Vector2.left * G * rbody.mass * 2;
    }

    private void RotateRight()
    {
        transform.rotation = originRotation;
        transform.Rotate(Vector3.forward, 90f);
        forse.force = Vector2.right * G * rbody.mass * 2;
    }

    private void RotateUp()
    {
        transform.rotation = originRotation;
        transform.Rotate(Vector3.forward, 180f);
        forse.force = Vector2.up * G * rbody.mass * 2;
    }
}