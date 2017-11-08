using System;
using UnityEngine;

namespace ZigZag
{
	public abstract class Health : MonoBehaviour
	{
		#region Public Variables
		[Tooltip("Maximum health the object can attain.")]
		public int FullHealth = 100;
		#endregion

		#region Private/Protected Variables
		protected int m_currentHealth;
		#endregion

		#region Properties
		public int CurrentHealth
		{
			get { return m_currentHealth; }
		}
		#endregion

		#region Public Methods
		public virtual void ReceiveDamage(int damage)
		{
			m_currentHealth -= damage;
			if (m_currentHealth < 0)
			{
				die ();
			}
		}

		public virtual void ReceiveHeal(int heal)
		{
			m_currentHealth += heal;
			if (m_currentHealth > FullHealth)
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
