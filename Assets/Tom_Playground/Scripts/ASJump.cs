using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag 
{

	public class ASJump : AgentSkill {
		#region Public Variables
		#endregion

		#region Private/Protected Variables
		#endregion

		#region Properties
		#endregion

		#region Public Methods
		public override bool Activate ()
		{
			Debug.Log ("IsGrounded: " + AgentComponent.IsGrounded.ToString ());
			if (AgentComponent.IsGrounded && AgentComponent.ActivateAgentSkill (this))
			{
				AgentComponent.SetVelocityY (AgentComponent.JumpPower);
				AgentComponent.DeactivateAgentSkill (this);
				return true;
			}
			Debug.Log ("Couldn't activate Jump");
			return false;
		}

		#endregion

		#region Private/Protected Methods
		#endregion

		#region Unity Methods
		protected override void Awake()
		{
			base.Awake ();
			m_activator = "Jump";
		}
		#endregion
	}

}