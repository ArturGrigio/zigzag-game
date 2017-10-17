using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	#region Member Variables

	public float speed = 1.0f;
	public float jumpPower = 1.0f;

	private bool m_isGrounded;
	private bool m_facingRight;
	private Rigidbody2D m_rigidbody2D;

	#endregion

	/// <summary>
	/// Initialize member variables.
	/// </summary>
	void Awake () 
	{
		m_isGrounded = true;
		m_facingRight = true;
		m_rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	/// <summary>
	/// Update() is called every frame.
	/// </summary>
	void Update () 
	{
		float moveHorizontal = Input.GetAxis("Horizontal") * speed;
		m_rigidbody2D.velocity = new Vector2 (moveHorizontal, m_rigidbody2D.velocity.y);

		// Jump
		if (Input.GetButton ("Jump") && m_isGrounded)
		{
			m_rigidbody2D.velocity = Vector2.up * jumpPower;
			m_isGrounded = false;
		}

		// Flip the character depending on which direction it is facing
		if (moveHorizontal > 0 && !m_facingRight)
		{
			Flip ();
		}
		else if (moveHorizontal < 0 && m_facingRight)
		{
			Flip ();
		} 
	}

	/// <summary>
	/// Flip the character to face the current direction it is facing.
	/// </summary>
	/// 
	/// <remarks>
	/// The modified rotation in this method is in radian.
	/// </remarks>
	private void Flip()
	{
		m_facingRight = !m_facingRight;
		float yRotation = 360.0f - transform.eulerAngles.y;
		Vector3 newEulerAngles = new Vector3 (0.0f, yRotation, 0.0f);

		transform.eulerAngles = newEulerAngles;
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// 
	/// <remarks>
	/// Set the m_isGrounded flag to true.
	/// </remarks>
	/// <param name="other">Other.</param>
	private void OnCollisionEnter2D(Collision2D other)
	{
		m_isGrounded = true;
	}
}
