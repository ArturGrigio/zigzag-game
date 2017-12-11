using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	public class Boss : Agent
	{
		public GameObject ArrowPrefab;
		private bool m_coroutineRunning;

		private void fireArrows()
		{
			GameObject arrow = Instantiate (ArrowPrefab);
			arrow.transform.position = transform.position;
		}

		public override void ReceiveDamage (float damage)
		{
			base.ReceiveDamage (damage);

			if (!m_coroutineRunning)
			{
				StartCoroutine (flashDamage ());
			}
		}

		private IEnumerator flashDamage()
		{
			m_coroutineRunning = true;

			PlayerManager.SetSpriteAlpha (m_spriteRenderer, 0.1f);
			yield return new WaitForSeconds (0.099f);
			PlayerManager.SetSpriteAlpha (m_spriteRenderer, 1f);

			m_coroutineRunning = false;
		}

		protected override void die ()
		{
			OnDeath ();
			//Destroy (gameObject);
		}


		private void Start()
		{
			m_coroutineRunning = false;
			m_spriteRenderer = GetComponent<SpriteRenderer> ();
			InvokeRepeating ("fireArrows", 3f, 4f);
		}


	}
}