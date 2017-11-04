using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huy
{
	public class Player : MonoBehaviour
	{
		#region Public Variables

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

		#endregion

		#region Private Variables

		/// <summary>
		/// Flag indicating whether the character is on the ground or not.
		/// </summary>
		private bool m_isGrounded;

		/// <summary>
		/// Flag indicating whether the character is sliding down against a wall.
		/// </summary>
		private bool m_isSliding;

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
		/// The 2D rigid body component of the character.
		/// </summary>
		private Rigidbody2D m_rigidbody2D;

		/// <summary>
		/// The 2D edge collider component of the character.
		/// </summary>
		private EdgeCollider2D m_edgeCollider2D;

		#endregion

		#region Public Methods

		/// <summary>
		/// Move the character.
		/// </summary>
		/// 
		/// <param name="velocityX">
		/// Velocity of the character in the x direction.
		/// </param>
		/// 
		/// <param name="velocityY">
		/// Unused velocity in the y direction.
		/// </param>
		public void Move(float velocityX, float velocityY)
		{
			m_rigidbody2D.velocity = new Vector2 (velocityX * speed, m_rigidbody2D.velocity.y);

			// Flip the character depending on which direction it is facing
			if (velocityX > 0 && !m_facingRight)
			{
				flip ();
			}
			else if (velocityX < 0 && m_facingRight)
			{
				flip ();
			}
		}

		/// <summary>
		/// Make the character jump.
		/// </summary>
		/// 
		/// <param name="jumpFlag">
		/// Flag indicating if the player presses the jump button.
		/// </param>
		public void Jump(bool jumpFlag)
		{
			if (jumpFlag && m_isGrounded)
			{
				m_rigidbody2D.velocity = Vector2.up * jumpPower;
				m_isGrounded = false;
			}
		}

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Awake () 
		{
			m_isGrounded = true;
			m_isSliding = false;
			m_facingRight = true;
			m_rigidbody2D = GetComponent<Rigidbody2D>();
			m_edgeCollider2D = GetComponent<EdgeCollider2D> ();
		}

		/// <summary>
		/// Use for updating physics movement in a fixed frame rate.
		/// </summary>
		private void FixedUpdate ()
		{
			if (!m_isSliding)
			{
				fallFaster ();
			}
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
		/// <param name="collision">
		/// The collision data.
		/// </param>
		void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.tag == "Platform") 
			{
				m_isGrounded = true;
			}

			if (collision.otherCollider == m_edgeCollider2D)
			{
				// Fix the z rotation if it is not 0 when the edge collider
				// collides with something else
				if (transform.eulerAngles.z != 0f)
				{
					flip ();
				}

				// Deal jump damage here
				Health health = collision.gameObject.GetComponent<Health> ();
				if (health != null)
				{
					health.ReceiveDamage (1f);
				}
			}

			wallJump ();
			slideDownWall ();
		}

		/// <summary>
		/// Raises the collision exit 2D event.
		/// </summary>
		///
		/// <param name="collision">
		/// The collision data.
		/// </param>
		private void OnCollisionExit2D(Collision2D collision)
		{
			if (collision.gameObject.tag == "Platform") 
			{
				m_isGrounded = false;
			}
			m_isSliding = false;
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Make the character jump higher when the jump button is held down.
		/// Otherwises make the character jump lower.
		/// </summary>
		private void fallFaster()
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
		/// The modified rotation in this method is in degree and it z and x
		/// rotation will be set to 0 degree.
		/// 
		/// Rotate 45 degree when facing right
		/// Rotate 135 degree when facing left
		/// </remarks>
		private void flip()
		{
			m_facingRight = !m_facingRight;
			float yRotation = (m_facingRight) ? 45f : 135f;

			transform.rotation = Quaternion.Euler (0f, yRotation, 0f);
			transform.Rotate (Vector3.left * Time.deltaTime);
		}

		/// <summary>
		/// Enable the wall jump mechanic.
		/// </summary>
		private void wallJump()
		{
			if (wallCollided())
			{
				m_isGrounded = true;
			}
		}

		/// <summary>
		/// Enable the sliding down wall mechanic.
		/// </summary>
		private void slideDownWall()
		{
			if (wallCollided ())
			{
				m_rigidbody2D.velocity = new Vector2 (m_rigidbody2D.velocity.x, -Time.deltaTime);
				m_isSliding = true;
			}
		}

		/// <summary>
		/// Detect if the player has collided to a wall.
		/// </summary>
		/// 
		/// <returns>
		/// True if wall was collided. False otherwise.
		/// </returns>
		private bool wallCollided()
		{
			// Cast a ray in the direction the character is facing in 
			// order to detect a wall
			Vector2 direction = (m_facingRight) ? Vector2.right : Vector2.left;
			RaycastHit2D hit = Physics2D.Raycast (transform.position, direction, 1f);

			return (hit.normal == Vector2.left || hit.normal == Vector2.right);
		}
			
		#endregion
	}
}