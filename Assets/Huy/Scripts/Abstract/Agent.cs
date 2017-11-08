using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huy 
{

	[RequireComponent (typeof(Rigidbody2D))]
	public abstract class Agent : Health 
    {
		#region Public Variables

		public enum Direction { Left = -1, Center = 0, Right = 1};

		[SerializeField]
		[Tooltip("Gameobject with 2D collider used to determine where ground is. MUST be set as a trigger and in the GroundDetection layer.")]
		private GroundDetector m_GroundTrigger;

		#endregion

		#region Private/Protected Variables

		private bool m_isGrounded = true;
		private Dictionary<string, Skill> m_skills;
		private Vector2 m_newVelocity = Vector2.zero;
		private bool m_updateVelocity = false;


		protected Direction m_facing = Direction.Right;
		protected Rigidbody2D m_rigidbody2D;
		protected bool m_lockSkills = false;
		protected Skill m_activeSkill = null;

        /// <summary>
        /// The fall multiplier for higher jump.
        /// </summary>
        private const float HighFallMultiplier = 2.5f;

        /// <summary>
        /// The fall multiplier for lower jump.
        /// </summary>
        private const float LowFallMultiplier = 1f;

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
		public Dictionary<string, Skill> Skills
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
			get { return m_rigidbody2D; }
		}

		public Direction Facing 
		{
			get { return m_facing; }
		}

		public bool IsAttacking { get; set; }

		#endregion

		#region Public Methods
		public bool ActivateAgentSkill(Skill s)
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

		public bool DeactivateAgentSkill(Skill s)
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
			SetVelocity (velocity, m_updateVelocity ? m_newVelocity.y : m_rigidbody2D.velocity.y);
		}

		public void SetVelocityY(float velocity)
		{
			SetVelocity (m_updateVelocity ? m_newVelocity.x : m_rigidbody2D.velocity.x, velocity);
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
			m_skills = new Dictionary<string, Skill> ();
			Debug.Log ("Load skills (" + gameObject.name + "): ");

			foreach (Skill skill in gameObject.GetComponents<Skill>()) 
            {
				Debug.Log ("Found: " + skill.Activator);
				m_skills.Add (skill.Activator, skill);
			}
		}

        /// <summary>
        /// Make the character jump higher when the jump button is held down.
        /// Otherwises make the character jump lower.
        /// </summary>
        private void fallFaster()
        {
            if (m_rigidbody2D.velocity.y < 0f)
            {
                // High jump
                m_rigidbody2D.velocity += Vector2.up * HighFallMultiplier * Physics2D.gravity.y * Time.deltaTime;
            }
            else if (m_rigidbody2D.velocity.y > 0f && !Input.GetButton ("Jump"))
            {
                // Low jump
                m_rigidbody2D.velocity += Vector2.up * LowFallMultiplier * Physics2D.gravity.y * Time.deltaTime;
            }
        }

		private void OnGroundEnter(Collider2D collider) 
		{
			Debug.Log ("Ground Enter Agent");
			m_isGrounded = true;
		}

		private void OnGroundExit(Collider2D collider)
		{
			Debug.Log ("Ground Exit Agent");
			m_isGrounded = false;
		}

		#endregion

		#region Unity Methods
		protected override void Awake()
		{
			base.Awake ();
			m_rigidbody2D = GetComponent<Rigidbody2D> ();
//			m_GroundTrigger.OnGroundEnter += OnGroundEnter;
//			m_GroundTrigger.OnGroundExit += OnGroundExit;
			loadSkills ();
		}

		private void FixedUpdate() 
		{
			if (m_updateVelocity == true)
			{
				m_rigidbody2D.velocity = m_newVelocity;
				m_updateVelocity = false;
				m_newVelocity = Vector2.zero;
			}

            fallFaster();
		}

		#endregion

	}
}