using UnityEngine;
using System.Collections;

namespace Huy
{
	public class EnemyHealth : Health
	{

		#region Private/Protected Methods
		protected override void setHealth(float scaledDamage)
		{
		}

		public override void ReceiveDamage(float damage)
		{
			currentHealth -= damage;

			if (currentHealth <= 0f)
			{
				die ();
			}
			else
			{
				float scaledDamage = currentHealth / FullHealth;
				setHealth (scaledDamage);
			}
		}

		protected override void die()
		{
			Destroy (gameObject);
		}

		#endregion

		#region Unity Methods

		private void Awake()
		{
			currentHealth = FullHealth;
		}

		#endregion

	}
}
