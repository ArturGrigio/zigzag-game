using UnityEngine;
using System.Collections;

namespace ZigZag
{
	/// <summary>
	/// Handle the wall sliding mechanic.
	/// </summary>
	[RequireComponent(typeof(WallJumpSkill))]
	public class WallSlidingSkill : Skill
	{
		#region Public Variables

		/// <summary>
		/// The sliding multiplier that determines the speed of sliding down against a wall.
		/// </summary>
		[Tooltip("The sliding multiplier that determines the speed of sliding down against a wall.")]
		[Range(20f, 100f)]
		public float SlidingMultiplier = 10f;

		#endregion

		#region Private/Protected Variables

		/// <summary>
		/// Reference to the player agent component.
		/// </summary>
		private Player m_player;

		/// <summary>
		/// Reference to the wall jump skill component.
		/// </summary>
		private WallJumpSkill m_wallJumpSkill;

		/// <summary>
		/// The original angular drag value.
		/// </summary>
		private float m_originalAngularDrag;

		/// <summary>
		/// Flag indicating whether the player has collied against a wall.
		/// </summary>
		private bool m_colliedWall;

		/// <summary>
		/// Rigidbody 2D component.
		/// </summary>
		private Rigidbody2D m_rigidbody2D;

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
		/// 
		/// </summary>
		protected override void Start ()
		{
			base.Start ();

			//m_activatorType = ActivatorTypes.Passive;
			//m_activator = "Passive";
			m_player = AgentComponent as Player;
			m_rigidbody2D = GetComponent<Rigidbody2D> ();
			m_wallJumpSkill = GetComponent<WallJumpSkill> ();

			m_originalAngularDrag = m_rigidbody2D.angularDrag;

		}

		/// <summary>
		/// Raises the collision enter 2D event.
		/// </summary>
		/// 
		/// <remarks>
		/// Activate the skill here.
		/// </remarks>
		/// <param name="collision">Collision.</param>
		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (m_wallJumpSkill.WallCollision)
			{
				m_isActive = true;
				m_rigidbody2D.angularDrag = SlidingMultiplier;
			}
		}

		/// <summary>
		/// Raises the collision exit 2D event.
		/// </summary>
		/// <param name="collision">Collision.</param>
		private void OnCollisionExit2D(Collision2D collision)
		{
			if (m_isActive)
			{
				m_isActive = false;
				m_rigidbody2D.angularDrag = m_originalAngularDrag;
			}
		}

		#endregion
	}
}
