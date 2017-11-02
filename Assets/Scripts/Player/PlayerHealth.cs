using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ZigZag
{
	public class PlayerHealth : Health
	{
		public GameObject healthBar;
		public Image redPanel;
		private bool m_coroutineRunning;

		#region Unity Methods

		private void Awake()
		{
			currentHealth = FullHealth;
			m_coroutineRunning = false;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Receives the damage.
		/// </summary>
		/// <param name="damage">Damage amount.</param>
		public override void ReceiveDamage(float damage)
		{
			currentHealth -= damage;
			float scaledDamage;

			if (currentHealth <= 0f)
			{
				// TODO: Player dies and load Game Over UI
				scaledDamage = 0f;
			}
			else
			{
				scaledDamage = currentHealth / FullHealth;
			}

			// Only run 1 coroutine at a time
			if (!m_coroutineRunning)
			{
				StartCoroutine (flashScreenCoroutine ());
			}

			setHealth (scaledDamage);
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Set the player health on the Canvas.
		/// </summary>
		/// 
		/// <param name="scaledDamage">
		/// The amount of health to set to.
		/// </param>
		protected override void setHealth(float scaledDamage)
		{
			float y = healthBar.transform.localScale.y;
			float z = healthBar.transform.localScale.z;
			healthBar.transform.localScale = new Vector3 (scaledDamage, y, z);
		}

		protected override void die()
		{
			// TODO: Load the Game Over UI or scene
		}

		/// <summary>
		/// Flash the screen red when player is damaged
		/// </summary>
		/// 
		/// <returns>The coroutine.</returns>
		private IEnumerator flashScreenCoroutine()
		{
			m_coroutineRunning = true;
			redPanel.enabled = true;

			yield return new WaitForSeconds (0.09f);

			redPanel.enabled = false;
			m_coroutineRunning = false;
		}

		#endregion
	}
}