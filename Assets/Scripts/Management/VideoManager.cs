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

		#endregion

		// Use this for initialization
		private void Start ()
		{
			m_rawImage = GetComponent<RawImage> ();
			m_audioSource = GetComponent<AudioSource> ();
			StartCoroutine(playVideo ());
		}

		private IEnumerator playVideo ()
		{
			m_rawImage.texture = movie;
			movie.Play ();

			m_audioSource.clip = movie.audioClip;
			m_audioSource.Play ();

			yield return new WaitForSeconds (m_audioSource.clip.length);

			// TODO: switch to the game scene
			StartCoroutine(loadScene());
		}

		private IEnumerator loadScene()
		{
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync ("Main-Test");

			while (!asyncLoad.isDone)
			{
				yield return null;
			}
		}
	}
}
