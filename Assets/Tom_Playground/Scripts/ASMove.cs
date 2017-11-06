using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag {

	public class ASMove : AgentSkill {
		#region Public Variables

		#endregion

		#region Private/Protected Variables
		#endregion

		#region Properties
		#endregion

		#region Public Methods
		public override bool ActivateAxis (float axis)
		{
			if(AgentComponent.IsGrounded && AgentComponent.ActivateAgentSkill(this))
			{
				AgentComponent.SetVelocityX (axis * AgentComponent.Speed);
				AgentComponent.DeactivateAgentSkill (this);
				return true;
			}
			return false;
		}

		#endregion

		#region Private/Protected Methods
		#endregion

		#region Unity Methods
		protected override void Awake()
		{
			base.Awake ();
			m_activatorType = ActivatorTypes.Axis;
			m_activator = "Horizontal";
		}
		#endregion
	}

}