using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ZigZag
{
	public class TouchAttack : Skill
	{
		public float attackDamage = 1f;

		private bool m_touchPlayer;
		private Player m_activeShape;

		protected override void Awake ()
		{
			base.Awake ();
			m_touchPlayer = false;
			m_activeShape = null;
		}

		private void Update()
		{
			// Only deal damage if enemy is touching player and if player does not have any active skill
			if (m_touchPlayer && m_activeShape != null && m_activeShape.ActiveSkill == null)
			{
				m_activeShape.ReceiveDamage (attackDamage);
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			int activePlayerLayer = LayerMask.NameToLayer ("Active Player");

			if (collision.collider.gameObject.layer == activePlayerLayer)
			{
				m_touchPlayer = true;
				m_activeShape = collision.collider.GetComponent<Player> ();
			}
		}

		private void OnCollisionExit2D(Collision2D collision)
		{
			int activePlayerLayer = LayerMask.NameToLayer ("Active Player");

			if (collision.collider.gameObject.layer == activePlayerLayer)
			{
				m_touchPlayer = false;
				m_activeShape = null;
			}
		}

	}
}

