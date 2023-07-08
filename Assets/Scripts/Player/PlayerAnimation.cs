using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.MoveDirection.x != 0|| playerMovement.MoveDirection.y != 0)
        {
            animator.SetBool("Moving", true);

            SpriteDirectionChecker();
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    void SpriteDirectionChecker()
    {
        if(playerMovement.LastHorizontalVector > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
