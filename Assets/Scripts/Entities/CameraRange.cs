using UnityEngine;
using System.Collections;

namespace ZigZag
{
	/// <summary>
	/// This class holds the needed minimum x position of the camera and swaps this value
	/// with the current camera minimum x position depending on the location in the level.
	/// </summary>
	public class CameraRange : MonoBehaviour
	{
		public float CameraMinY = -4.6f;
		private Camera2DFollow m_cameraFollow2D;

		// Use this for initialization
		void Start ()
		{
			m_cameraFollow2D = Camera.main.GetComponent<Camera2DFollow> ();
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			int activePlayerLayer = LayerMask.NameToLayer ("Active Player");

			if (collider.gameObject.layer == activePlayerLayer)
			{
				float temp = m_cameraFollow2D.MinY;
				m_cameraFollow2D.MinY = CameraMinY;
				CameraMinY = temp;
			}
		}
	}
}
