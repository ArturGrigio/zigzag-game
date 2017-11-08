using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huy
{

	public class Move : Skill 
	{
		#region Public Variables
		/// <summary>
		/// Character movement speed.
		/// </summary>
		[Tooltip("Character movement speed")]
		public float Speed = 10.0f;
		#endregion

		#region Private/Protected Variables
		#endregion

		#region Properties
		#endregion

		#region Public Methods
		public override bool ActivateAxis (float axis)
		{
			if(AgentComponent.ActivateAgentSkill(this))
			{
				AgentComponent.SetVelocityX (axis * Speed);
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