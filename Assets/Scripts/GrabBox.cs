using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class GrabBox : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDist;
    [SerializeField] private BoxCollider2D handCollider2D;

    private CharMovement charMovement;
    private int boxLayer;
    private GameObject box;
    private Animator animator;
    private GravityVector gravityVector;

    // Start is called before the first frame update
    void Start()
    {
        boxLayer = LayerMask.NameToLayer("Box");
        charMovement = GetComponent<CharMovement>();
        animator = charMovement.GetComponent<Animator>();
        gravityVector = GetComponent<CharMovement>().gravityVectorType;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("HasBox", handCollider2D.enabled);
        RaycastHit2D hitInfo =
            Physics2D.Raycast(rayPoint.position, transform.right * charMovement.GetDirection(), rayDist);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == boxLayer)
        {
            //Debug.Log("HIT SOMETHING: " + hitInfo.collider.gameObject.name.ToString());
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("PRESSED E");
                if (box == null)
                {
                    box = hitInfo.collider.gameObject;
                    var box_rbody = box.GetComponent<Rigidbody2D>();
                    if (box_rbody == null)
                    {
                        box = null;
                        return;
                    }
                    //Debug.Log(charMovement.name + " grabbed box");
                    box_rbody.isKinematic = true;
                    box.GetComponent<BoxCollider2D>().enabled = false;
                    box.layer = 0;
                    handCollider2D.enabled = true;
                    box.transform.position = grabPoint.position;
                    box.transform.SetParent(transform);
                    var box_gravitycontroller = box.GetComponent<BoxGravityController>();
                    box_gravitycontroller.RotateBox(gravityVector);
                    box_gravitycontroller.ChangeGravityVector(gravityVector);
                }
                else
                {
                    //Debug.Log(charMovement.name + "released box");
                    box.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                    
                    handCollider2D.enabled = false;
                    box.layer = boxLayer;
                    box.GetComponent<BoxCollider2D>().enabled = true;
                    box.GetComponent<Rigidbody2D>().isKinematic = false;
                    box.transform.SetParent(null);
                    box = null;
                }
            }
        }

        Debug.DrawRay(rayPoint.position, transform.right * charMovement.GetDirection() * rayDist);
    }
}