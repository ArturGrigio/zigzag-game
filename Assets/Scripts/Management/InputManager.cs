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
			Skill activeSkill = PlayerManager.CurrentShape.ActiveSkill;
			// UI/Universal input processing
			if (Input.GetButtonDown ("Swap Character"))
			{
				if (activeSkill == null || (activeSkill.CanCancel && activeSkill.Cancel()))
				{
					PlayerManager.NextPlayer ();
				}
			}
			
			// Player skill checks
			foreach (Skill skill in PlayerManager.CurrentShape.Skills) 
			{
				switch (skill.SkillType) 
				{
				case SkillTypes.Axis:
					if (skill.CanActivate) 
					{
						skill.ActivateAxis (Input.GetAxis (skill.Activator));
					}
					break;

				case SkillTypes.Hold:
					if (skill.IsActive && Input.GetButton (skill.Activator) == false) 
					{
						Debug.Log ("HOLD RELEASE");
						skill.Cancel ();
					} 
					else if (skill.CanActivate && Input.GetButtonDown (skill.Activator)) 
					{
						Debug.Log ("HOLD BEGIN");
						skill.Activate ();
					}
					break;

				case SkillTypes.Toggle:
					if (Input.GetButton (skill.Activator)) 
					{
						if (skill.CanCancel && skill.IsActive) 
						{
							skill.Cancel ();
						} 
						else if (skill.CanActivate) 
						{
							skill.Activate ();
						}
					}
					break;

				case SkillTypes.Instant:
					if (skill.CanActivate && Input.GetButtonDown (skill.Activator)) 
					{
						skill.Activate ();
					}
					break;
				case SkillTypes.Passive:
					break;
				}
			}
		}

		#endregion
	}
}
