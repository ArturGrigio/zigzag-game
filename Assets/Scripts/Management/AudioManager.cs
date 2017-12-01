using UnityEngine;
using System.Collections;

namespace ZigZag
{
	/// <summary>
	/// This class manages the overall audio of the game.
	/// </summary>
	public class AudioManager : MonoBehaviour
	{
		#region Public Variables

		/// <summary>
		/// The main theme audio clip.
		/// </summary>
		[Tooltip("The main theme audio clip")]
		public AudioClip MainTheme;

		/// <summary>
		/// The boss theme audio clip.
		/// </summary>
		[Tooltip("The boss theme audio clip")]
		public AudioClip BossTheme;

		/// <summary>
		/// The boss trigger when player is about to fight the boss.
		/// </summary>
		public BossTrigger bossTrigger;

		#endregion

		#region Private/Protected Variables

		/// <summary>
		/// The audio source component.
		/// </summary>
		private AudioSource m_audioSource;

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Handle the boss event.
		/// </summary>
		private void BeforeBossHandler()
		{
			StartCoroutine(switchAudio (0.01f));
			bossTrigger.BeforeBoss -= BeforeBossHandler;
		}

		/// <summary>
		/// Fade out the audio and play the boss theme audio clip.
		/// </summary>
		/// <returns>Coroutine.</returns>
		/// <param name="fadeTime">Fade time.</param>
		private IEnumerator switchAudio(float fadeTime)
		{
			float startVolume = m_audioSource.volume;

			// Fade out the main theme
			while (m_audioSource.volume > 0f)
			{
				m_audioSource.volume -= fadeTime;
				yield return null;
			}

			m_audioSource.Stop ();
			m_audioSource.volume = startVolume;

			// Play the boss theme audio clip
			m_audioSource.clip = BossTheme;
			m_audioSource.Play ();
		}

		#endregion

		#region Unity Methods

		// Use this for initialization
		private void Awake ()
		{
			bossTrigger.BeforeBoss += BeforeBossHandler;

			m_audioSource = GetComponent<AudioSource> ();
			m_audioSource.clip = MainTheme;
			m_audioSource.Play ();
		}

		#endregion
	}
}
