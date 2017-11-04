using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huy
{
	public class MovingPlatform : MonoBehaviour 
	{

		#region Public Variables

		[Tooltip("Distance below starting (X,Y) position to move. The values should be positive.")]
		public Vector2 MinOffset = new Vector2 (0, 0);

		[Tooltip("Distance above starting (X,Y) position to move.")]
		public Vector2 MaxOffset = new Vector2 (0, 0);

		[Tooltip("Starting velocity of platform. Magnitude stays the same when directions switch. Should divide MinOffset and MaxOffset evenly for predictable behavior.")]
		public Vector2 Speed = new Vector2(1,1);

		#endregion

		#region Private/Protected Variables

		private Vector2 m_min, m_max;
		private Rigidbody2D m_rigidBody2D;

		#endregion

		#region Private Methods

		private void setVelocity()
		{
			Vector2 set_velocity = m_rigidBody2D.velocity;
			if (transform.position.x > m_max.x || transform.position.x < m_min.x) 
			{
				set_velocity.x = -set_velocity.x;
			}
			if (transform.position.y > m_max.y || transform.position.y < m_min.y) 
			{
				set_velocity.y = -set_velocity.y;
			}
			m_rigidBody2D.velocity = set_velocity;
		}

		#endregion

		#region Unity Methods
		// Use this for initialization
		void Start () 
		{
			m_rigidBody2D = transform.GetComponent<Rigidbody2D> ();
			m_min = (Vector2)transform.position - MinOffset;
			m_max = (Vector2)transform.position + MaxOffset;
			if (MinOffset != Vector2.zero || MaxOffset != Vector2.zero) 
			{
				m_rigidBody2D.velocity = Speed;
			}
		}
		
		// Update is called once per frame
		void FixedUpdate () 
		{
			setVelocity ();
		}

		#endregion
	}
}