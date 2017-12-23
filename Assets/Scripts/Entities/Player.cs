using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	public class Player : Agent
	{
		#region Public Variables

		#endregion

		#region Private/Protected Variables

		private Vector2 m_initialPosition;

		#endregion

		#region Properties

		public Vector2 InitialPosition
		{
			get { return m_initialPosition; }
		}

		#endregion

		#region Public Methods

		public override void ReceiveDamage (float damage)
		{
			if (!m_invicible && m_currentHealth > 0f)
			{
				base.ReceiveDamage (damage);
			}
		}

		#endregion

		#region Private/Protected Methods
		protected override void die ()
		{
			base.die ();
			m_rigidBody2D.velocity = Vector2.zero;

			Skill[] skills = GetComponents<Skill> ();
			foreach (Skill skill in skills)
			{
				// Cancel any active skill when the player dies
				if (skill.IsActive)
				{
					skill.Cancel ();
				}
			}
		}	
		#endregion

		#region Unity Methods

		protected override void Awake ()
		{
			base.Awake ();
			m_initialPosition = transform.position;
		}

		/// <summary>
		/// Raises the trigger enter 2D event.
		/// </summary>
		/// <param name="collider">Collider.</param>
		private void OnTriggerEnter2D(Collider2D collider)
		{
			int activePlayerLayer = LayerMask.NameToLayer ("Active Player");
			AcquireShapeTrigger shapeTrigger = collider.GetComponent<AcquireShapeTrigger> ();

			// The active player dies when he falls into a pitfall
			if (gameObject.layer == activePlayerLayer && collider.CompareTag ("PitFall"))
			{
				this.die ();
			}
			else if (gameObject.layer == activePlayerLayer && shapeTrigger != null)
			{
				Player otherShape = shapeTrigger.Shape;
				if (!PlayerManager.Instance.Players.Contains (otherShape))
				{
					PlayerManager.Instance.AcquireShape (otherShape);
				}
			}
		}



		#endregion
	}
}