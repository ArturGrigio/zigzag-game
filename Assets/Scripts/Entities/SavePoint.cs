using UnityEngine;
using System.Collections.Generic;

namespace ZigZag
{
	/// <summary>
	/// This class saves the positions of all the acquired shapes when
	/// the current shape reaches to a save point.
	/// </summary>
	public class SavePoint : MonoBehaviour
	{
		#region Public Variables

		/// <summary>
		/// The camera 2D follow.
		/// </summary>
		public Camera2DFollow camera2DFollow;

		/// <summary>
		/// The camera range1.
		/// </summary>
		public CameraRange cameraRange1;

		/// <summary>
		/// The camera range2.
		/// </summary>
		public CameraRange cameraRange2;

		#endregion

		#region Private Variables

		/// <summary>
		/// The audio source component.
		/// </summary>
		private AudioSource m_audioSource;

		/// <summary>
		/// The audio manager.
		/// </summary>
		private AudioManager m_audioManager;

		/// <summary>
		/// The position of the latest save point.
		/// </summary>
		private static Vector2 m_latestSavePoint;

		/// <summary>
		/// The saved theme. Play this saved music theme when player
		/// is respawned. 
		/// Ex: die during boss fight, respawn right before boss fight but the music
		/// should not be the Boss Theme.
		/// </summary>
		private static AudioClip m_savedMusicTheme;

		/// <summary>
		/// The saved camera range.
		/// 0 -> Min X
		/// 1 -> Max X
		/// 2 -> Min Y
		/// 3 -> Max Y
		/// </summary>
		private static float[] m_savedCameraBoundary = new float[4];

		/// <summary>
		/// The saved value belonging to the first CameraRange object.
		/// </summary>
		private static float m_savedCameraRangeValue1;

		/// <summary>
		/// The saved value belonging to the second CameraRange object.
		/// </summary>
		private static float m_savedCameraRangeValue2;

		#endregion

		#region Properties

		public static Vector2 LatestSavePoint
		{
			get { return m_latestSavePoint; }
		}

		public static AudioClip SavedMusicTheme
		{
			get { return m_savedMusicTheme; }
		}

		public static float[] SavedCameraBoundary
		{
			get { return m_savedCameraBoundary; }
		}

		public static float SavedCameraRangeValue1
		{
			get { return m_savedCameraRangeValue1; }
		}

		public static float SavedCameraRangeValue2
		{
			get { return m_savedCameraRangeValue2; }
		}

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Start()
		{
			m_audioSource = GetComponent<AudioSource> ();
			m_audioManager = AudioManager.Instance;
		}

		/// <summary>
		/// Raises the trigger enter 2D event.
		/// </summary>
		/// <param name="collider">Collider.</param>
		private void OnTriggerEnter2D(Collider2D collider)
		{
			int activePlayerLayer = LayerMask.NameToLayer ("Active Player");

			// Check if the player has come in contact with a save point and only save 
			// if the save point is the latest one in the game
			if (collider.gameObject.layer == activePlayerLayer && LatestSavePoint.x < transform.position.x)
			{
				m_audioSource.Play ();
				save ();
			}
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Save the game progress
		/// </summary>
		private void save()
		{
			// Save location
			m_latestSavePoint = transform.position;

			// Save music theme
			if (m_savedMusicTheme != m_audioManager.MusicSource.clip)
			{
				m_savedMusicTheme = m_audioManager.MusicSource.clip;
			}

			// Save camera boundary
			m_savedCameraBoundary[0] = camera2DFollow.MinX;
			m_savedCameraBoundary[1] = camera2DFollow.MaxX;
			m_savedCameraBoundary[2] = camera2DFollow.MinY;
			m_savedCameraBoundary[3] = camera2DFollow.MaxY;

			// Save camera range values
			m_savedCameraRangeValue1 = cameraRange1.CameraMinY;
			m_savedCameraRangeValue2 = cameraRange2.CameraMinY;
		}

		#endregion
	}
}

