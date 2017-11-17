using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag 
{
	public class Stomp: Skill 
	{
		#region Public Variables
		/// <summary>
		/// The value that determines how high when the character jumps.
		/// </summary>
		[Tooltip("Value indicating how much damage the skill will do")]
		public int AttackDamage = 1;
		public float Speed = 30f;
		#endregion

		#region Private/Protected Variables
		private float m_defaultMass;
		#endregion

		#region Properties
		public override bool CanActivate
		{
			get
			{ return AgentComponent.IsGrounded == false; }
		}
		#endregion

		#region Public Methods
		public override bool Activate ()
		{
			if (AgentComponent.ActivateAgentSkill (this))
			{
				AgentComponent.AttackDamage = AttackDamage;
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
					AgentComponent.AttackDamage = 0f;
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
		protected override void Start()
		{
			base.Start ();
			AgentComponent.SurfaceDetectorComponent.OnSurfaceEnter += OnSurfaceEnter;
		}
		#endregion
	}

}