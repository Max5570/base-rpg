using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    [Space(10)]
    public Rigidbody2D rb;
    public Animator animator;

    private Player _player;
    private Vector2 movement;

    private void Start() 
    {
        _player = GetComponent<Player>();
    }

    private void Update() 
    {
        //if player is stunned he can't move
        if (_player.status == Player.Status.stunned)
        {
            return;
        }
        //get move buttons
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        //inform animator the move direction
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) )
            animator.SetFloat("LookDirection", 0);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) )
            animator.SetFloat("LookDirection", 90);

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) )
            animator.SetFloat("LookDirection", 180);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
            animator.SetFloat("LookDirection", 270);

    }

    private void FixedUpdate() 
    {
        //if player is stunned he can't move
        if (_player.status == Player.Status.stunned)
        {
            animator.SetFloat("Speed", 0);
            return;
        }
        //clamp diagonal movement
        if (movement.sqrMagnitude > 1)
            movement = movement.normalized;
        //move player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        //set speed value to 0 or 1. If the speed is greater than 0 play move animation
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    
}
