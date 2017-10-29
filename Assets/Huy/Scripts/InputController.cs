using UnityEngine;
using System.Collections;

namespace ZigZag
{
	/// <summary>
	/// This class handles user inputs and performs the correct
	/// actions based on the inputs.
	/// </summary>
	[RequireComponent(typeof(ZigZag.PlayerMovement))]
	public class InputController : MonoBehaviour
	{
	 	#region Member Variables

		/// <summary>
		/// The skill manager.
		/// </summary>
		public SkillManager skillManager;

		/// <summary>
		/// Flag indicating whether the jump button has been pressed.
		/// </summary>
		private bool m_pressedJump;

		/// <summary>
		/// The input velocity in the x direction.
		/// </summary>
		private float m_velocityX;

		/// <summary>
		/// Reference to the PlayerMovement component.
		/// </summary>
		private PlayerMovement m_playerMovement;

		#endregion

		#region Private Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Awake ()
		{
			m_pressedJump = false;
			m_playerMovement = GetComponent<PlayerMovement> ();
		}
			
		/// <summary>
		/// Update is called once per frame.
		/// </summary>
		/// 
		/// <remarks>
		/// Check for user inputs every frame.
		/// </remarks>
		private void Update ()
		{
			// Only accept input when a skill is not being executed
			if (!skillManager.IsSkillActive)
			{
				m_pressedJump = Input.GetButtonDown ("Jump");
				m_velocityX = Input.GetAxis ("Horizontal");

				if (Input.GetKeyDown (KeyCode.Alpha1))
				{
					skillManager.Attack ();
				}
				else if (Input.GetKeyDown (KeyCode.Alpha2))
				{
					skillManager.Ground ();
				}
				else if (Input.GetKeyDown (KeyCode.Alpha3))
				{
					skillManager.Air ();
				}
			}
		}

		/// <summary>
		/// Use for updating physics movement in a fixed frame rate.
		/// </summary>
		private void FixedUpdate()
		{
			m_playerMovement.Move (m_velocityX, 0f);
			m_playerMovement.Jump (m_pressedJump);
		}

		#endregion
	}
}
