using UnityEngine;
using System.Collections;

namespace ZigZag
{
	public class PlayerHealth : Health
	{
		#region Unity Methods

		private void Awake()
		{
			currentHealth = FullHealth;
		}

		#endregion

		#region Private/Protected Methods

		protected override void setHealth(float scaledDamage)
		{
		}

		public override void ReceiveDamage(float damage)
		{
			currentHealth -= damage;
			float scaledDamage = currentHealth / FullHealth;

			setHealth (scaledDamage);
		}

		protected override void die()
		{
			// Load the Game Over UI or scene
		}

		#endregion
	}
}