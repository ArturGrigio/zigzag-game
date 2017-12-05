using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag 
{
	[RequireComponent(typeof(Attacker))]
	public class Stomp: Skill 
	{
		#region Public Variables
		/// <summary>
		/// The value that determines how high when the character jumps.
		/// </summary>
		[Tooltip("Value indicating how much damage the skill will do")]
		public float AttackDamage = 1f;

		/// <summary>
		/// The speed of crashing down when Stomp is activated.
		/// </summary>
		[Tooltip("The speed of crashing down when Stomp is activated")]
		public float Speed = 30f;

		/// <summary>
		/// The stomp audio.
		/// </summary>
		[Tooltip("The stomp audio")]
		public AudioClip StompAudio;

		#endregion

		#region Private/Protected Variables
		private float m_defaultMass;
		private AudioSource m_audioSource;
		#endregion

		#region Properties
		public override bool CanActivate
		{
			get { return AgentComponent.IsGrounded == false; }
		}
		#endregion

		#region Public Methods
		public override bool Activate ()
		{
			if (AgentComponent.ActivateAgentSkill (this))
			{
				AgentComponent.AttackerComponent.AttackDamage = AttackDamage;
				m_isActive = true;
				AgentComponent.SetVelocity (0, -Speed);
				return true;
			}
			return false;
		}

		public override bool Cancel ()
		{
			if (m_isActive)
			{
				bool result = AgentComponent.DeactivateAgentSkill (this);
				if (result == true)
				{
					m_isActive = false;
					AgentComponent.AttackerComponent.AttackDamage = AgentComponent.AttackerComponent.DefaultAttackDamage;

					// Set velocity to 0 to reduce the chances of cube 
					// falling through the ground or platforms
					AgentComponent.SetVelocity(0f, 0f);

					// Play the stomp audio
					m_audioSource.clip = StompAudio;
					m_audioSource.Play ();
				}
					
				return result;
			}
			return false;
		}

		#endregion

		#region Private/Protected Methods
		private void OnSurfaceEnter (Collision2D collision, Surface surface)
		{
			if (surface == Surface.Ground)
			{
				Cancel ();
			}
		}
		#endregion

		#region Unity Methods

		protected override void Awake ()
		{
			base.Awake ();
			m_skillType = SkillTypes.Instant;
			m_audioSource = GetComponent<AudioSource> ();
		}

		protected virtual void Start()
		{
			AgentComponent.SurfaceDetectorComponent.OnSurfaceEnter += OnSurfaceEnter;
		}
		#endregion
	}

}