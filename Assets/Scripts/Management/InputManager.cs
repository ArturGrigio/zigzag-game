using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ZigZag
{
	/// <summary>
	/// This class handles user inputs and performs the correct
	/// actions based on the inputs.
	/// </summary>
	public class InputManager : MonoBehaviour
	{
	 	#region Public Variables

		/// <summary>
		/// The player manager.
		/// </summary>
		public PlayerManager PlayerManager;

		#endregion

		#region Private/Protected Variables

		#endregion

		#region Unity Methods
			

		/// <remarks>
		/// Check for user inputs every frame.
		/// </remarks>
		private void Update ()
		{
			if (PlayerManager.CurrentShape.CanCancel)
			{
				// UI/Universal input processing
				if (Input.GetButtonDown ("Swap Character"))
				{
					if (PlayerManager.CurrentShape.ActiveSkill == null 
						|| PlayerManager.CurrentShape.ActiveSkill.Cancel())
					{
						PlayerManager.NextPlayer ();
					}
				}
				
				// Player skill checks
				foreach (Skill skill in PlayerManager.CurrentShape.Skills)
				{
					if (skill.CanActivate)
					{
						switch (skill.ActivatorType)
						{
						case Skill.ActivatorTypes.Axis:
							skill.ActivateAxis (Input.GetAxis (skill.Activator));
							break;

						case Skill.ActivatorTypes.Hold:
							if (skill.IsActive && Input.GetButton (skill.Activator) == false)
							{
								Debug.Log ("HOLD RELEASE");
								skill.Cancel ();
							}
							else if (Input.GetButtonDown (skill.Activator))
							{
								Debug.Log ("HOLD BEGIN");
								skill.Activate ();
							}
							break;

						case Skill.ActivatorTypes.Toggle:
							if (skill.IsActive == false && Input.GetButton (skill.Activator))
							{
								if (skill.IsActive)
								{
									skill.Cancel ();
								}
								else
								{
									skill.Activate ();
								}
							}
							break;

						case Skill.ActivatorTypes.Instant:
							if (Input.GetButtonDown (skill.Activator))
							{
								skill.Activate ();
							}
							break;
						}
					}
				}
			}
		}

		#endregion
	}
}
