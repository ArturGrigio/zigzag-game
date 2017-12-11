using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZigZag
{
	public class Bomb : MonoBehaviour 
	{
		public float AttackDamage = 1f;

		private void OnCollisionEnter2D(Collision2D collision)
		{
			int activePlayerLayer = LayerMask.NameToLayer ("Active Player");

			if (collision.collider.gameObject.layer == activePlayerLayer)
			{
				Player currentShape = collision.collider.GetComponent<Player> ();
				currentShape.ReceiveDamage (AttackDamage);
			}

			Destroy (gameObject, 5f);
		}
	}
}