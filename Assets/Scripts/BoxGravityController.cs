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

    
    void Awake()
    {
        originRotation = transform.rotation;
        forse = GetComponent<ConstantForce2D>();
        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 0;
        RotateBox(gravityVector);
    }

    public void RotateBox(GravityVector gravityVector)
    {
        this.gravityVector = gravityVector;
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

    public void SetForce(Vector2 f)
    {
        forse.force = f;
    }

    public GravityVector GetOppositeGravityVector()
    {
        switch (gravityVector)
        {
            case GravityVector.Down:
                return GravityVector.Up;
            case GravityVector.Up:
                return GravityVector.Down;
            case GravityVector.Left:    
                return  GravityVector.Right;
            case GravityVector.Right:
                return GravityVector.Left;
        }
        return GravityVector.Up;
    }

    public Vector2 GetOppositeVector2()
    {
        switch (gravityVector)
        {
            case GravityVector.Down:
                return Vector2.up;
            case GravityVector.Up:
                return Vector2.down;
            case GravityVector.Left:    
                return  Vector2.right;
            case GravityVector.Right:
                return Vector2.left;
        }
        return Vector2.zero;
    }

    public void ChangeGravityVector(GravityVector gv)
    {
        gravityVector = gv;
    }
}