using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag 
{
	[RequireComponent(typeof(MultiJump))]
	[RequireComponent(typeof(Attacker))]
	public class RocketDrill : Skill 
	{
		#region Public Variables
		/// <summary>
		/// The value that determines how high when the character jumps.
		/// </summary>
		[Tooltip("Value indicating how much damage the skill will do")]
		public float AttackDamage = 1f;

		[Tooltip("Value indicating how high the character should jump")]
		public float JumpPower = 20f;

		#endregion

		#region Private/Protected Variables
		protected MultiJump m_multiJump;
		protected float m_defaultJump;
		private Animator m_animator;
		private AudioSource m_audioSource;
		#endregion

		#region Properties
		public override bool CanActivate 
		{
			get
			{
				return m_multiJump.CanActivate;
			}
		}
		#endregion

		#region Public Methods
		public override bool Activate ()
		{
			m_multiJump.JumpPower = JumpPower;
			bool result = m_multiJump.Activate ();
			if(result == true)
			{
				result = AgentComponent.ActivateAgentSkill (this);
				if(result == true)
				{
					AgentComponent.SetVelocityX (0f);
					AgentComponent.AttackerComponent.AttackDamage = AttackDamage;
					m_isActive = true;

					// Play the drill audio and animation
					m_audioSource.Play ();
					m_animator.SetInteger ("RocketDrill", 1);
				}
			}
				
			m_multiJump.JumpPower = m_defaultJump;
			return result;
		}

		public override bool Cancel ()
		{
			Debug.Log ("Attempt to cancel Rocket Drill");
			bool result = AgentComponent.DeactivateAgentSkill (this);
			if (result == true)
			{
				m_isActive = false;
				AgentComponent.AttackerComponent.AttackDamage = AgentComponent.AttackerComponent.DefaultAttackDamage;
			}

			return result;
		}

		#endregion

		#region Private/Protected Methods
		protected virtual void OnSurfaceEnter (Collision2D collision, Surface surface)
		{
			if (surface == Surface.Ceiling || surface == Surface.Ground)
			{
				m_animator.SetInteger ("RocketDrill", 0);
				Cancel ();
			}
		}
		#endregion

		#region Unity Methods
		protected override void Awake()
		{
			base.Awake ();
			m_skillType = SkillTypes.Hold;
			m_canCancel = true;
			m_multiJump = AgentComponent.GetComponent<MultiJump> ();
			m_defaultJump = m_multiJump.JumpPower;

			m_audioSource = GetComponent<AudioSource> ();
			m_animator = GetComponent<Animator> ();
		}

		protected virtual void Start()
		{
			AgentComponent.SurfaceDetectorComponent.OnSurfaceEnter += OnSurfaceEnter;
		}
		#endregion
	}

}