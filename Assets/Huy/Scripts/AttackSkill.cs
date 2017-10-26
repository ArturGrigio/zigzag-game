using UnityEngine;
using System.Collections;

namespace ZigZag
{
	public abstract class AttackSkill : MonoBehaviour, ISkill
	{
		public float attackDamage = 1f;
		public string attackName = "attack";
		protected bool m_isActive;
		protected bool m_isEnabled;

		public bool IsActive 
		{
			get { return m_isActive; }
		}

		public bool IsEnabled 
		{
			get { return m_isEnabled; }
			set { m_isEnabled = value; }
		}

		public abstract void Activate();
		public abstract void Deactivate();
	}
}
