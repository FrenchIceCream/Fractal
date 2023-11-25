using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PressFinish : MonoBehaviour
{
    private bool isOnPlatform;
    private bool isBox;
    private GameObject parent;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float timer = 0f;
    private float speed = 10f;
    public bool State;
    [SerializeField] private float duration;
    [SerializeField] public GravityVector gravityVectorType;


    void Start()
    {
        parent = transform.parent.gameObject;
        startPos = parent.transform.position;
        targetPos = parent.transform.position + TargetPos();
        State = false;
    }

    void Update()
    {
        if (isOnPlatform)
        {
            if (isBox)
            {
                if (timer >= duration)
                {
                    State = true;
                    /*nextLevelUI.SetActive(true);
                    Time.timeScale = 0;
                    timer = float.MinValue;*/
                    //Debug.Log("DONE");
                }
                else
                {
                    State = false;
                    timer += Time.deltaTime;
                }
            }
            else
            {
                timer = float.MinValue;
            }

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
            if (collision.gameObject.layer == LayerMask.NameToLayer("Box"))
                isBox = true;
            isOnPlatform = true;
            //Debug.Log("ENTER");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Box") || collision.gameObject.CompareTag("Player"))
        {
            isOnPlatform = false;
            isBox = false;
            timer = 0;
            State = false;
            //Debug.Log("EXIT");
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