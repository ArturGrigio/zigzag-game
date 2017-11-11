using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huy 
{

	public class Jump : Skill 
	{
		#region Public Variables
		/// <summary>
		/// The value that determines how high when the character jumps.
		/// </summary>
		[Tooltip("Value indicating how high the character should jump")]
		public float JumpPower = 30f;
		#endregion

		#region Private/Protected Variables

		/// <summary>
		/// Reference to the wall jump skill component.
		/// This will be null if a shape does not have a wall jump capability.
		/// </summary>
		private WallJumpSkill m_wallJumpSkill;

		#endregion

		#region Properties
		#endregion

		#region Public Methods
		public override bool Activate ()
		{
			//Debug.Log ("IsGrounded: " + AgentComponent.IsGrounded.ToString ());
			if ( ((m_wallJumpSkill != null && m_wallJumpSkill.WallCollision) || AgentComponent.IsGrounded) &&
				 AgentComponent.ActivateAgentSkill (this) )
			{
				AgentComponent.SetVelocityY (JumpPower);
				AgentComponent.DeactivateAgentSkill (this);
				return true;
			}
			//Debug.Log ("Couldn't activate Jump");
			return false;
		}

		#endregion

		#region Private/Protected Methodss

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