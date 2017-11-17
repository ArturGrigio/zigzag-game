using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huy 
{		
	public class PlayerManager : MonoBehaviour 
	{
		#region Public Variables

		[Tooltip("Sets the starting player object when set in a scene. Indicates the active player object at runtime.")]
		public Player CurrentShape;

		[Tooltip("Main camera which follows the active player")]
		public Camera2DFollow PlayerCamera;

		#endregion

		#region Private/Protected Variables

		private PlayerHealth m_playerHealth = null;
		private int m_activeIndex = 0;
		private List<Player> m_players = new List<Player>();
		private int m_activeLayer;
		private int m_inactiveLayer;

		#endregion

		#region Unity Methods

		// Use this for initialization
		// Update is called once per frame
		void Awake()
		{
			//Disable collision between player objects and set active player
			m_activeLayer = LayerMask.NameToLayer("Active Player");
			m_inactiveLayer = LayerMask.NameToLayer("Inactive Player");
			loadPlayers ();
			changePlayer (m_activeIndex);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Changes the active player object to the next in the list.
		/// Rotates back to the first object when the end is reached.
		/// </summary>
		public void NextPlayer()
		{
			changePlayer ((m_activeIndex + 1) % m_players.Count);
		}

		public void ReceiveDamage(float damage)
		{
			m_playerHealth.ReceiveDamage (damage);
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Sets the active player object.
		/// </summary>
		/// <param name="index">Index of desired player object.</param>
		private void changePlayer(int index) 
		{
			CurrentShape.gameObject.layer = m_inactiveLayer;
			m_activeIndex = index;
			CurrentShape = m_players [m_activeIndex];
			CurrentShape.gameObject.layer = m_activeLayer;
			PlayerCamera.Target = CurrentShape.gameObject.transform;
		}

		/// <summary>
		/// Loads the players.
		/// </summary>
		private void loadPlayers()
		{
			bool activeSet = false;
			m_players.Clear ();

			foreach (Transform t in transform) 
			{
				Player shapePlayer = t.gameObject.GetComponent<Player> ();
				if (shapePlayer != null) 
				{
					if (shapePlayer == CurrentShape) 
					{
						m_activeIndex = m_players.Count;
						activeSet = true;
					}

					m_players.Add (shapePlayer);
				}
			}

			if (activeSet == false)
			{
				CurrentShape = m_players [m_activeIndex];
			}
		}
			
		#endregion
	}
}