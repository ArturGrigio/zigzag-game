using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Huy
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
			
		/// <summary>
		/// Update is called once per frame.
		/// </summary>
		/// 
		/// <remarks>
		/// Check for user inputs every frame.
		/// </remarks>
		private void Update ()
		{
			// UI/Universal input processing
			if (Input.GetButtonDown ("Swap Character")) 
			{
				PlayerManager.NextPlayer ();
			}
				
			foreach (KeyValuePair<string,Skill> skill in PlayerManager.CurrentShape.Skills)
			{
				switch (skill.Value.ActivatorType)
				{
					case Skill.ActivatorTypes.Button:
						if (Input.GetButtonDown (skill.Key))
						{
							skill.Value.Activate ();
						}
						else if (Input.GetButtonDown (skill.Key))
						{
							skill.Value.Activate ();
						}

						break;

					case Skill.ActivatorTypes.Axis:
						skill.Value.ActivateAxis (Input.GetAxis (skill.Key));
						break;
				}
			}
		}

		#endregion
	}
}
