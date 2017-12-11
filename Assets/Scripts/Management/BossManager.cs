using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	public class BossManager : MonoBehaviour
	{
		public GameObject BossPrefab;
		public BossTrigger bossTrigger;
		public event Health.DeathHandler BossDeath;

		private static BossManager m_bossManager = null;
		private PlayerManager m_playerManager;
		private Boss currentBoss;

		public static BossManager Instance
		{
			get { return m_bossManager; }
		}

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

		private void Start()
		{
			m_playerManager = PlayerManager.Instance;

			m_playerManager.PlayerDeath += playerDeathHandler;
			m_playerManager.RespawnPlayer += respawnHandler;
			bossTrigger.BeforeBoss += beforeBossHandler;
		}

		private void OnBossDeath()
		{
			if (BossDeath != null)
			{
				BossDeath.Invoke ();
			}
		}

		private void respawnHandler()
		{
			bossTrigger.BeforeBoss += beforeBossHandler;
		}

		private void playerDeathHandler ()
		{
			if (currentBoss != null)
			{
				Destroy (currentBoss.gameObject);
				bossTrigger.BossDoor.SetActive (false);
			}
		}

		private void beforeBossHandler()
		{
			currentBoss = Instantiate (BossPrefab).GetComponent<Boss> ();
			currentBoss.Death += bossDeathHandler;
			bossTrigger.BeforeBoss -= beforeBossHandler;
		}

		private void bossDeathHandler()
		{
			bossTrigger.BossDoor.SetActive (false);
			bossTrigger.IsBossDeath = true;
			OnBossDeath ();

			if (currentBoss.gameObject != null)
			{
				Destroy (currentBoss.gameObject);
			}
		}
	}
}