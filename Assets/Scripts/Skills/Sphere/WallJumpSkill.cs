using UnityEngine;
using System.Collections;

namespace ZigZag
{
	/// <summary>
	/// Handle the wall jump mechanic.
	/// </summary>
	[RequireComponent(typeof(Jump))]
	public class WallJumpSkill : Skill
	{
		#region Private/Protected Variablels

		/// <summary>
		/// Reference to the player agent component.
		/// </summary>
		private Player m_player;

		/// <summary>
		/// Flag indicating whether the player has collied against a wall.
		/// </summary>
		private bool m_colliedWall;

		#endregion

		#region Properties

		/// <summary>
		/// Gets a value indicating whether this <see cref="Huy.WallJumpSkill"/> wall collision.
		/// </summary>
		/// <value><c>true</c> if wall collision; otherwise, <c>false</c>.</value>
		public bool WallCollision 
		{
			get { return m_colliedWall; }
		
		}
			
		public override bool CanActivate
		{
			get
			{ return false; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Activate the skill and begin any sort of animations or movements.
		/// </summary>
		public override bool Activate ()
		{
			return false;
		}

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			m_player = AgentComponent as Player;
			m_colliedWall = false;
		}

		/// <summary>
		/// Raises the collision enter 2D event.
		/// </summary>
		/// 
		/// <remarks>
		/// Activate the skill here.
		/// </remarks>
		/// 
		/// <param name="collision">Collision.</param>
//		private void OnCollisionEnter2D(Collision2D collision)
//		{
//			// Only activate when player collies against a wall and if sphere
//			// is not on the ground
//			if (m_player.WallCollided () && !m_player.IsGrounded)
//			{
//				Debug.Log ("collied against wall");
//				m_colliedWall = true;
//			}
//		}
//
		/// <summary>
		/// Raises the collision exit 2D event.
		/// </summary>
		/// <param name="collision">Collision.</param>
//		private void OnCollisionExit2D(Collision2D collision)
//		{
//			if (m_colliedWall && !m_player.IsGrounded)
//			{
//				Debug.Log ("exit wall");
//				m_colliedWall = false;
//			}
//		}
//
		#endregion
	}
}
