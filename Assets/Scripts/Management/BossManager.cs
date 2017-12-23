using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	/// <summary>
	/// This class manages the boss and anything that relates to the boss event.
	/// </summary>
	public class BossManager : MonoBehaviour
	{
		#region Public Variables

		/// <summary>
		/// The boss prefab.
		/// </summary>
		[Tooltip("The boss prefab")]
		public GameObject BossPrefab;

		/// <summary>
		/// The door in which will be used to lock the player inside the boss room.
		/// </summary>
		[Tooltip("The door in which will be used to lock the player inside the boss room")]
		public GameObject BossDoor;

		/// <summary>
		/// The boss trigger.
		/// </summary>
		[Tooltip("The boss trigger")]
		public BossTrigger bossTrigger;

		/// <summary>
		/// The health bar.
		/// </summary>
		[Tooltip("The health bar")]
		public GameObject HealthBar;

		#endregion

		#region Private/Protected Variables

		/// <summary>
		/// The boss manager singleton instance.
		/// </summary>
		private static BossManager m_bossManager = null;

		/// <summary>
		/// The player manager.
		/// </summary>
		private PlayerManager m_playerManager;

		/// <summary>
		/// Reference to the current boss.
		/// </summary>
		private Boss m_currentBoss;

		/// <summary>
		/// Indicate whether it's possible to spawn a boss.
		/// </summary>
		private bool m_allowSpawn;

		/// <summary>
		/// The original x scale of the health bar.
		/// </summary>
		private float m_originalHealthBarXScale;

		/// <summary>
		/// The blue health bar.
		/// </summary>
		private GameObject m_healthBarBlue;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the singleton instance.
		/// </summary>
		public static BossManager Instance
		{
			get { return m_bossManager; }
		}

		/// <summary>
		/// Gets a reference to the current boss.
		/// </summary>
		public Boss CurrentBoss
		{
			get { return m_currentBoss; }
		}

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize the singleton instance.
		/// </summary>
		private void Awake()
		{
			if (m_bossManager != null && m_bossManager != this)
			{
				Destroy (this.gameObject);
			}
			else
			{
				m_bossManager = this;
			}
		}

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Start()
		{
			m_allowSpawn = true;
			m_playerManager = PlayerManager.Instance;
			m_healthBarBlue = HealthBar.transform.GetChild (2).gameObject;
			m_originalHealthBarXScale = m_healthBarBlue.transform.localScale.x;

			bossTrigger.BeforeBoss += beforeBossHandler;
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Display the boss health.
		/// </summary>
		private void displayBossHealth()
		{
			float scaledHealth = (m_currentBoss.CurrentHealth / m_currentBoss.FullHealth);

			float y = m_healthBarBlue.transform.localScale.y;
			float z = m_healthBarBlue.transform.localScale.z;

			m_healthBarBlue.transform.localScale = new Vector3 (scaledHealth * m_originalHealthBarXScale , y, z);
		}

		/// <summary>
		/// Spawn a boss. Only 1 boss is active at a time.
		/// </summary>
		private void spawnBoss()
		{
			m_currentBoss = Instantiate (BossPrefab).GetComponent<Boss> ();
			m_currentBoss.Death += currentBossDeathHandler;
			m_currentBoss.HealthDisplay += healthDisplayHandler;
			m_allowSpawn = false;
		}

		/// <summary>
		/// Handle the event when the boss's health changes.
		/// </summary>
		/// <param name="healthStatus">Health status.</param>
		private void healthDisplayHandler(HealthStatus healthStatus)
		{
			switch (healthStatus)
			{
				case HealthStatus.Damage:
					displayBossHealth ();
					break;

				case HealthStatus.Heal:
					break;

				default:
					break;
			}
		}

		/// <summary>
		/// Reset the blue health bar.
		/// </summary>
		private void resetHealthBarBlue()
		{
			float x = m_originalHealthBarXScale;
			float y = m_healthBarBlue.transform.localScale.y;
			float z = m_healthBarBlue.transform.localScale.z;

			m_healthBarBlue.transform.localScale = new Vector3 (x, y, z);
		}

		/// <summary>
		/// Handle the event when the player is respawned.
		/// </summary>
		private void respawnHandler()
		{
			m_allowSpawn = true;
		}

		/// <summary>
		/// Handle the event when the player dies.
		/// </summary>
		private void playerDeathHandler ()
		{
			// Unregister these events
			m_currentBoss.Death -= currentBossDeathHandler;
			m_currentBoss.HealthDisplay -= healthDisplayHandler;
			m_playerManager.PlayerDeath -= playerDeathHandler;
			m_playerManager.RespawnPlayer -= respawnHandler;

			// Destroy the current boss and its arrows
			m_currentBoss.RemoveArrowObjects ();
			Destroy (m_currentBoss.gameObject);

			HealthBar.SetActive (false);
			BossDoor.SetActive (false);

			resetHealthBarBlue ();
			m_currentBoss = null;
			m_allowSpawn = true;
		}

		/// <summary>
		/// Handle the event when the player triggers the boss event.
		/// </summary>
		private void beforeBossHandler()
		{
			if (m_allowSpawn)
			{
				// Register these events when player is about to face the boss
				m_playerManager.PlayerDeath += playerDeathHandler;
				m_playerManager.RespawnPlayer += respawnHandler;

				spawnBoss ();

				HealthBar.SetActive (true);
				BossDoor.SetActive (true);
				AudioManager.Instance.PlayMusic ("Boss");
			}
		}

		/// <summary>
		/// Handle the event when the current boss dies.
		/// </summary>
		private void currentBossDeathHandler()
		{
			// Unregister these events when the boss dies
			m_currentBoss.Death -= currentBossDeathHandler;
			m_currentBoss.HealthDisplay -= healthDisplayHandler;
			m_playerManager.PlayerDeath -= playerDeathHandler;
			m_playerManager.RespawnPlayer -= respawnHandler;
			bossTrigger.BeforeBoss -= beforeBossHandler;

			m_currentBoss = null;
			m_allowSpawn = false;

			HealthBar.SetActive (false);
			BossDoor.SetActive (false);
			AudioManager.Instance.PlayMusic ("Main");
		}

		#endregion
	}
}