using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag 
{

	public class Jump : Skill {
		#region Public Variables
		/// <summary>
		/// The value that determines how high when the character jumps.
		/// </summary>
		[Tooltip("Value indicating how high the character should jump")]
		public float JumpPower = 15f;
		#endregion

		#region Private/Protected Variables
		#endregion

		#region Properties
		public override bool CanActivate {
			get
			{
				return AgentComponent.IsGrounded;
			}
		}
		#endregion

		#region Public Methods

		public override bool Activate ()
		{
			if (AgentComponent.ActivateAgentSkill(this))
			{
				AgentComponent.SetVelocityY (JumpPower);
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
		}
		#endregion
	}

}