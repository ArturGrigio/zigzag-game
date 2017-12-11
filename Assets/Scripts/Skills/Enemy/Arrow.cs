using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	public class Arrow : MonoBehaviour 
	{
		public float AttackDamage = 1f;
		public float Speed = 5f;
		public float RotatingSpeed = 200f;

		private Rigidbody2D m_rigidbody2D;
		private PlayerManager m_playerManager;
		private BossManager m_bossManager;

		private void deathHandler()
		{
			if (gameObject != null)
			{
				Destroy (gameObject);
				m_playerManager.PlayerDeath -= deathHandler;
				m_bossManager.BossDeath -= deathHandler;
			}
		}

		private void Start()
		{
			m_rigidbody2D = GetComponent<Rigidbody2D> ();
			m_playerManager = PlayerManager.Instance;
			m_bossManager = BossManager.Instance;

			m_playerManager.PlayerDeath += deathHandler;
			m_bossManager.BossDeath += deathHandler;
		}

		private void FixedUpdate()
		{
			Vector2 pointToTarget = (Vector2)transform.position - (Vector2)m_playerManager.CurrentShape.transform.position;
			pointToTarget.Normalize ();

			float value = Vector3.Cross (pointToTarget, transform.right).z;
			 
			m_rigidbody2D.angularVelocity = RotatingSpeed * value;
			m_rigidbody2D.velocity = transform.right * Speed;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			int activePlayerLayer = LayerMask.NameToLayer ("Active Player");

			if (collision.collider.gameObject.layer == activePlayerLayer)
			{
				Player currentShape = collision.collider.GetComponent<Player> ();
				currentShape.ReceiveDamage (AttackDamage);
			}

			Destroy (gameObject);
			m_playerManager.PlayerDeath -= deathHandler;
			m_bossManager.BossDeath -= deathHandler;
		}
	}
}
