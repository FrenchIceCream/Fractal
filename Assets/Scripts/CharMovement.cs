using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 20f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform groundCheck2;
    [SerializeField] private float jumpingPower;

    private LayerMask groundLayer;

    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
            rbody.velocity = new Vector2(rbody.velocity.x, jumpingPower);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && rbody.velocity.y > 0f)
            rbody.velocity = new Vector2(rbody.velocity.x, rbody.velocity.y * 0.5f);

        ChangeDir();
    }

    private void FixedUpdate()
    {
        rbody.velocity = new Vector2(horizontal * speed, rbody.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapArea(new Vector2(groundCheck.position.x, groundCheck.position.y),
            new Vector2(groundCheck2.position.x, groundCheck2.position.y));
    }

    private bool IsStanding()
    {
        //в теории возможен двойной прыжок, но если ты сможешь это обузить, то ты это заслужил
        return rbody.velocity.y == 0f;
    }

    private void ChangeDir()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public int GetDirection()
    {
        if (isFacingRight)
            return 1;
        return -1;
    }
}