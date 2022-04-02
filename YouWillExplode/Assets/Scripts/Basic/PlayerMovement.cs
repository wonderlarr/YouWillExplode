using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private Vector2 moveDir;

    // Public or Serialized vars show up in the inspector window in the editor, private vars do not.
    public float walkSpeed = 1f;

    // Awake is called after object construction
    private void Awake() 
    {
        // Rigidbody2D of THIS instance of the prefab
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get axis input
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        //Debug.Log(moveDir);


        if (moveDir.sqrMagnitude > 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        

        // Flip sprite when walking in different directions
        if (moveDir.x > 0)
        {
            sprite.flipX = false;
        }
        else if (moveDir.x < 0)
        {
            sprite.flipX = true;
        }

    }

    // Fixed update is called once per PHYSICS frame, fixed to 50 times a second (0.02) by default
    private void FixedUpdate()
    {
        // Move player, better method
        rb.MovePosition(rb.position + moveDir * walkSpeed * Time.fixedDeltaTime);
    }
}
