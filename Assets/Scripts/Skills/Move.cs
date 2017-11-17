using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag 
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
		public override bool CanActivate 
		{
			get { return true; }
		}
		#endregion

		#region Public Methods
		public override bool ActivateAxis (float axis)
		{
			if( AgentComponent.ActiveSkill == null || AgentComponent.ActiveSkill.AllowMovement == true)
			{
				AgentComponent.SetVelocityX (axis * Speed);
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
			m_activatorType = ActivatorTypes.Axis;
		}

		#endregion
	}

}