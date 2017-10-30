using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	public class EnemyMovement : MonoBehaviour, IMovement
	{
		#region Member Variables

		/// <summary>
		/// Enemy movement speed.
		/// </summary>
		[Tooltip("Enemy movement speed")]
		public float speed;

		/// <summary>
		/// The target transform.
		/// </summary>
		[Tooltip("Reference to a target transform")]
		public Transform targetTransform;

		/// <summary>
		/// Flag indicating if the enemy is touching the player.
		/// </summary>
		private bool m_touchtarget;

		#endregion

		#region Public Methods

		/// <summary>
		/// Move the enemy.
		/// </summary>
		/// 
		/// <param name="x">
		/// The x coordinate to move to.
		/// </param>
		/// 
		/// <param name="y">
		/// The y coordinate to move to.
		/// </param>
		public void Move(float x, float y)
		{
			if (!m_touchtarget)
			{
				Vector2 targetPosition = new Vector2 (x, y);
				transform.position = Vector2.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);
			}
		}

		/// <summary>
		/// Make the enemy jump. NOT IMPELEMENTED.
		/// </summary>
		/// 
		/// <param name="jumpFlag">
		/// Flag indicating if the needs to jump.
		/// </param>
		public void Jump(bool jumpFlag)
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Awake()
		{
			m_touchtarget = false;
		}

		/// <summary>
		/// Update is called once per frame.
		/// </summary>
		private void Update()
		{
			// Use the y position of this game object so its y location won't change
			Move (targetTransform.position.x, transform.position.y);
		}

		/// <summary>
		/// Raises the collision enter2 d event.
		/// </summary>
		/// 
		/// <param name="colliedObject">
		/// Collied object.
		/// </param>
		private void OnCollisionEnter2D(Collision2D colliedObject)
		{
			// Deal damage to target if the collied object is the target
			if (colliedObject.gameObject.CompareTag ("Player"))
			{
				m_touchtarget = true;
			}
		}

		/// <summary>
		/// Raises the collision exit2 d event.
		/// </summary>
		/// 
		/// <param name="colliedObject">
		/// Collied object.
		/// </param>
		private void OnCollisionExit2D(Collision2D colliedObject)
		{
			if (colliedObject.gameObject.CompareTag ("Player"))
			{
				m_touchtarget = false;	
			}
		}

		#endregion
	}
}