using UnityEngine;
using System.Collections;

public class JumpAttack : MonoBehaviour
{
	public Collider2D bottomCollider;

	private void OnCollisionEnter2D(Collision2D colliedObject)
	{
		if (colliedObject.otherCollider == bottomCollider)
		{
			Health health = colliedObject.gameObject.GetComponent<Health> ();

			if (health != null)
			{
				health.ReceiveDamage (1f);
			}
		}	
	}
}

