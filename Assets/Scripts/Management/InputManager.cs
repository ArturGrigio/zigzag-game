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
		#region Private/Protected Variables

		/// <summary>
		/// The singleton instance of the InputManager class.
		/// </summary>
		private static InputManager m_inputManager = null;

		/// <summary>
		/// The player manager.
		/// </summary>
		private PlayerManager m_playerManager;

		#endregion

		#region Properties

		/// <summary>
		/// Get the InputManager singleton.
		/// </summary>
		public static InputManager Instance
		{
			get { return m_inputManager; }
		}

		#endregion

		#region Unity Methods
			
		/// <summary>
		/// Initialize the singleton instance.
		/// </summary>
		private void Awake ()
		{
			if (m_inputManager != null && m_inputManager != this)
			{
				Destroy (this.gameObject);
			}
			else
			{
				m_inputManager = this;
			}
		}

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Start()
		{
			m_playerManager = PlayerManager.Instance;
		}
			
		/// <remarks>
		/// Check for user inputs every frame.
		/// </remarks>
		private void Update ()
		{
			Skill activeSkill = m_playerManager.CurrentShape.ActiveSkill;
			// UI/Universal input processing
			if (Input.GetButtonDown ("Swap Character"))
			{
				if ( (activeSkill == null  && m_playerManager.CurrentShape.IsGrounded ) || 
					 (activeSkill != null  && activeSkill.CanCancel && activeSkill.Cancel()) )
				{
					m_playerManager.NextPlayer ();
				}
			}
			
			// Player skill checks
			foreach (Skill skill in m_playerManager.CurrentShape.Skills) 
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
