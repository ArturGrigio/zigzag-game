using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	/// <summary>
	/// Move the enemy and attack player upon touch.
	/// </summary>
	public class Patrol : Skill 
	{
		#region Public Variables

		public Vector2 MinPosition;
		public Vector2 MaxPosition;
		public Vector2 Velocity;
		public Direction direction;

		#endregion

		#region Private/Protected Variables

		private Rigidbody2D m_rigidbody2D;

		#endregion

		#region Properties
		#endregion

		#region Public Methods

		#endregion

		#region Private/Protected Methods

		#endregion

		#region Unity Methods

		protected override void Awake ()
		{
			base.Awake ();

			m_rigidbody2D = GetComponent<Rigidbody2D> ();
		}

		private void FixedUpdate()
		{
			if (transform.position.x > MaxPosition.x && direction == Direction.Right)
			{
				Velocity = new Vector2 (-Velocity.x, Velocity.y);
				direction = Direction.Left;
			}
			else if (transform.position.x < MinPosition.x && direction == Direction.Left)
			{
				Velocity = new Vector2 (-Velocity.x, Velocity.y);
				direction = Direction.Right;
			}
			else if (transform.position.y > MaxPosition.y && direction == Direction.Up)
			{
				Velocity = new Vector2 (Velocity.x, -Velocity.y);
				direction = Direction.Down;
			}
			else if (transform.position.y < MinPosition.y && direction == Direction.Down)
			{
				Velocity = new Vector2 (Velocity.x, -Velocity.y);
				direction = Direction.Up;
			}

			m_rigidbody2D.velocity = Velocity;	
		}

		#endregion
	}
}