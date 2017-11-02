using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huy
{
	public class Player : Agent
	{
		#region Public Variables

		#endregion

		#region Private Variables

		#endregion

		#region Public Methods

		/// <summary>
		/// Detect if the player has collided to a wall.
		/// </summary>
		/// 
		/// <returns>
		/// True if collie against wall. False otherwise.
		/// </returns>
		public bool WallCollided()
		{
			// Cast a ray in the direction the character is facing in 
			// order to detect a wall
			Vector2 direction = Vector2.zero;

			if (m_facing == Direction.Left)
			{
				direction = Vector2.left;
			}
			else if (m_facing == Direction.Right)
			{
				direction = Vector2.right;
			}

			int inactivePlayerLayer = LayerMask.NameToLayer("Inactive Player");
			RaycastHit2D hit = Physics2D.Raycast (transform.position, direction, 0.5f, inactivePlayerLayer);

			return (hit.normal == Vector2.left || hit.normal == Vector2.right);
		}

		#endregion

		#region Unity Methods

		protected override void Awake()
		{
			base.Awake ();

//			Debug.Log ("init Player");
//			Debug.Log (gameObject.name);
//			Debug.Log (m_rigidbody2D);
		}


		#endregion

		#region Private/Protected Methods

		#endregion
	}
}