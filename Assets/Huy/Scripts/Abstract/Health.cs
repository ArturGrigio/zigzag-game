using System;
using UnityEngine;

namespace Huy
{
	public abstract class Health : MonoBehaviour
	{
		#region Public Variables
		/// <summary>
		/// Maximum health the object can attain.
		/// </summary>
		[Tooltip("Maximum health the object can attain.")]
        public float FullHealth = 100;

		#endregion

		#region Private/Protected Variables
		protected float m_currentHealth;
		#endregion

		#region Properties
		public float CurrentHealth
		{
			get { return m_currentHealth; }
		}
		#endregion

		#region Public Methods

		public virtual void ReceiveDamage(float damage)
		{
			m_currentHealth -= damage;
			if (m_currentHealth <= 0f)
			{
				die();
			}
		}

        public virtual void ReceiveHeal(float heal)
		{
			m_currentHealth += heal;
			if (m_currentHealth >= FullHealth)
			{
				m_currentHealth = FullHealth;
			}
		}

		#endregion

		#region Private/Protected Methods

		protected virtual void die() 
		{
			Destroy (gameObject);
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
