using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class PressGravity : MonoBehaviour
{
    private bool isOnPlatform;
    private GameObject parent;
    private Vector3 startPos;
    private Vector3 targetPos;

    [SerializeField] private List<BoxGravityController> BoxesList;
    private float speed = 10f;
    [SerializeField] public GravityVector gravityVectorType;


    void Start()
    {
        //isGravityOn = true;
        parent = transform.parent.gameObject;
        startPos = parent.transform.position;
        targetPos = parent.transform.position + TargetPos();
    }

    void Update()
    {
        if (isOnPlatform)
        {
            MoveDown();
        }
        else
            MoveUp();
    }

    private void MoveDown()
    {
        parent.transform.position = Vector2.MoveTowards(parent.transform.position, targetPos, speed * Time.deltaTime);
    }

    private void MoveUp()
    {
        parent.transform.position = Vector2.MoveTowards(parent.transform.position, startPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Box") || collision.gameObject.CompareTag("Player"))
        {
            ChangeGravity();
            isOnPlatform = true;
            //Debug.Log("ENTER");
        }
    }

        private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Box") || collision.gameObject.CompareTag("Player"))
        {
            isOnPlatform = false;
            //Debug.Log("EXIT");
        }
    }


    private void ChangeGravity()
    {
        foreach (BoxGravityController box in BoxesList)
        {
            box.transform.Rotate(Vector3.forward, 180f);
            box.SetForce(box.GetOppositeVector2() * 9.81f * box.GetComponent<Rigidbody2D>().mass * 2);
            box.ChangeGravityVector(box.GetOppositeGravityVector());
        }
    }

    private Vector3 TargetPos()
    {
        switch (gravityVectorType)
        {
            case GravityVector.Down:
                return Vector3.down * 0.5f;
            case GravityVector.Up:
                return Vector3.up * 0.5f;
            case GravityVector.Left:
                return Vector3.left * 0.5f;
            case GravityVector.Right:
                return Vector3.right * 0.5f;
        }

        return Vector3.zero;
    }
}