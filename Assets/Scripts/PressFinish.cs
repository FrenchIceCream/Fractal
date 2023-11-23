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
    [SerializeField] private GameObject nextLevelUI;
    [SerializeField] private float duration;

    void Start()
    {
        parent = transform.parent.gameObject;
        startPos = parent.transform.position;
        targetPos = parent.transform.position - new Vector3(0, 0.5f, 0);
    }

    void Update()
    {
        if (isOnPlatform == true)
        {
            if (isBox)
            {
                timer += Time.deltaTime;
                if (timer >= duration) 
                {
                    nextLevelUI.SetActive(true);
                    Time.timeScale = 0;
                    timer = float.MinValue;
                    //Debug.Log("DONE");
                }
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Box")  || collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Box") )
                isBox = true;
            isOnPlatform = true;
            //Debug.Log("ENTER");
        }
    }
 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Box")  || collision.gameObject.CompareTag("Player"))
        {
            isOnPlatform = false;
            isBox = false;
            timer = 0;
            //Debug.Log("EXIT");
        }
    }
}
