using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag 
{
	public enum Direction { Left = -1, Center = 0, Right = 1};

	[RequireComponent (typeof(Rigidbody2D))]
	[RequireComponent (typeof(SpriteRenderer))]
	public abstract class Agent : Health 
	{
		#region Public Variables

		#endregion

		#region Private/Protected Variables

		protected SurfaceDetector m_surfaceDetector;
		protected Direction m_facing = Direction.Right;
		protected Rigidbody2D m_rigidBody2D;
		protected Skill m_activeSkill = null;
		protected SpriteRenderer m_spriteRenderer;


		private List<Skill> m_skills;
		private Vector2 m_newVelocity = Vector2.zero;
		private bool m_updateVelocity = false;

		#endregion

		#region Properties

		/// <summary>
		/// Gets a value indicating whether this instance is on the ground.
		/// </summary>
		/// <value><c>true</c> if this instance is grounded; otherwise, <c>false</c>.</value>
		public bool IsGrounded
		{
			get { return m_surfaceDetector.IsOnSurface (Surface.Ground); }
		}

		/// <summary>
		/// Set of <see cref="ZigZag.AgentSkill"/>s available to this <see cref="ZigZag.Agent"/>.
		/// </summary>
		/// <value>Reference to <see cref="ZigZag.AgentSkill"/> object, using its <see cref="ZigZag.AgentSkill.Activator"/> as the key. </value>
		public List<Skill> Skills
		{
			get { return m_skills; }
		}
			
		/// <summary>
		/// Gets a value indicating whether this <see cref="ZigZag.Agent"/> can cancel its current <see cref="ZigZag.AgentSkill"/>.
		/// </summary>
		/// <value><c>true</c> if skill can be cancelled; otherwise, <c>false</c>.</value>
		public bool CanCancel
		{
			get { return (m_activeSkill == null || m_activeSkill.CanCancel); }
		}
			
		public Direction Facing 
		{
			get { return m_facing; }
		}

		public SurfaceDetector SurfaceDetectorComponent
		{
			get { return m_surfaceDetector; }
		}
			
		public Skill ActiveSkill
		{
			get { return m_activeSkill; }
		}

		public float AttackDamage { get; set; }

		#endregion

		#region Public Methods
		public bool ActivateAgentSkill(Skill s)
		{
			if (m_activeSkill == null || 
				(m_activeSkill.CanCancel && m_activeSkill.Cancel () == true))
			{
				m_activeSkill = s;
				return true;
			}
			return false;
		}

		public bool DeactivateAgentSkill(Skill s)
		{
			if (m_activeSkill == s)
			{
				m_activeSkill = null;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Sets the x velocity of the agent. If velocity was set since the last FixedUpdate,
		/// uses the new y velocity. Otherwise, uses current Agent y velocity.
		/// </summary>
		/// <param name="velocity">Velocity.</param>
		public void SetVelocityX(float velocity) 
		{
			SetVelocity (velocity, m_updateVelocity ? m_newVelocity.y : m_rigidBody2D.velocity.y);
		}

		/// <summary>
		/// Sets the y velocity of the agent. If velocity was set since the last FixedUpdate,
		/// uses the new x velocity. Otherwise, uses the current Agent x velocity.
		/// </summary>
		/// <param name="velocity">Velocity.</param>
		public void SetVelocityY(float velocity)
		{
			SetVelocity (m_updateVelocity ? m_newVelocity.x : m_rigidBody2D.velocity.x, velocity);
		}

		/// <summary>
		/// Sets the velocity of the agent.
		/// </summary>
		/// <param name="velocityX">Velocity x.</param>
		/// <param name="velocityY">Velocity y.</param>
		public void SetVelocity(float velocityX, float velocityY)
		{
			m_updateVelocity = true;
			m_newVelocity = new Vector2 (velocityX, velocityY);
			if (velocityX < 0 && m_facing == Direction.Right)
			{
				m_facing = Direction.Left;
				horizontalFlip ();
			} else if (velocityX > 0 && m_facing == Direction.Left)
			{
				m_facing = Direction.Right;
				horizontalFlip ();
			}
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Detects skill scripts attached to the agent and adds them to its skills list.
		/// </summary>
		private void loadSkills()
		{
			m_skills = new List<Skill> ();

			foreach (Skill skill in gameObject.GetComponents<Skill>()) 
			{
				m_skills.Add (skill);
			}
		}

		/// <summary>
		/// Performs actions which are required when facing direction changes.
		/// </summary>
		protected virtual void horizontalFlip ()
		{
			m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
		}
		#endregion

		#region Unity Methods
		protected override void Awake()
		{
			base.Awake ();
			m_rigidBody2D = GetComponent<Rigidbody2D> ();
			m_surfaceDetector = GetComponent<SurfaceDetector> ();
			m_spriteRenderer = GetComponent<SpriteRenderer> ();
			AttackDamage = 0f;
			loadSkills ();
		}

		private void FixedUpdate() 
		{
			if (m_updateVelocity == true)
			{
				m_rigidBody2D.velocity = m_newVelocity;
				m_updateVelocity = false;
				m_newVelocity = Vector2.zero;
			}
		}

		#endregion

	}

}