using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Transform GFX;
	[SerializeField] private float jumpForce = 10f;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private Transform feetPos;
	[SerializeField] private float groundDistance = 0.25f;
	[SerializeField] private float jumpTime = 0.3f;

	[SerializeField] private float crouchHeight = 0.5f;

	private bool isGrounded = false;
	private bool isJumping = false;
	private float jumpTimer;

    private bool isCroughed = false;

    public bool IsGrounded => isGrounded;
    public bool IsJumping => isJumping;
    public float JumpTime => jumpTime;
    public bool IsCroughed => isCroughed;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();

        
	}

	private void Update() {
		isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

		#region JUMPING
		if (Input.GetButtonDown("Jump")) { 
			StartJump();
		}


		/*
		if (isGrounded && Input.GetButtonDown("Jump")) {
			isJumping = true;
			rb.velocity = Vector2.up * jumpForce;
			//Debug.Log("PULAR");
		}*/

		if (Input.GetButton("Jump"))
		{
			UpdateJump();
		}

        /*
		if (isJumping && Input.GetButton("Jump")) {

			if (jumpTimer < jumpTime) {
				rb.velocity = Vector2.up * jumpForce;

				jumpTimer += Time.deltaTime;
			} else {
				isJumping = false;
			}
			//Debug.Log("PULANDO");

        }*/

        if (Input.GetButtonUp("Jump")) {
			EndJump();
        }
		/*
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpTimer = 0;
            //Debug.Log("PULOU");
        }*/
        #endregion

        #region CROUCHING

        if (Input.GetButton("Crouch")) {
            StartCrough();
        }
        /*
        if (isGrounded && Input.GetButton("Crouch"))
        {
            GFX.localScale = new Vector3(GFX.localScale.x, crouchHeight, GFX.localScale.z);

            if (isJumping)
            {
                GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
            }
            Debug.Log("AGACHANDO");

        }*/

        if (Input.GetButtonUp("Crouch")) {
            EndCrough();
        }
        /*
        if (Input.GetButtonUp("Crouch"))
        {
            GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
            Debug.Log("AGACHOU");

        }*/

        #endregion
    }

    public void StartJump()
    {
        if (isGrounded)
        {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            //Debug.Log("PULAR");
        }
    }

    public void UpdateJump() {
        if (isJumping)
        {

            if (jumpTimer < jumpTime)
            {
                rb.velocity = Vector2.up * jumpForce;

                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
            //Debug.Log("PULANDO");

        }
    }

	public void EndJump() {
        isJumping = false;
        jumpTimer = 0;
        //Debug.Log("PULOU");
    }

	public void StartCrough() {
        if (isGrounded )
        {
            GFX.localScale = new Vector3(GFX.localScale.x, crouchHeight, GFX.localScale.z);
            isCroughed = true;
            if (isJumping)
            {
                GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
                isCroughed = false;
            }
            //Debug.Log("AGACHANDO");

        }
    }

	public void EndCrough() {
        GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
        isCroughed = false;
        //Debug.Log("AGACHOU");
    }

}
