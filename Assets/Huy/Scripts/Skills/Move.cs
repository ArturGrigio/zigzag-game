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

				// Flip the character to the correct facing direction
				if ((axis > 0f && AgentComponent.Facing == Agent.Direction.Left) ||
					(axis < 0f && AgentComponent.Facing == Agent.Direction.Right))
				{
					//flip ();
				}

				return true;
			}
			return false;
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Flip the character to face the current direction it is facing.
		/// </summary>
		/// 
		/// <remarks>
		/// The modified rotation in this method is in degree and it z and x
		/// rotation will be set to 0 degree.
		/// </remarks>
		private void flip()
		{
			float yRotation = 0f;

			if (AgentComponent.Facing == Agent.Direction.Right)
			{
				AgentComponent.Facing = Agent.Direction.Left;
				yRotation = 45f;
			}
			else if (AgentComponent.Facing == Agent.Direction.Left)
			{
				AgentComponent.Facing = Agent.Direction.Right;
				yRotation = -45f;
			}
				
			transform.rotation = Quaternion.Euler (0f, yRotation, 0f);
			transform.Rotate (Vector3.left * Time.deltaTime);
		}

		#endregion

		#region Unity Methods
		protected override void Start()
		{
			base.Start ();
			m_activatorType = ActivatorTypes.Axis;
			m_activator = "Horizontal";
		}
		#endregion
	}
}