using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 20f;
    private bool isFacingRight = true;
    private Animator animator;

    private Vector2 jumpVector;

    private readonly float G = 9.81f;


    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform groundCheck2;
    [SerializeField] private float jumpingPower;
    [SerializeField] public GravityVector gravityVectorType;


    void Start()
    {
        RotateModel();
        //Debug.Log(rbody.totalTorque);
        jumpVector = GetJumpVector();
        rbody.gravityScale = 0;
        GetComponent<ConstantForce2D>().force = GetGravity();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded())
            rbody.velocity = jumpVector;

        ChangeDir();
    }

    private void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        animator.SetBool("IsJumping", !IsGrounded());
    }

    private void FixedUpdate()
    {
        rbody.velocity = GetFixedUpdate();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapArea(new Vector2(groundCheck.position.x, groundCheck.position.y),
            new Vector2(groundCheck2.position.x, groundCheck2.position.y));
    }

    private Vector2 GetGravity()
    {
        switch (gravityVectorType)
        {
            case GravityVector.Down:
                return Vector2.down * G * rbody.mass * 3;
            case GravityVector.Up:
                return Vector2.up * G * rbody.mass * 3;
            case GravityVector.Left:
                return Vector2.left * G * rbody.mass * 3;
            case GravityVector.Right:
                return Vector2.right * G * rbody.mass * 3;
        }

        return Vector2.zero;
    }

    private Vector2 GetJumpVector()
    {
        switch (gravityVectorType)
        {
            case GravityVector.Down:
                return Vector2.up * jumpingPower;
            case GravityVector.Up:
                return Vector2.down * jumpingPower;
            case GravityVector.Left:
                return Vector2.right * jumpingPower;
            case GravityVector.Right:
                return Vector2.left * jumpingPower;
        }

        return Vector2.zero;
    }

    private Vector2 GetFixedUpdate()
    {
        switch (gravityVectorType)
        {
            case GravityVector.Down:
                return new Vector2(horizontal * speed, rbody.velocity.y);
            case GravityVector.Up:
                return new Vector2(-horizontal * speed, rbody.velocity.y);
            case GravityVector.Left:
                return new Vector2(rbody.velocity.x, -horizontal * speed);
            case GravityVector.Right:
                return new Vector2(rbody.velocity.x, horizontal * speed);
        }

        return Vector2.zero;
    }

    private void RotateModel()
    {
        switch (gravityVectorType)
        {
            case GravityVector.Down:
                return;
            case GravityVector.Up:
                transform.Rotate(Vector3.forward, 180f);
                return;
            case GravityVector.Left:
                transform.Rotate(Vector3.forward, -90f);
                return;
            case GravityVector.Right:
                transform.Rotate(Vector3.forward, 90f);
                return;
        }
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

public enum GravityVector
{
    Down,
    Up,
    Left,
    Right
}