using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ZigZag
{
	/// <summary>
	/// This class handles user inputs and performs the correct
	/// actions based on the inputs.
	/// </summary>
	public class InputController : MonoBehaviour
	{
	 	#region Public Variables

		/// <summary>
		/// The player manager.
		/// </summary>
		public PlayerManager PlayerManager;

		#endregion

		#region Private/Protected Variables

		/// <summary>
		/// The input velocity in the x direction.
		/// </summary>
		private float m_velocityX;

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
			Debug.Log ("Checking for skills: ");
			foreach (KeyValuePair<string,AgentSkill> skill in PlayerManager.CurrentShape.Skills)
			{
				switch (skill.Value.ActivatorType)
				{
					case AgentSkill.ActivatorTypes.Button:
						Debug.Log (skill.Key + ": " + Input.GetButtonDown(skill.Key).ToString());
						if (Input.GetButtonDown (skill.Key))
						{
							skill.Value.Activate ();
						}
						break;
					case AgentSkill.ActivatorTypes.Axis:
						Debug.Log (skill.Key + ": " + Input.GetAxis(skill.Key).ToString());
						skill.Value.ActivateAxis (Input.GetAxis (skill.Key));
						break;
				}
			}
		}

		#endregion
	}
}
