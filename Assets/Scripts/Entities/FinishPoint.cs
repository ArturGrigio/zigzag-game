using UnityEngine;
using System.Collections;

namespace ZigZag
{
	/// <summary>
	/// This class represents the finish point in the game. It will fire the Finish event to let
	/// the subscribers know that the game has ended.
	/// </summary>
	public class FinishPoint : MonoBehaviour
	{
		#region Public Variables

		/// <summary>
		/// Delegate for the Finish event.
		/// </summary>
		public delegate void FinishHandler();

		/// <summary>
		/// Occurs when the player finishes the game.
		/// </summary>
		public event FinishHandler Finish;

		#endregion

		#region Private Methods

		/// <summary>
		/// Raises the Finish event.
		/// </summary>
		private void OnFinish()
		{
			if (Finish != null)
			{
				Finish.Invoke ();
			}
		}

		#endregion

		#region Unith Methods

		/// <summary>
		/// Raises the trigger enter 2D event.
		/// </summary>
		/// <param name="collider">Collider.</param>
		private void OnTriggerEnter2D (Collider2D collider)
		{
			int activePlayerLayer = LayerMask.NameToLayer ("Active Player");

			if (collider.gameObject.layer == activePlayerLayer)
			{
				OnFinish ();		
			}
		}

		#endregion
	}
}
