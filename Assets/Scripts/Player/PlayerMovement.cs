using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    Rigidbody2D rigidbody;
    public Vector2 MoveDirection { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
     }

    void Move()
    {
        rigidbody.velocity = new Vector2(MoveDirection.x * movementSpeed, MoveDirection.y * movementSpeed);
    }
}
