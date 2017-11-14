using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag 
{

	public class Jump : Skill 
	{
		#region Public Variables
		/// <summary>
		/// The value that determines how high when the character jumps.
		/// </summary>
		[Tooltip("Value indicating how high the character should jump")]
		public float JumpPower = 15f;
		#endregion

		#region Private/Protected Variables

		/// <summary>
		/// Reference to the wall jump skill component.
		/// This will be null if a shape does not have a wall jump capability.
		/// </summary>
		private WallJumpSkill m_wallJumpSkill;

		#endregion

		#region Properties
		public override bool CanActivate 
		{
			get
			{
				return (m_wallJumpSkill != null && m_wallJumpSkill.WallCollision) || AgentComponent.IsGrounded;
			}
		}
		#endregion

		#region Public Methods

		public override bool Activate ()
		{
			if (AgentComponent.ActivateAgentSkill (this) )
			{
				AgentComponent.SetVelocityY (JumpPower);
				AgentComponent.DeactivateAgentSkill (this);
				return true;
			}
		
			return false;
		}

		#endregion

		#region Private/Protected Methods
		#endregion

		#region Unity Methods
		protected override void Start()
		{
			base.Start ();
			m_wallJumpSkill = GetComponent<WallJumpSkill> ();
		}
		#endregion
	}

}