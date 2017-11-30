using UnityEngine;
using System.Collections;

namespace ZigZag
{
	/// <summary>
	/// Handle the wall jump mechanic.
	/// </summary>
	public class WallJump : Jump
	{
		#region Public Variables

		/// <summary>
		/// The sliding multiplier that determines the speed of sliding down a wall.
		/// </summary>
		[Tooltip("The sliding multiplier that determines the speed of sliding down a wall.")]
		[Range(20f, 50f)]
		public float SlidingMultiplier = 10f;

		#endregion

		#region Private/Protected Variablels

		/// <summary>
		/// Flag indicating whether the player has collied against a wall.
		/// </summary>
		private bool m_colliedWall;

		/// <summary>
		/// The rigidbody 2D component.
		/// </summary>
		private Rigidbody2D m_rigidbody2D;

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
			get{ return base.CanActivate || m_colliedWall; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Activate the skill and begin any sort of animations or movements.
		/// </summary>
		public override bool Activate ()
		{
			if (m_colliedWall && AgentComponent.ActivateAgentSkill (this))
			{
				m_rigidbody2D.drag = 1f;
				AgentComponent.SetVelocityY (JumpPower);
				AgentComponent.DeactivateAgentSkill (this);
				m_colliedWall = false;

				return true;
			}

			return base.Activate ();
		}

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();
			m_skillType = SkillTypes.Instant;
			m_colliedWall = false;

			m_rigidbody2D = GetComponent<Rigidbody2D> ();
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
		private void OnCollisionEnter2D(Collision2D collision)
		{
			ContactPoint2D[] contacts = new ContactPoint2D[10];
			collision.GetContacts (contacts);

			// Loop throug each contact point to see if player has come in contact with a wall
			foreach (ContactPoint2D contact in contacts)
			{
				Surface colliedSurface = SurfaceDetector.SurfaceFromNormal (contact.normal);

				if (colliedSurface == Surface.LeftWall || colliedSurface == Surface.RightWall)
				{
					m_colliedWall = true;
					m_rigidbody2D.drag = SlidingMultiplier;
					break;
				}
			}
		}

		/// <summary>
		/// Raises the collision exit 2D event.
		/// </summary>
		/// <param name="collision">Collision.</param>
		private void OnCollisionExit2D(Collision2D collision)
		{
			if (m_colliedWall)
			{
				m_colliedWall = false;
				m_rigidbody2D.drag = 1f;
			}
		}

		#endregion
	}
}
