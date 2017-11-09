﻿using System;
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
		/// <summary>
		/// Deals damage to the agent and checks for death.
		/// </summary>
		/// <param name="damage">Damage.</param>
		public virtual void ReceiveDamage(int damage)
		{
			m_currentHealth -= damage;
			if (m_currentHealth < 0)
			{
				die ();
			}
		}

		/// <summary>
		/// Removes damage from the agent.
		/// </summary>
		/// <param name="heal">Heal.</param>
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
		/// <summary>
		/// Performs required actions when the agent dies.
		/// </summary>
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
