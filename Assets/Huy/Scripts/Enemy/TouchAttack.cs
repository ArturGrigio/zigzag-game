using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Huy
{
	public class TouchAttack : MonoBehaviour
	{
		private Enemy m_enemy;

		// Use this for initialization
		private void Awake ()
		{
			m_enemy = GetComponent<Enemy> ();
		}
		
		// Update is called once per frame
		private void Update ()
		{
			// Constantly deal damage to the player if enemy and player are touching each other
			if (m_enemy.TouchTarget)
			{
				Transform parentTransform = m_enemy.TargetTransform.parent;

				if (parentTransform != null)
				{
					GameObject parentObject = parentTransform.gameObject;
					PlayerManager playerManager = parentObject.GetComponent<PlayerManager> ();

					if (playerManager != null)
					{
						playerManager.ReceiveDamage (0.01f);
					}
				}
			}
		}


	}
}

