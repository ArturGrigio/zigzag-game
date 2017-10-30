using UnityEngine;
using System.Collections;

namespace ZigZag
{
	/// <summary>
	/// This class handles user inputs and performs the correct
	/// actions based on the inputs.
	/// </summary>
	public class InputController : MonoBehaviour
	{
	 	#region Member Variables

		/// <summary>
		/// The skill manager.
		/// </summary>
		public SkillManager skillManager;

		/// <summary>
		/// The player manager.
		/// </summary>
		public PlayerManager PlayerManager;

		/// <summary>
		/// Flag indicating whether the jump button has been pressed.
		/// </summary>
		private bool m_pressedJump;

		/// <summary>
		/// The input velocity in the x direction.
		/// </summary>
		private float m_velocityX;

		#endregion

		#region Private Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Awake ()
		{
			m_pressedJump = false;
		}
			
		private void Start()
		{
			skillManager.SetCurrentShape (PlayerManager.CurrentShape);
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
				if (Input.GetButtonDown ("Swap Character")) 
				{
					PlayerManager.NextPlayer ();
					skillManager.SetCurrentShape (PlayerManager.CurrentShape);
				}

				m_pressedJump = Input.GetButtonDown ("Jump");
				m_velocityX = Input.GetAxis ("Horizontal");

				if (Input.GetButtonDown ("Attack"))
				{
					skillManager.Attack ();
				}
				else if (Input.GetButtonDown ("Ground Skill"))
				{
					skillManager.Ground ();
				}
				else if (Input.GetButtonDown ("Air Skill"))
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
			PlayerManager.CurrentShape.Move (m_velocityX, 0f);
			PlayerManager.CurrentShape.Jump (m_pressedJump);
		}

		#endregion
	}
}
