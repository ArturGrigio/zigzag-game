using UnityEngine;
using System.Collections;

namespace ZigZag
{
	/// <summary>
	/// This class serves as an event trigger for "before" the player fights the boss.
	/// </summary>
	public class BossTrigger : MonoBehaviour
	{
		#region Public Variables

		/// <summary>
		/// Delegate handler for handling the boss event.
		/// </summary>
		public delegate void BossEventHandler ();

		/// <summary>
		/// Occurs when player is about to face the final boss.
		/// </summary>
		public event BossEventHandler BeforeBoss;

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Raises the boss event.
		/// </summary>
		private void OnBoss()
		{
			if (BeforeBoss != null)
			{
				BeforeBoss.Invoke ();
			}
		}

		#endregion

		#region Unity Methods

		/// <summary>
		/// Raises the trigger enter 2D event.
		/// </summary>
		/// <param name="collider">Collider.</param>
		private void OnTriggerEnter2D(Collider2D collider)
		{
			int activePlayerLayer = LayerMask.NameToLayer ("Active Player");

			if (collider.gameObject.layer == activePlayerLayer)
			{
				// Fire the event
				OnBoss ();
				Debug.Log ("boss event fire");
			}
		}

		#endregion
	}
}
