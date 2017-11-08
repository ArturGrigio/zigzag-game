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

		#endregion

		#region Properties
		#endregion

		#region Public Methods
		public override bool Activate ()
		{
			Debug.Log ("IsGrounded: " + AgentComponent.IsGrounded.ToString ());
			if (AgentComponent.IsGrounded && AgentComponent.ActivateAgentSkill (this))
			{
				AgentComponent.SetVelocityY (JumpPower);
				AgentComponent.DeactivateAgentSkill (this);
				return true;
			}
			Debug.Log ("Couldn't activate Jump");
			return false;
		}

		#endregion

		#region Private/Protected Methodss

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