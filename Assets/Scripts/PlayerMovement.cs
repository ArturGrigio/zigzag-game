using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	#region Member Variables

	/// <summary>
	/// Character movement speed.
	/// </summary>
	[Tooltip("Character movement speed")]
	public float speed = 1.0f;

	/// <summary>
	/// The value that determines how high when the character jumps.
	/// </summary>
	[Tooltip("Value indicating how high the character should jump")]
	public float jumpPower = 1.0f;

	/// <summary>
	/// The bottom collider for detecting collision with the ground.
	/// </summary>
	[Tooltip("Collider use for detecting collision with the ground")]
	public BoxCollider2D bottomCollider;

	/// <summary>
	/// Flag indicating whether the character is on the ground or not.
	/// </summary>
	private bool m_isGrounded;

	/// <summary>
	/// Flag indicating whether the character is facing right (true)
	/// or left (false).
	/// </summary>
	private bool m_facingRight;

	/// <summary>
	/// The fall multiplier for higher jump.
	/// </summary>
	private const float HighFallMultiplier = 2.5f;

	/// <summary>
	/// The fall multiplier for lower jump.
	/// </summary>
	private const float LowFallMultiplier = 1f;

	/// <summary>
	/// The 2D rigid body componentof the character.
	/// </summary>
	private Rigidbody2D m_rigidbody2D;

	#endregion

	#region Public Methods

	/// <summary>
	/// Move the character.
	/// </summary>
	/// 
	/// <param name="velocityX">
	/// Velocity of the character in the x direction.
	/// </param>
	public void Move(float velocityX)
	{
		m_rigidbody2D.velocity = new Vector2 (velocityX * speed, m_rigidbody2D.velocity.y);

		// Flip the character depending on which direction it is facing
		if (velocityX > 0 && !m_facingRight)
		{
			Flip ();
		}
		else if (velocityX < 0 && m_facingRight)
		{
			Flip ();
		}
	}

	/// <summary>
	/// Make the character jump.
	/// </summary>
	/// 
	/// <param name="pressedJump">
	/// Flag indicating if the player presses the jump button.
	/// </param>
	public void Jump(bool pressedJump)
	{
		if (pressedJump && m_isGrounded)
		{
			m_rigidbody2D.velocity = Vector2.up * jumpPower;
			m_isGrounded = false;
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Initialize member variables.
	/// </summary>
	private void Awake () 
	{
		m_isGrounded = true;
		m_facingRight = true;
		m_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	/// <summary>
	/// Use for updating physics movement in a fixed frame rate.
	/// </summary>
	private void FixedUpdate () 
	{
		FallFaster ();
	}

	/// <summary>
	/// Make the character jump higher when the jump button is held down.
	/// Otherwises make the character jump lower.
	/// </summary>
	private void FallFaster()
	{
		if (m_rigidbody2D.velocity.y < 0f)
		{
			// Low jump
			m_rigidbody2D.velocity += Vector2.up * HighFallMultiplier * Physics2D.gravity.y * Time.deltaTime;
		}
		else if (m_rigidbody2D.velocity.y > 0f && !Input.GetButton ("Jump"))
		{
			// High jump
			m_rigidbody2D.velocity += Vector2.up * LowFallMultiplier * Physics2D.gravity.y * Time.deltaTime;
		}
	}

	/// <summary>
	/// Flip the character to face the current direction it is facing.
	/// </summary>
	/// 
	/// <remarks>
	/// The modified rotation in this method is in degree.
	/// </remarks>
	private void Flip()
	{
		m_facingRight = !m_facingRight;
		float yRotation = (m_facingRight) ? 45f : 135f;

		transform.rotation = Quaternion.Euler (0f, yRotation, 0f);
		transform.Rotate (Vector3.left * Time.deltaTime);
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// 
	/// <remarks>
	/// Set the m_isGrounded flag to true when the bottom collider
	/// hits an object.
	/// </remarks>
	/// 
	/// <param name="other">
	/// The collied object.
	/// </param>
	private void OnCollisionEnter2D(Collision2D colliedObject)
	{
		if (colliedObject.otherCollider == bottomCollider)
		{
			m_isGrounded = true;
		}
	}

	#endregion
}
