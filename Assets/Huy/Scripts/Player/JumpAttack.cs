using UnityEngine;
using System.Collections;

namespace ZigZag
{
	public class JumpAttack : MonoBehaviour
	{
		private EdgeCollider2D m_edgeCollider2D;

		/// <summary>
		/// 
		/// </summary>
		private void Awake()
		{
			m_edgeCollider2D = GetComponent<EdgeCollider2D> ();
		}

		/// <summary>
		/// Raises the collision enter2 d event.
		/// </summary>
		/// 
		/// <param name="collision">Collision.</param>
		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.otherCollider == m_edgeCollider2D)
			{
				Health health = collision.gameObject.GetComponent<Health> ();

				if (health != null)
				{
					Debug.Log (collision.collider + " " + collision.otherCollider);
					health.ReceiveDamage (1f);
				}
			}
		}
	}
}
