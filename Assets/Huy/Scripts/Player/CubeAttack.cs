using UnityEngine;
using System.Collections;

namespace ZigZag
{
	public class CubeAttack : AttackSkill
	{
		private Rigidbody2D m_rigidBody2D;

		public override void Activate()
		{
			m_isActive = true;
		}

		public override void Deactivate()
		{
			m_isActive = false;
		}

		private void Awake()
		{
			m_isActive = false;
			m_isEnabled = false;
			m_rigidBody2D = GetComponent<Rigidbody2D> ();
		}
	}
}
