using UnityEngine;
using System.Collections;

namespace ZigZag
{
	public class PlayerHealth : Health
	{
		private void Awake()
		{
			currentHealth = fullHealth;
		}

		protected override void SetHealth(float scaledDamage)
		{
		}

		public override void ReceiveDamage(float damage)
		{
			currentHealth -= damage;
			float scaledDamage = currentHealth / fullHealth;

			SetHealth (scaledDamage);
		}

		protected override void Die()
		{
			// Load the Game Over UI or scene
		}
	}
}