using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
	public class CameraController : MonoBehaviour {
		private Vector3 m_playerOffset;
		public PlayerManager m_playerManager;
		// Use this for initialization
		void Start () 
		{
			m_playerOffset = transform.position - m_playerManager.CurrentShape.transform.position;
		}
		
		// Update is called once per frame
		void LateUpdate () 
		{
			transform.position = m_playerManager.CurrentShape.transform.position + m_playerOffset;
		}
	}

}