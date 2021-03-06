﻿using UnityEngine;
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

		/// <summary>
		/// The camera range 1.
		/// </summary>
		[Tooltip("The first CamerRange object in the level")]
		public CameraRange cameraRange1;

		/// <summary>
		/// The camera range 2.
		/// </summary>
		[Tooltip("The second CamerRange object in the level")]
		public CameraRange cameraRange2;

		#endregion

		#region Private Variables

		/// <summary>
		/// The current velocity.
		/// </summary>
		private Vector3 currentVelocity;

		/// <summary>
		/// The default width of the camera view.
		/// </summary>
		private float defaultWidth;

		/// <summary>
		/// The player manager.
		/// </summary>
		private PlayerManager m_playerManager;

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Handle the player respawn event.
		/// Load the saved boundary values.
		/// </summary>
		private void respawnPlayerHandler()
		{
			MinX = SavePoint.SavedCameraBoundary [0];
			MaxX = SavePoint.SavedCameraBoundary [1];
			MinY = SavePoint.SavedCameraBoundary [2];
			MaxY = SavePoint.SavedCameraBoundary [3];

			cameraRange1.CameraMinY = SavePoint.SavedCameraRangeValue1;
			cameraRange2.CameraMinY = SavePoint.SavedCameraRangeValue2;
		}

		#endregion

		#region Unity Methods

		// Use this for initialization
		private void Start ()
		{
			m_playerManager = PlayerManager.Instance;
			m_playerManager.RespawnPlayer += respawnPlayerHandler;

			currentVelocity = Vector3.zero;
			defaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
		}

		// Update is called once per frame
		private void Update ()
		{
			Camera.main.orthographicSize = defaultWidth / Camera.main.aspect;

			Vector3 targetPosition = Target.position + Offset;
			Vector3 desiredPosition = Vector3.SmoothDamp (transform.position, targetPosition, ref currentVelocity, SmoothTime);

			float clampX = Mathf.Clamp (desiredPosition.x, MinX, MaxX);
			float clampY = Mathf.Clamp (desiredPosition.y, MinY, MaxY);
			transform.position = new Vector3 (clampX, clampY, desiredPosition.z);
		}

		#endregion
	}
}
