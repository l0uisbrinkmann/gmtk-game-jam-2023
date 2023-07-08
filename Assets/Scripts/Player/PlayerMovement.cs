using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    Rigidbody2D rigidbody;
    [HideInInspector]
    public Vector2 MoveDirection { get; set; }
    [HideInInspector]
    public Vector2 LastMoveDirection { get; set; }
    [HideInInspector]
    public float LastHorizontalVector { get; set; }
    [HideInInspector]
    public float LastVerticalVector { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        LastMoveDirection = new Vector2(1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        MoveDirection = new Vector2(moveX, moveY).normalized;

        if(MoveDirection.x != 0)
        {
            LastHorizontalVector = MoveDirection.x;
            LastMoveDirection = new Vector2(LastHorizontalVector, 0f);
        }
        if(MoveDirection.y != 0)
        {
            LastVerticalVector = MoveDirection.y;
            LastMoveDirection = new Vector2(0f, LastVerticalVector);
        }
        if(MoveDirection.x != 0 && MoveDirection.y != 0)
        {
            LastMoveDirection = new Vector2(LastHorizontalVector, LastVerticalVector);
        }
     }

    void Move()
    {
        rigidbody.velocity = new Vector2(MoveDirection.x * movementSpeed, MoveDirection.y * movementSpeed);
    }
}
