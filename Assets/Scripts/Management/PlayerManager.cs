using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag 
{		
	/// <summary>
	/// Manage all the player shapes.
	/// </summary>
	public class PlayerManager : MonoBehaviour 
	{
		#region Public Variables

		[Tooltip("Main camera which follows the active player")]
		public Camera2DFollow PlayerCamera;

		/// <summary>
		/// Delegate handler for handling the death of the player.
		/// </summary>
		public delegate void PlayerDeathHandler ();

		/// <summary>
		/// Occurs when the player dies.
		/// </summary>
		public event PlayerDeathHandler PlayerDeath;

		/// <summary>
		/// The health bar GUI.
		/// </summary>
		public GameObject HealthBar;

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

		/// <summary>
		/// Get a list of acquired shapes.
		/// </summary>
		/// <value>The player shapes.</value>
		public List<Player> Players
		{
			get { return m_players; }
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

		/// <summary>
		/// Load the saved positions from the latest save point.
		/// </summary>
		public void LoadPositions()
		{
			foreach (Player player in m_players)
			{
				player.transform.position = SavePoint.SavedPlayerPositions [player];
				player.ReceiveHeal (player.FullHealth);
			}
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Sets the active player object.
		/// </summary>
		/// <param name="index">Index of desired player object.</param>
		private void changePlayer(int index) 
		{
			SpriteRenderer spriteRenderer;

			// Set the old shape to inactive status
			m_currentShape.gameObject.layer = m_inactiveLayer;
			m_activeIndex = index;
			m_currentShape.Death -= playerDeathHandler;

			// Set the old shape half-transparent
			spriteRenderer = m_currentShape.GetComponent<SpriteRenderer> ();
			spriteRenderer.color = new Color (1f, 1f, 1f, 0.5f);

			// Set the new current shape active
			m_currentShape = m_players [m_activeIndex];
			m_currentShape.gameObject.layer = m_activeLayer;
			PlayerCamera.Target = m_currentShape.gameObject.transform;
			m_currentShape.Death += playerDeathHandler;

			// Set the new current shape opaque
			spriteRenderer = m_currentShape.GetComponent<SpriteRenderer> ();
			spriteRenderer.color = new Color (1f, 1f, 1f, 1f);

			displayCurrentHealth ();
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

		/// <summary>
		/// Handle the player death event. Simply fire the 
		/// player death event to the SceneManager.
		/// </summary>
		private void playerDeathHandler()
		{
			OnPlayerDeath ();
		}

		/// <summary>
		/// Raises the player death event.
		/// </summary>
		private void OnPlayerDeath()
		{
			if (PlayerDeath != null)
			{
				PlayerDeath.Invoke ();
			}
		}

		/// <summary>
		/// Display the health of the current shape.
		/// </summary>
		private void displayCurrentHealth()
		{
			float scaledHealth = m_currentShape.CurrentHealth / m_currentShape.FullHealth;

			float y = HealthBar.transform.localScale.y;
			float z = HealthBar.transform.localScale.z;
			HealthBar.transform.localScale = new Vector3 (scaledHealth, y, z);
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

		/// <summary>
		/// Check every frame to see if the player has fallen off over a cliff.
		/// </summary>
		private void FixedUpdate()
		{
			if (m_currentShape.transform.position.y < -10f)
			{
				playerDeathHandler ();
			}
		}

		#endregion
	}
}