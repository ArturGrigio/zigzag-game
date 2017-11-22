using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	public class Attacker : MonoBehaviour 
	{
		#region Public Variables
		[Tooltip("Amount of damage dealt by attack when not modified by a skill.")]
		public float DefaultAttackDamage = 0f;
		#endregion

		#region Private/Protected Variables
		#endregion

		#region Properties
		public float AttackDamage { get; set; }
		public bool IsContinuous = false;
		#endregion

		#region Public Methods

		#endregion

		#region Private/Protected Methods
		protected virtual bool dealDamage(Collision2D collision)
		{
			if (AttackDamage > 0f)
			{
				Health recipient = collision.gameObject.GetComponent<Health> ();
				if (recipient != null && recipient.IsInvulnerable == false)
				{
					Debug.Log (gameObject.name + " dealt damage to " + collision.gameObject.name);
					recipient.ReceiveDamage (AttackDamage);
					return true;
				}
			}
			return false;
		}
		#endregion

		#region Unity Methods

		protected virtual void Awake()
		{
			AttackDamage = DefaultAttackDamage;
		}

		protected virtual void OnCollisionEnter2D(Collision2D collision)
		{
			dealDamage (collision);
		}

		protected virtual void OnCollisionStay2D(Collision2D collision)
		{
			if (IsContinuous == true)
			{
				dealDamage (collision);
			}
		}
		#endregion
	}
}
