using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ZigZag
{
	public class TouchAttack : Skill
	{
		public float attackDamage = 1f;
		public Image redPanel;
		private bool m_touchPlayer;
		private static bool m_coroutineRunning;
		private Player m_activeShape;

		protected override void Awake ()
		{
			base.Awake ();
			m_touchPlayer = false;
			m_activeShape = null;
		}

		private void Update()
		{
			if (m_touchPlayer && m_activeShape != null && 
				m_activeShape.ActiveSkill == null && !m_activeShape.Invicible &&
				m_activeShape.CurrentHealth > 0f)
			{
				m_activeShape.ReceiveDamage (attackDamage);

				if (!m_coroutineRunning)
				{
					StartCoroutine (flashScreenCoroutine ());
				}
			}
		}

		/// <summary>
		/// Flash the screen red when player is damaged
		/// </summary>
		/// 
		/// <returns>The coroutine.</returns>
		private IEnumerator flashScreenCoroutine()
		{
			m_coroutineRunning = true;
			redPanel.enabled = true;

			yield return new WaitForSeconds (0.099f);

			redPanel.enabled = false;
			m_coroutineRunning = false;
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

