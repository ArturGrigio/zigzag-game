using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class InputController : MonoBehaviour
{
	#region Member Variables

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
		m_pressedJump = Input.GetButtonDown ("Jump");
		m_velocityX = Input.GetAxis("Horizontal");
	}

	/// <summary>
	/// Use for updating physics movement in a fixed frame rate.
	/// </summary>
	private void FixedUpdate()
	{
		m_playerMovement.Move (m_velocityX);
		m_playerMovement.Jump (m_pressedJump);
	}

	#endregion
}

