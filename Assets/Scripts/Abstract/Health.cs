using System;
using UnityEngine;

namespace ZigZag
{
	public enum HealthStatus {Heal, Damage}

	public abstract class Health : MonoBehaviour
	{
		#region Public Variables

		[Tooltip("Maximum health the object can attain.")]
		public float FullHealth = 100;

		/// <summary>
		/// Delegate handler for handling the death of an agent.
		/// </summary>
		public delegate void DeathHandler ();

		public delegate void HealthDisplayHandler(HealthStatus healthStatus);
		public event HealthDisplayHandler HealthDisplay;

		/// <summary>
		/// Occurs when an agent dies.
		/// </summary>
		public event DeathHandler Death;

		#endregion

		#region Private/Protected Variables
		protected float m_currentHealth;
		#endregion

		#region Properties
		public float CurrentHealth
		{
			get { return m_currentHealth; }
		}

		public bool IsInvulnerable { get; set;}

		#endregion

		#region Public Methods
		/// <summary>
		/// Deals damage to the agent and checks for death.
		/// </summary>
		/// <param name="damage">Damage.</param>
		public virtual void ReceiveDamage(float damage)
		{
			m_currentHealth -= damage;
			if (m_currentHealth <= 0)
			{
				m_currentHealth = 0f;
				die ();
			}

			OnHealthDisplay (HealthStatus.Damage);
		}

		/// <summary>
		/// Removes damage from the agent.
		/// </summary>
		/// <param name="heal">Heal.</param>
		public virtual void ReceiveHeal(float heal)
		{
			m_currentHealth += heal;
			if (m_currentHealth >= FullHealth)
			{
				m_currentHealth = FullHealth;
			}

			OnHealthDisplay (HealthStatus.Heal);
		}
		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Performs required actions when the agent dies.
		/// </summary>
		protected virtual void die() 
		{
			//Destroy (gameObject);
			OnDeath();
		}

		/// <summary>
		/// Raises the death event.
		/// </summary>
		protected virtual void OnDeath()
		{
			if (Death != null)
			{
				Death.Invoke ();
			}
		}

		/// <summary>
		/// Raises the health display event.
		/// </summary>
		protected virtual void OnHealthDisplay(HealthStatus healthStatus)
		{
			if (HealthDisplay != null)
			{
				HealthDisplay.Invoke (healthStatus);
			}
		}

		#endregion

		#region Unity Methods
		protected virtual void Awake()
		{
			m_currentHealth = FullHealth;
		}
		#endregion
	}
}
