using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
	public float fullHealth = 100f;
	protected float currentHealth;

	protected abstract void SetHealth(float scaledDamage);
	public abstract void ReceiveDamage(float damage);
	protected abstract void Die();
}

