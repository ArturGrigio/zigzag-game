using UnityEngine;
using System.Collections.Generic;

namespace ZigZag
{
	/// <summary>
	/// This class saves the positions of all the acquired shapes when
	/// the current shape reaches to a save point.
	/// </summary>
	public class SavePoint : MonoBehaviour
	{
		#region Public Variables

		/// <summary>
		/// The player manager.
		/// </summary>
		public PlayerManager playerManager;

		/// <summary>
		/// Dictionary stores the players and their saved positions.
		/// </summary>
		public static Dictionary<Player, Vector2> SavedPlayerPositions = new Dictionary<Player, Vector2>();

		#endregion

		#region Unity Methods

		/// <summary>
		/// Raises the trigger enter 2D event.
		/// </summary>
		/// <param name="collider">Collider.</param>
		private void OnTriggerEnter2D(Collider2D collider)
		{
			int activePlayerLayer = LayerMask.NameToLayer ("Active Player");

			// Check if the player has come in contact with a save point
			if (collider.gameObject.layer == activePlayerLayer)
			{
				savePositions ();
			}
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Save the current positions of the shapes in the player manager.
		/// </summary>
		private void savePositions()
		{
			foreach (Player player in playerManager.Players)
			{
				if (!SavedPlayerPositions.ContainsKey (player))
				{
					SavedPlayerPositions.Add (player, player.transform.position);
				}
				else
				{
					SavedPlayerPositions[player] = player.transform.position;
				}
			}
		}

		#endregion
	}
}
