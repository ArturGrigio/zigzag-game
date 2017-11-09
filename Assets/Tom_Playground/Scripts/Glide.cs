using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	
	public class Glide : Skill {

		#region Public Variables
		/// <summary>
		/// The value that determines how high when the character jumps.
		/// </summary>
		[Tooltip("Value indicating how much damage the skill will do")]
		public float GravityScale = 0.25f;
		#endregion

		#region Private/Protected Variables
		private Vector2 m_defaultGravity;
		private Vector2 m_lowGravity;

		#endregion

		#region Properties
		public override bool CanActivate {
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
				return true;
			}
			return false;
		}

		public override bool Cancel ()
		{
			if (IsActive)
			{
				Debug.Log ("GLIDE STOP");
				bool result = AgentComponent.DeactivateAgentSkill (this);
				if (result == true)
				{
					m_isActive = false;
					Physics2D.gravity = m_defaultGravity;
				}
				return result;
			}
			return false;
		}

		#endregion

		#region Private/Protected Methods
		private void OnGroundEnter (Collider2D collider)
		{
			Cancel ();
		}

		private void calcGravity()
		{
			m_defaultGravity = Physics2D.gravity;
			m_lowGravity = GravityScale * Physics2D.gravity;
			Debug.Log ("Grav=" + m_defaultGravity.ToString () + ", low grav=" + m_lowGravity.ToString ());
		}
		#endregion

		#region Unity Methods
		protected override void Awake()
		{
			base.Awake ();
			m_canCancel = true;
			m_allowMovement = true;
			m_activatorType = ActivatorTypes.Hold;
			AgentComponent.GroundDetectorComponent.OnGroundEnter += OnGroundEnter;
			calcGravity ();
		}
		#endregion
	}
}