using UnityEngine;
using System.Collections;

namespace ZigZag
{
	/// <summary>
	/// This class handles the camera movement.
	/// </summary>
	public class Camera2DFollow : MonoBehaviour
	{
		#region Public Variables

		/// <summary>
		/// The target for the camera to follow.
		/// </summary>
		[Tooltip("The target for the camera to follow")]
		public Transform Target;

		/// <summary>
		/// The time to make the camera catch up to the target after the target has moved.
		/// </summary>
		[Tooltip("The time to make the camera catch up to the target after the target has moved")]
		public float SmoothTime = 0.3f;

		/// <summary>
		/// The offset determines the position of the camera relative to the target.
		/// </summary>
		[Tooltip("The offset determines the position of the camera relative to the target")]
		public Vector3 Offset = new Vector3(0f, 0f, -10f);

		/// <summary>
		/// The minimum x position the camera can have.
		/// </summary>
		[Tooltip("The minimum x position the camera can have")]
		public float MinX;

		/// <summary>
		/// The maximum x position the camera can have.
		/// </summary>
		[Tooltip("The maximum x position the camera can have")]
		public float MaxX;

		/// <summary>
		/// The minimum y position the camera can have.
		/// </summary>
		[Tooltip("The minimum y position the camera can have")]
		public float MinY;

		/// <summary>
		/// The maximum y position the camera can have.
		/// </summary>
		[Tooltip("The maximum y position the camera can have")]
		public float MaxY;

		#endregion

		#region Private Variables

		/// <summary>
		/// The current velocity.
		/// </summary>
		private Vector3 currentVelocity;

		#endregion

		#region Unity Methods

		// Use this for initialization
		private void Awake ()
		{
			currentVelocity = Vector3.zero;
		}

		// Update is called once per frame
		private void Update ()
		{
			Vector3 targetPosition = Target.TransformPoint (Offset);
			Vector3 desiredPosition = Vector3.SmoothDamp (transform.position, targetPosition, ref currentVelocity, SmoothTime);

			float clampX = Mathf.Clamp (desiredPosition.x, MinX, MaxX);
			float clampY = Mathf.Clamp (desiredPosition.y, MinY, MaxY);
			transform.position = new Vector3 (clampX, clampY, desiredPosition.z);
		}

		#endregion
	}
}
