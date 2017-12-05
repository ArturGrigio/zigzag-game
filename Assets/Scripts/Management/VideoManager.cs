using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ZigZag
{
	/// <summary>
	/// This class handles the opening of the game
	/// </summary>
	public class VideoManager : MonoBehaviour
	{
		#region Public Variables

		/// <summary>
		/// The movie.
		/// </summary>
		public MovieTexture movie;

		#endregion

		#region Private Variables

		/// <summary>
		/// The raw image component.
		/// </summary>
		private RawImage m_rawImage;

		/// <summary>
		/// The audio source component.
		/// </summary>
		private AudioSource m_audioSource;

		/// <summary>
		/// The singleton instance of the VideoManager class.
		/// </summary>
		private static VideoManager m_videoManager = null;

		#endregion

		#region Properties

		/// <summary>
		/// Get the VideoManager singleton.
		/// </summary>
		public static VideoManager Instance
		{
			get { return m_videoManager; }
		}

		#endregion

		#region Private/Protected Methods

		private IEnumerator playVideo ()
		{
			m_rawImage.texture = movie;
			movie.Play ();

			m_audioSource.clip = movie.audioClip;
			m_audioSource.Play ();

			yield return new WaitForSeconds (m_audioSource.clip.length);

			yield return StartCoroutine(ScreenManager.LoadScene("Main"));
		}

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize the singleton instance.
		/// </summary>
		private void Awake ()
		{
			if (m_videoManager != null && m_videoManager != this)
			{
				Destroy (this.gameObject);
			}
			else
			{
				m_videoManager = this;
			}
		}

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Start ()
		{
			m_rawImage = GetComponent<RawImage> ();
			m_audioSource = GetComponent<AudioSource> ();
			StartCoroutine (playVideo ());
		}

		#endregion
	}
}
