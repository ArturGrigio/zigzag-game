using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huy
{
	public class CameraController : MonoBehaviour {

		#region Public Variables
		[Tooltip("Determines which player object to follow.")]
		public PlayerManager PlayerManager;

		#endregion

		#region Private/Protected Variables

		private Vector3 m_playerOffset;

		#endregion

		#region Unity Methiods

		// Use this for initialization
		void Start () 
		{
			m_playerOffset = transform.position - PlayerManager.CurrentShape.transform.position;
		}
		
		// Update is called once per frame
		void LateUpdate () 
		{
			transform.position = PlayerManager.CurrentShape.transform.position + m_playerOffset;
		}

		#endregion
	}

}