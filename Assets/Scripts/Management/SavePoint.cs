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

		#region Private Variables

		/// <summary>
		/// The audio source component.
		/// </summary>
		private AudioSource m_audioSource;

		/// <summary>
		/// The position of the latest save point.
		/// </summary>
		private static Vector2 LatestSavePoint;

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Awake()
		{
			m_audioSource = GetComponent<AudioSource> ();
		}

		/// <summary>
		/// Raises the trigger enter 2D event.
		/// </summary>
		/// <param name="collider">Collider.</param>
		private void OnTriggerEnter2D(Collider2D collider)
		{
			int activePlayerLayer = LayerMask.NameToLayer ("Active Player");

			// Check if the player has come in contact with a save point and only save 
			// if the save point is the latest one in the game
			if (collider.gameObject.layer == activePlayerLayer && LatestSavePoint.x < transform.position.x)
			{
				m_audioSource.Play ();
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
					SavedPlayerPositions.Add (player, transform.position);
				}
				else
				{
					SavedPlayerPositions[player] = transform.position;
				}
			}

			LatestSavePoint = transform.position;
		}

		#endregion
	}
}
