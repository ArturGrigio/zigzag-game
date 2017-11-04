using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huy
{
	public class Enemy : MonoBehaviour
	{
		#region Public Variables

		/// <summary>
		/// Enemy movement speed.
		/// </summary>
		[Tooltip("Enemy movement speed")]
		public float speed;

		#endregion

		#region Private/Protected Variables

		/// <summary>
		/// Flag indicating if the enemy is touching the player.
		/// </summary>
		private bool m_touchTarget;

		/// <summary>
		/// The target transform.
		/// </summary>
		private Transform m_targetTransform;

		/// <summary>
		/// The player manager.
		/// </summary>
		private PlayerManager m_playerManager;

		#endregion

		#region Public Properties

		public bool TouchTarget 
		{
			get { return m_touchTarget; }
		}

		public Transform TargetTransform 
		{
			get { return m_targetTransform; }
		}

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
			if (!m_touchTarget)
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

		#region Unity Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Awake()
		{
			m_touchTarget = false;
			m_playerManager = GameObject.FindObjectOfType<PlayerManager> ();
			m_targetTransform = m_playerManager.CurrentShape.transform;
		}

		/// <summary>
		/// Update is called once per frame.
		/// </summary>
		private void Update()
		{
			// Use the y position of this game object so its y location won't change
			Move (m_targetTransform.position.x, transform.position.y);
		}

		/// <summary>
		/// Raises the collision enter2 d event.
		/// </summary>
		/// 
		/// <param name="collision">
		/// Collision data.
		/// </param>
		private void OnCollisionEnter2D(Collision2D collision)
		{
			// Deal damage to target if the collied object is the target
			if (collision.gameObject.CompareTag ("Player"))
			{
				m_touchTarget = true;
			}
		}

		/// <summary>
		/// Raises the collision exit2 d event.
		/// </summary>
		/// 
		/// <param name="collision">
		/// Collied object.
		/// </param>
		private void OnCollisionExit2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag ("Player"))
			{
				m_touchTarget = false;	
			}
		}

		#endregion
	}
}