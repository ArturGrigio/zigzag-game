using UnityEngine;
using System.Collections;

namespace ZigZag
{
	public class PlayerHealth : Health
	{
		public GameObject healthBar;

		#region Unity Methods

		private void Awake()
		{
			currentHealth = FullHealth;
		}

		private void Update()
		{
			if (Input.GetKeyDown (KeyCode.A))
			{
				ReceiveDamage (10f);
			}
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

			setHealth (scaledDamage);
		}

		#endregion

		#region Private/Protected Methods

		protected override void setHealth(float scaledDamage)
		{
			float y = healthBar.transform.localScale.y;
			float z = healthBar.transform.localScale.z;
			healthBar.transform.localScale = new Vector3 (scaledDamage, y, z);
		}

		protected override void die()
		{
			// Load the Game Over UI or scene
		}

		#endregion
	}
}