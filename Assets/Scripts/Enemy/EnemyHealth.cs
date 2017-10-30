using UnityEngine;
using System.Collections;

namespace ZigZag
{
	public class EnemyHealth : Health
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

			if (currentHealth <= 0f)
			{
				Die ();
			}
			else
			{
				float scaledDamage = currentHealth / fullHealth;
				SetHealth (scaledDamage);
			}
		}

		protected override void Die()
		{
			Destroy (gameObject);
		}
	}
}
