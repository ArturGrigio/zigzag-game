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
		/// The door in which will be used to lock the player inside the boss room.
		/// </summary>
		[Tooltip("The door in which will be used to lock the player inside the boss room")]
		public GameObject BossDoor;

		/// <summary>
		/// Delegate handler for handling the boss event.
		/// </summary>
		public delegate void BossEventHandler ();

		/// <summary>
		/// Occurs when player is about to face the final boss.
		/// </summary>
		public event BossEventHandler BeforeBoss;

		public bool IsBossDeath = false;

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

			if (collider.gameObject.layer == activePlayerLayer && !IsBossDeath)
			{
				// Fire the event
				OnBoss ();

				BossDoor.SetActive (true);
				Debug.Log ("boss event fire");
			}
		}

		#endregion
	}
}
