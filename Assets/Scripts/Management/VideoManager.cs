using UnityEngine;
using UnityEngine.UI;
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
			StartCoroutine(PlayVideo ());
		}

		private IEnumerator PlayVideo ()
		{
			m_rawImage.texture = movie;
			movie.Play ();

			m_audioSource.clip = movie.audioClip;
			m_audioSource.Play ();

			yield return new WaitForSeconds (m_audioSource.clip.length + 5f);
			// TODO: switch to the game scene
		}
	}
}
