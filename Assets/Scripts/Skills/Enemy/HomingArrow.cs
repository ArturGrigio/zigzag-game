using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	/// <summary>
	/// Class representing the arrow attack of the boss.
	/// </summary>
	public class HomingArrow : MonoBehaviour 
	{
		#region Public Variables

		/// <summary>
		/// The attack damage.
		/// </summary>
		[Tooltip("The attack damage")]
		public float AttackDamage = 1f;

		/// <summary>
		/// The speed of the arrow.
		/// </summary>
		[Tooltip("The speed of the arrow")]
		public float Speed = 5f;

		/// <summary>
		/// The rotating speed of the arrow.
		/// </summary>
		[Tooltip("The rotating speed of the arrow")]
		public float RotatingSpeed = 200f;

		/// <summary>
		/// The destroy time of the arrow in seconds.
		/// </summary>
		[Tooltip("The destroy time of the arrow in seconds")]
		public float DestroyTime = 5f;

		#endregion

		#region Private/Protected Variables

		/// <summary>
		/// The rigidbody 2D.
		/// </summary>
		private Rigidbody2D m_rigidbody2D;

		/// <summary>
		/// The player manager.
		/// </summary>
		private PlayerManager m_playerManager;

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Disable the arrow object after a certain amount of seconds.
		/// </summary>
		/// <returns>Iterator.</returns>
		private IEnumerator disableArrowObject()
		{
			yield return new WaitForSeconds (DestroyTime);
			gameObject.SetActive (false);
		}
			
		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Start()
		{
			m_playerManager = PlayerManager.Instance;
			m_rigidbody2D = GetComponent<Rigidbody2D> ();

			StartCoroutine (disableArrowObject ());
		}

		/// <summary>
		/// Control the homing rotation and direction of the arrow.
		/// </summary>
		private void FixedUpdate()
		{
			// Get a pointing vector of the target
			Vector2 pointToTarget = (Vector2)transform.position - (Vector2)m_playerManager.CurrentShape.transform.position;
			pointToTarget.Normalize ();

			float value = Vector3.Cross (pointToTarget, transform.right).z;
			 
			// Rotate and move the arrow
			m_rigidbody2D.angularVelocity = RotatingSpeed * value;
			m_rigidbody2D.velocity = transform.right * Speed;
		}

		/// <summary>
		/// Handle the collision enter 2D event.
		/// </summary>
		/// <param name="collision">Collision.</param>
		private void OnCollisionEnter2D(Collision2D collision)
		{
			gameObject.SetActive (false);

			int activePlayerLayer = LayerMask.NameToLayer ("Active Player");
			if (collision.collider.gameObject.layer == activePlayerLayer)
			{
				Player currentShape = collision.collider.GetComponent<Player> ();
				currentShape.ReceiveDamage (AttackDamage);
			}
		}

		#endregion
	}
}
