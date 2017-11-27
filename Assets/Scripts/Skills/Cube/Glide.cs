using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	public class Glide : Skill 
	{
		#region Public Variables

		/// <summary>
		/// The value that determines how high when the character jumps.
		/// </summary>
		[Tooltip("Value indicating how much damage the skill will do")]
		public float GravityScale = 0.25f;

		/// <summary>
		/// The glide audio clip.
		/// </summary>
		[Tooltip("The glide audio clip")]
		public AudioClip GlideAudio;

		#endregion

		#region Private/Protected Variables
		private Vector2 m_defaultGravity;
		private Vector2 m_lowGravity;
		private AudioSource m_audioSource;
		private Animator m_animator;
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
				Debug.Log ("GLIDE START");
				m_isActive = true;
				AgentComponent.SetVelocityY(0);
				Physics2D.gravity = m_lowGravity;

				// Play the glide audio and animation
				m_audioSource.clip = GlideAudio;
				m_audioSource.loop = true;
				m_audioSource.Play ();
				m_animator.SetInteger ("Glide", 1);

				return true;
			}
			return false;
		}

		public override bool Cancel ()
		{
			if (m_isActive)
			{
				Debug.Log ("GLIDE STOP");
				bool result = AgentComponent.DeactivateAgentSkill (this);
				if (result == true)
				{
					m_isActive = false;
					Physics2D.gravity = m_defaultGravity;
				}

				m_animator.SetInteger ("Glide", 0);
				m_audioSource.loop = false;

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

		private void calcGravity()
		{
			m_defaultGravity = Physics2D.gravity;
			m_lowGravity = GravityScale * Physics2D.gravity;
		}
		#endregion

		#region Unity Methods
		protected override void Awake()
		{
			base.Awake ();
			m_canCancel = true;
			m_allowMovement = true;
			m_skillType = SkillTypes.Hold;
			calcGravity ();

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