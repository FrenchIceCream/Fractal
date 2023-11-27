using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_ButtonTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    //[SerializeField] private GameObject objectToTrigger;
    void OnTriggerEnter2D(Collider2D col)
    {
        //objectToTrigger.GetComponent<SpriteRenderer>().SetMaterials();
        animator.SetTrigger("ButtonTrigger");
    }
}
