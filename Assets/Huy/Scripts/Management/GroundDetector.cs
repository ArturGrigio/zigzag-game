using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Huy
{
	[RequireComponent(typeof(Collider2D))]
	public class GroundDetector : MonoBehaviour 
	{

		#region Public Variables
		public delegate void GroundEnterHandler(Collider2D collider);
		public delegate void GroundExitHandler(Collider2D collider);

		public event GroundEnterHandler OnGroundEnter;
		public event GroundExitHandler OnGroundExit;
		#endregion

		#region Private/Protected Variables
		#endregion

		#region Properties
		#endregion

		#region Public Methods
		#endregion

		#region Private/Protected Methods
		#endregion

		#region Unity Methods
		void OnTriggerEnter2D(Collider2D collider)
		{
			//Debug.Log ("Ground Enter");
			OnGroundEnter.Invoke (collider);
		}

		void OnTriggerExit2D(Collider2D collider)
		{
			//Debug.Log ("Ground Exit");
			OnGroundExit.Invoke (collider);
		}
		#endregion
	}
}
