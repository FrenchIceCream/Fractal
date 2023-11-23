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

    // Start is called before the first frame update
    void Start()
    {
        boxLayer = LayerMask.NameToLayer("Box");
        charMovement = GetComponent<CharMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right * charMovement.GetDirection(), rayDist);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == boxLayer)
        {
            //Debug.Log("HIT SOMETHING: " + hitInfo.collider.gameObject.layer.ToString());
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("PRESSED E");
                if (box == null)
                {
                    box = hitInfo.collider.gameObject;
                    box.GetComponent<Rigidbody2D>().isKinematic = true;
                    box.GetComponent<BoxCollider2D>().enabled = false;
                    handCollider2D.enabled = true;
                    box.transform.position = grabPoint.position;
                    box.transform.rotation = new Quaternion(0,0,0,0);
                    box.transform.SetParent(transform);
                }
                else 
                {
                    box.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                    handCollider2D.enabled = false;
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
