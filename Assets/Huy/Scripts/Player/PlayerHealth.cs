using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Huy
{
	/// <summary>
	/// Represent the player health and handle receiving damage.
	/// This class is shared among all objects.
	/// </summary>
	public class PlayerHealth : Health
	{
		#region Public Variables

		/// <summary>
		/// Reference to the green health bar.
		/// </summary>
		[Tooltip("Reference to the green health bar")]
		public GameObject HealthBar;

		/// <summary>
		/// The red panel used for when player is damaged.
		/// </summary>
		[Tooltip("The red panel used for when player is damaged")]
		public Image RedPanel;

		#endregion

		#region Private Variables

		/// <summary>
		/// Flag indicating whether the flashing red coroutine is running or not.
		/// </summary>
		private bool m_flashingRedCoroutineRunning;

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Awake()
		{
			currentHealth = FullHealth;
			m_flashingRedCoroutineRunning = false;
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
				// Only run 1 coroutine at a time
				if (!m_flashingRedCoroutineRunning)
				{
					StartCoroutine (flashScreenCoroutine ());
				}

				scaledDamage = currentHealth / FullHealth;
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
			float y = HealthBar.transform.localScale.y;
			float z = HealthBar.transform.localScale.z;
			HealthBar.transform.localScale = new Vector3 (scaledDamage, y, z);
		}

		protected override void die()
		{
			// TODO: Load the Game Over UI or scene
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Flash the screen red when player is damaged
		/// </summary>
		/// 
		/// <returns>The coroutine.</returns>
		private IEnumerator flashScreenCoroutine()
		{
			m_flashingRedCoroutineRunning = true;
			RedPanel.enabled = true;

			yield return new WaitForSeconds (0.09f);

			RedPanel.enabled = false;
			m_flashingRedCoroutineRunning = false;
		}

		#endregion
	}
}