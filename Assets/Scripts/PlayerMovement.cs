using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;

    public float movementSpeed = 10f;
    public float jumpForce = 400f;
    public float maxVelocityX = 4f;

    public AudioClip soundEffect;

    private bool grounded;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void FixedUpdate() {
        var force = new Vector2(0f, 0f);

        float moveHorizontal = Input.GetAxis("Horizontal");

        var absVelocityX = Mathf.Abs(rigidBody.velocity.x);
        var absVelocityY = Mathf.Abs(rigidBody.velocity.y);

        if (absVelocityY == 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (absVelocityX < maxVelocityX)
        {
            force.x = (movementSpeed * moveHorizontal);
        }

        if (grounded == true && Input.GetButton("Jump"))
        {
            if (soundEffect)
            {
                AudioSource.PlayClipAtPoint(soundEffect, transform.position);
            }

            grounded = false;
            force.y = jumpForce;
            animator.SetInteger("AnimState", 2);
        }

        rigidBody.AddForce(force);

        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (grounded)
            {
                animator.SetInteger("AnimState", 1);
            }
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (grounded)
            {
                animator.SetInteger("AnimState", 1);
            }
        }
        else // not moving
        {
            if (grounded)
            {
                animator.SetInteger("AnimState", 0);
            }
        }
	}
}
