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

        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;

        private float horizontal;
        private float vertical;
        private Vector2 moveInput;

        private bool doubleJumped = false;
        private bool facingRight = true;

        private void FixedUpdate()
        {
            rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);

            if (IsGrounded())
            {
                doubleJumped = true;
            }

            if (horizontal > 0.01f && !facingRight)
            {
                Flip();
            }
            else if (horizontal < -0.01f && facingRight)
            {
                Flip();
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
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
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

        private bool IsGrounded()
        {
            return Physics2D.OverlapBox(groundCheck.position, new Vector2(1f, 0.1f), 0, groundLayer);
        }

        private void Flip()
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        #endregion
    }
}
