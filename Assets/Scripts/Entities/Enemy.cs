using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	public class Enemy : Agent
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
			Destroy (gameObject);
		}	
		#endregion

		#region Unity Methods


		#endregion
	}
}