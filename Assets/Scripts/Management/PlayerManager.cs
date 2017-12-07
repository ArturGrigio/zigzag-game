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
		/// Occurs when the player dies.
		/// </summary>
		public event Health.DeathHandler PlayerDeath;

		/// <summary>
		/// Spawn player handler delegate.
		/// </summary>
		public delegate void RespawnPlayerHandler();

		/// <summary>
		/// Occurs when player is respawned.
		/// </summary>
		public event RespawnPlayerHandler RespawnPlayer;

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

		/// <summary>
		/// The singleton instance of the PlayerManager class.
		/// </summary>
		private static PlayerManager m_playerManager = null;

		/// <summary>
		/// The original x scale of the health bar.
		/// </summary>
		private float m_originalHealthBarXScale;

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

		/// <summary>
		/// Get the PlayerManager singleton.
		/// </summary>
		public static PlayerManager Instance
		{
			get { return m_playerManager; }
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
		/// Respawn the player and publish the RespawnPlayer event.
		/// </summary>
		public void Respawn()
		{
			foreach (Player player in m_players)
			{
				player.transform.position = SavePoint.LatestSavePoint;
				player.ReceiveHeal (player.FullHealth);
			}

			// Let subscribers know the player has been respawned
			OnRespawnPlayer ();
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Sets the active player object.
		/// </summary>
		/// <param name="index">Index of desired player object.</param>
		private void changePlayer(int index) 
		{
			SpriteRenderer spriteRenderer = m_currentShape.GetComponent<SpriteRenderer> ();
			Vector2 currentPosition = m_currentShape.transform.position;

			// Set the old shape to inactive status
			m_currentShape.gameObject.layer = m_inactiveLayer;
			m_activeIndex = index;
			m_currentShape.Death -= playerDeathHandler;
			setSpriteAlpha (spriteRenderer, 0f);

			// Set the new current shape active
			m_currentShape = m_players [m_activeIndex];
			m_currentShape.gameObject.layer = m_activeLayer;
			m_currentShape.transform.position = currentPosition;
			m_currentShape.Death += playerDeathHandler;
			PlayerCamera.Target = m_currentShape.gameObject.transform;

			spriteRenderer = m_currentShape.GetComponent<SpriteRenderer> ();
			setSpriteAlpha (spriteRenderer, 1f);
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

				// Make all shapes completely transparent
				SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer> ();
				setSpriteAlpha (spriteRenderer, 0f);

				m_players.Add (player);
			}

			if (activeSet == false)
			{
				m_currentShape = m_players [m_activeIndex];

				// Make active shape opaque
				SpriteRenderer spriteRenderer = m_currentShape.GetComponent<SpriteRenderer> ();
				setSpriteAlpha (spriteRenderer, 1f);
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
		/// Raises the respawn player event.
		/// </summary>
		private void OnRespawnPlayer()
		{
			if (RespawnPlayer != null)
			{
				RespawnPlayer.Invoke ();
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

			HealthBar.transform.localScale = new Vector3 (scaledHealth * m_originalHealthBarXScale , y, z);
		}

		/// <summary>
		/// Set the sprite alpha.
		/// </summary>
		/// <param name="spriteRenderer">Sprite renderer component.</param>
		/// <param name="alpha">Alpha value.</param>
		private static void setSpriteAlpha(SpriteRenderer spriteRenderer, float alpha)
		{
			float r = spriteRenderer.color.r;
			float g = spriteRenderer.color.g;
			float b = spriteRenderer.color.b;

			spriteRenderer.color = new Color (r, g, b, alpha);
		}

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize the singleton instance.
		/// </summary>
		private void Awake()
		{
			if (m_playerManager != null && m_playerManager != this)
			{
				Destroy (this.gameObject);
			}
			else
			{
				m_playerManager = this;
			}
		}

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Start()
		{
			//Disable collision between player objects and set active player
			m_activeLayer = LayerMask.NameToLayer ("Active Player");
			m_inactiveLayer = LayerMask.NameToLayer ("Inactive Player");
			m_originalHealthBarXScale = HealthBar.transform.localScale.x;
			loadPlayers ();
			changePlayer (m_activeIndex);
		}

		#endregion
	}
}