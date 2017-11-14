using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag 
{		
	public class PlayerManager : MonoBehaviour 
	{
		#region Public Variables

		[Tooltip("Main camera which follows the active player")]
		public Camera2DFollow PlayerCamera;

		#endregion

		#region Private/Protected Variables

		[SerializeField]
		[Tooltip("Sets the starting player object when set in a scene. Indicates the active player object at runtime.")]
		private Player m_currentShape;

		private int m_activeIndex = 0;
		private List<Player> m_players = new List<Player>();
		private int m_activeLayer;
		private int m_inactiveLayer;

		#endregion

		#region Properties
		public Player CurrentShape
		{
			get { return m_currentShape; }
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

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Sets the active player object.
		/// </summary>
		/// <param name="index">Index of desired player object.</param>
		private void changePlayer(int index) 
		{
			m_currentShape.gameObject.layer = m_inactiveLayer;
			m_activeIndex = index;
			m_currentShape = m_players [m_activeIndex];
			m_currentShape.gameObject.layer = m_activeLayer;
			PlayerCamera.Target = m_currentShape.gameObject.transform;
		}

		/// <summary>
		/// Loads the players from the PlayerManager's child objects.
		/// </summary>
		private void loadPlayers()
		{
			bool activeSet = false;
			m_players.Clear ();

			foreach(Player player in GetComponentsInChildren<Player>())
			{
				if (player == m_currentShape) 
				{
					m_activeIndex = m_players.Count;
					activeSet = true;
				}
				m_players.Add (player);
			}

			if (activeSet == false)
			{
				m_currentShape = m_players [m_activeIndex];
			}
		}

		#endregion

		#region Unity Methods

		// Use this for initialization
		// Update is called once per frame
		private void Awake()
		{
			//Disable collision between player objects and set active player
			m_activeLayer = LayerMask.NameToLayer("Active Player");
			m_inactiveLayer = LayerMask.NameToLayer("Inactive Player");
			loadPlayers ();
			changePlayer (m_activeIndex);
		}

		#endregion
	}
}