using System;
using UnityEngine;

namespace ZigZag
{
	public abstract class Health : MonoBehaviour
	{
		public float FullHealth = 100f;

		protected float currentHealth;

		public abstract void ReceiveDamage(float damage);

		protected abstract void setHealth(float scaledDamage);
		protected abstract void die();
	}
}
