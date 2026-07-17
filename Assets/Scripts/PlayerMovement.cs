using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovementNameSpace
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] Rigidbody2D rb;

        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float jump = 5f;
        [SerializeField] private float maxFallSpeed = -15f;
        [SerializeField] private float fallMultiplier = 2.5f;

        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;



        private float horizontal;
        private float vertical;
        private Vector2 moveInput;

        private bool doubleJumped = false;
        private bool facingRight = true;

        private bool moving = false;

        [SerializeField] private Animator anim;

        private void FixedUpdate()
        {
            //set abunatuibs
            anim.SetBool("IsRunning", moving && IsGrounded());

            //move
            rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);

            //resets double jump once landing
            if (IsGrounded())
            {
                doubleJumped = true;
            }

            //flip when changing direction
            if ((horizontal > 0.01f && !facingRight) || (horizontal < -0.01f && facingRight))
            {
                Flip();
            }

            if (rb.linearVelocity.y < 0)
            {
                // Falling - apply extra gravity
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
        }

        public float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }

        #region PLAYER_CONTROLS
        public void Move(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
            horizontal = moveInput.x;

            moving = true;
            if (context.canceled) moving = false;
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                anim.SetTrigger("Jump");
                if (IsGrounded())
                {
                    Debug.Log("Was Grounded");
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
                }
                else if (doubleJumped)
                {
                    Debug.Log("Double Jumped");
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
                    doubleJumped = !doubleJumped;
                }
            }
        }

        public void FastFall(InputAction.CallbackContext context)
        {
            if (context.performed && !IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * -fallMultiplier);
            }
        }

        private bool IsGrounded()
        {
            bool grounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(1f, 0.1f), 0, groundLayer);
            if (grounded == true) 
            { 
                anim.SetBool("Grounded", true);       
            }
            else 
            {
                anim.SetBool("Grounded", false);
                anim.SetFloat("AirSpeedY", -1);
            }
            return grounded;
        }

        private void Flip()
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        #endregion
    }
}
