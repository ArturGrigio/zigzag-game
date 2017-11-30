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
			OnDeath ();
		}	
		#endregion

		#region Unity Methods

		/// <summary>
		/// Raises the trigger enter 2D event.
		/// </summary>
		/// <param name="collider">Collider.</param>
		private void OnTriggerEnter2D(Collider2D collider)
		{
			// The player dies when he falls into a pitfall
			if (collider.CompareTag ("PitFall"))
			{
				this.die ();
			}
		}

		#endregion
	}
}