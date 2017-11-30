using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	public class Glide : Skill 
	{
		#region Public Variables

		/// <summary>
		/// The value that determines how much gravity force should affect the cube.
		/// </summary>
		[Tooltip("The value that determines how much gravity force should affect the cube")]
		public float GravityScale = 0.25f;

		/// <summary>
		/// The linear drag that determines the how slow the cube moves in glide form.
		/// </summary>
		[Tooltip("The linear drag that determines how slow the cube moves in glide form")]
		public float LinearDrag = 0.25f;

		/// <summary>
		/// The glide audio clip.
		/// </summary>
		[Tooltip("The glide audio clip")]
		public AudioClip GlideAudio;

		#endregion

		#region Private/Protected Variables
		private AudioSource m_audioSource;
		private Animator m_animator;
		private Rigidbody2D m_rigidbody2D;
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

				// Apply the gravity scale and linear drag
				m_rigidbody2D.gravityScale = GravityScale;
				m_rigidbody2D.drag = LinearDrag;

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

					// Revert the gravity scale and linear drag to default values
					m_rigidbody2D.gravityScale = 1f;
					m_rigidbody2D.drag = 0f;
				}

				// Stop playing the Glide animation and stop the audio from looping
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
			
		#endregion

		#region Unity Methods
		protected override void Awake()
		{
			base.Awake ();
			m_canCancel = true;
			m_allowMovement = true;
			m_skillType = SkillTypes.Hold;

			m_audioSource = GetComponent<AudioSource> ();
			m_animator = GetComponent<Animator> ();
			m_rigidbody2D = GetComponent<Rigidbody2D> ();
		}

		protected virtual void Start()
		{
			AgentComponent.SurfaceDetectorComponent.OnSurfaceEnter += OnSurfaceEnter;
		}
		#endregion
	}
}