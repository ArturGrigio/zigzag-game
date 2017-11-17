using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	public class Player : Agent
	{
		#region Public Variables

		/// <summary>
		/// List of layers to raycast against to.
		/// </summary>
		private LayerMask Layers = 0x800;

		#endregion

		#region Private/Protected Variables
		#endregion

		#region Properties
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
				
			RaycastHit2D hit = Physics2D.Raycast (transform.position, direction, 0.5f, Layers);

			return (hit.normal == Vector2.left || hit.normal == Vector2.right);
		}

		#endregion

		#region Private/Protected Methods
		protected override void die ()
		{
			throw new System.NotImplementedException ();
		}	
		#endregion

		#region Unity Methods

		protected override void Awake()
		{
			base.Awake();
		}

		#endregion
	}
}