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

		#endregion

		#region Properties

		#endregion

		#region Public Methods

		#endregion

		#region Private/Protected Methods
		protected override void die ()
		{
			base.die ();

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

		/// <summary>
		/// Raises the trigger enter 2D event.
		/// </summary>
		/// <param name="collider">Collider.</param>
		private void OnTriggerEnter2D(Collider2D collider)
		{
			int activePlayerLayer = LayerMask.NameToLayer ("Active Player");

			// The active player dies when he falls into a pitfall
			if (gameObject.layer == activePlayerLayer && collider.CompareTag ("PitFall"))
			{
				this.die ();
			}
		}

		#endregion
	}
}