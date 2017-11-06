using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag {

	[RequireComponent (typeof(Rigidbody2D))]
	[RequireComponent (typeof(EdgeCollider2D))]
	public abstract class Agent : Health {

		#region Public Variables

		public enum Direction { Left = -1, Center = 0, Right = 1};

		/// <summary>
		/// Character movement speed.
		/// </summary>
		[Tooltip("Character movement speed")]
		public float Speed = 10.0f;

		/// <summary>
		/// The value that determines how high when the character jumps.
		/// </summary>
		[Tooltip("Value indicating how high the character should jump")]
		public float JumpPower = 10.0f;

		[Tooltip("Direction the agent is facing. Can be Left,Center,Right [-1,0,1]")]
		public Direction Facing = Direction.Center;

		#endregion

		#region Private/Protected Variables

		private bool m_isGrounded = true;
		private Dictionary<string, AgentSkill> m_skills;
		private Vector2 m_newVelocity = Vector2.zero;
		private bool m_updateVelocity = false;

		protected Rigidbody2D m_rigidBody2D;
		protected bool m_lockSkills = false;
		protected AgentSkill m_activeSkill = null;

		/// <summary>
		/// The 2D edge collider component of the character.
		/// </summary>
		private EdgeCollider2D m_edgeCollider2D;



		#endregion

		#region Properties

		/// <summary>
		/// Gets a value indicating whether this instance is on the ground.
		/// </summary>
		/// <value><c>true</c> if this instance is grounded; otherwise, <c>false</c>.</value>
		public bool IsGrounded
		{
			get { return m_isGrounded; }
		}

		/// <summary>
		/// Set of <see cref="ZigZag.AgentSkill"/>s available to this <see cref="ZigZag.Agent"/>.
		/// </summary>
		/// <value>Reference to <see cref="ZigZag.AgentSkill"/> object, using its <see cref="ZigZag.AgentSkill.Activator"/> as the key. </value>
		public Dictionary<string, AgentSkill> Skills
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

		public Rigidbody2D Rigidbody2DComponent
		{
			get { return m_rigidBody2D; }
		}

		public bool IsAttacking { get; set; }

		#endregion

		#region Public Methods
		public bool ActivateAgentSkill(AgentSkill s)
		{
			Debug.Log ("ActiveSkill = " + ((m_activeSkill == null) ? "null" : m_activeSkill.name));
			if (m_activeSkill == null || 
				(m_activeSkill.CanCancel && m_activeSkill.Cancel () == true))
			{
				m_activeSkill = s;
				return true;
			}
			return false;
		}

		public bool DeactivateAgentSkill(AgentSkill s)
		{
			if (m_activeSkill == s)
			{
				m_activeSkill = null;
				return true;
			}
			return false;
		}

		public void SetVelocityX(float velocity) 
		{
			SetVelocity (velocity, m_updateVelocity ? m_newVelocity.y : m_rigidBody2D.velocity.y);
		}

		public void SetVelocityY(float velocity)
		{
			SetVelocity (m_updateVelocity ? m_newVelocity.x : m_rigidBody2D.velocity.x, velocity);
		}

		public void SetVelocity(float velocityX, float velocityY)
		{
			m_updateVelocity = true;
			m_newVelocity = new Vector2 (velocityX, velocityY);
			Debug.Log("Set velocity: " + m_newVelocity.ToString());
		}

		#endregion

		#region Private/Protected Methods
		private void loadSkills()
		{
			m_skills = new Dictionary<string, AgentSkill> ();
			Debug.Log ("Load skills (" + gameObject.name + "): ");
			foreach (AgentSkill skill in gameObject.GetComponents<AgentSkill>()) {
				Debug.Log ("Found: " + skill.Activator);
				m_skills.Add (skill.Activator, skill);
			}
		}

		#endregion

		#region Unity Methods
		protected override void Awake()
		{
			base.Awake ();
			m_rigidBody2D = GetComponent<Rigidbody2D> ();
			m_edgeCollider2D = GetComponent<EdgeCollider2D> ();
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

		void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.tag == "Platform" && collision.otherCollider == m_edgeCollider2D) 
			{
				m_isGrounded = true;
			}
		}

		void OnCollisionExit2D(Collision2D collision)
		{
			if (collision.gameObject.tag == "Platform" && collision.otherCollider == m_edgeCollider2D) 
			{
				m_isGrounded = false;
			}
		}


		#endregion

	}

}