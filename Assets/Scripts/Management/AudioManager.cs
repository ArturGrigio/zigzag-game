using UnityEngine;
using System.Collections;
using System.Linq;

namespace ZigZag
{
	/// <summary>
	/// This class manages the overall audio and sound effect of the game.
	/// </summary>
	public class AudioManager : MonoBehaviour
	{
		#region Public Variables

		/// <summary>
		/// Array of music themes.
		/// </summary>
		[Tooltip("Array of music themes")]
		public AudioClip[] MusicThemes;

		/// <summary>
		/// Array of sound effects.
		/// </summary>
		[Tooltip("Array of sound effects")]
		public AudioClip[] SoundEffects;

		/// <summary>
		/// The music source.
		/// </summary>
		public AudioSource MusicSource;

		/// <summary>
		/// The sound effect source.
		/// </summary>
		public AudioSource SoundEffectSource;

		/// <summary>
		/// The finish point script.
		/// </summary>
		public FinishPoint finishPoint;

		#endregion

		#region Private Variables

		/// <summary>
		/// The singleton instance of the AudioManager class.
		/// </summary>
		private static AudioManager m_audioManager = null;

		/// <summary>
		/// The player manager.
		/// </summary>
		private PlayerManager m_playerManager;

		#endregion

		#region Properties

		/// <summary>
		/// Get the AudioManager singleton.
		/// </summary>
		public static AudioManager Instance
		{
			get { return m_audioManager; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Play a single sound effect clip.
		/// </summary>
		/// <param name="soundEffectClip">Sound effect clip.</param>
		public void PlaySoundEffect(AudioClip soundEffectClip)
		{
			SoundEffectSource.clip = soundEffectClip;
			SoundEffectSource.Play ();
		}

		/// <summary>
		/// Play a single sound effect clip
		/// </summary>
		/// <param name="soundEffectName">Sound effect name.</param>
		public void PlaySoundEffect(string soundEffectName)
		{
			AudioClip soundEffect = SoundEffects.First (audio => audio.name.Contains (soundEffectName));
			SoundEffectSource.clip = soundEffect;
			SoundEffectSource.Play ();
		}

		/// <summary>
		/// Play the music theme.
		/// </summary>
		/// <param name="musicName">Music name.</param>
		public void PlayMusic(string musicName)
		{
			AudioClip music = MusicThemes.First (audio => audio.name.Contains (musicName));
			MusicSource.clip = music;
			MusicSource.Play ();
		}

		/// <summary>
		/// Stop the music and all sound effects.
		/// </summary>
		public void Stop()
		{
			MusicSource.Stop ();
			SoundEffectSource.Stop ();
		}

		/// <summary>
		/// Fade in the current music audio clip. This method plays the theme automatically.
		/// </summary>
		/// <returns>Audio coroutine.</returns>
		/// <param name="fadeTime">Fade in time.</param>
		public IEnumerator FadeInAudio(float fadeInTime)
		{
			float startVolume = MusicSource.volume;
			MusicSource.Play ();
			MusicSource.volume = 0f;

			// Fade in the current theme
			while (MusicSource.volume < startVolume)
			{
				MusicSource.volume += fadeInTime;
				yield return null;
			}
		}

		/// <summary>
		/// Fade out the current music audio clip. This method stops the theme automatically.
		/// </summary>
		/// <returns>Audio coroutine.</returns>
		/// <param name="fadeTime">Fade out time.</param>
		public IEnumerator FadeOutAudio(float fadeOutTime)
		{
			float startVolume = MusicSource.volume;

			// Fade out the current theme
			while (MusicSource.volume > 0f)
			{
				MusicSource.volume -= fadeOutTime;
				yield return null;
			}

			MusicSource.Stop ();
			MusicSource.volume = startVolume;
		}

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Handle the respawn player event.
		/// </summary>
		private void respawnPlayerHandler()
		{
			if (MusicSource.clip != SavePoint.SavedMusicTheme)
			{
				MusicSource.Stop ();
				MusicSource.clip = SavePoint.SavedMusicTheme;
				MusicSource.Play ();
			}
			else
			{
				StartCoroutine(FadeInAudio (0.02f));
			}
		}

		/// <summary>
		/// Handle the finish event. Play the ending theme.
		/// </summary>
		private void finishHandler ()
		{
			AudioClip endingTheme = MusicThemes.First (audio => audio.name.Contains ("Ending"));
			switchAudio (endingTheme);
		}

		/// <summary>
		/// Switch audio without any fade in or fade out.
		/// </summary>
		/// <param name="nextAudioClip">Next audio clip.</param>
		private void switchAudio(AudioClip nextAudioClip)
		{
			if (!MusicSource.isPlaying || MusicSource.clip != nextAudioClip)
			{
				MusicSource.Stop ();
				MusicSource.clip = nextAudioClip;
				MusicSource.Play ();
			}
		}

		/// <summary>
		/// Switch the audio.
		/// Fade out the current audio and play the next theme audio clip then fade in.
		/// </summary>
		/// <returns>Coroutine.</returns>
		/// <param name="nextAudioClip">Next audio clip.</param>
		/// <param name="fadeInTime">Fade in time of the next audio clip.</param>
		/// <param name="fadeOutTime">Fade out time of the current audio clip.</param>
		private IEnumerator switchAudio(AudioClip nextAudioClip, float fadeInTime, float fadeOutTime)
		{
			// Wait until the current audio is completely faded out
			yield return StartCoroutine (FadeOutAudio (fadeOutTime));

			// Play the next audio clip
			MusicSource.clip = nextAudioClip;

			// Fade in the next audio clip
			yield return StartCoroutine (FadeInAudio (fadeInTime));
		}

		#endregion

		#region Unity Methods

		/// <summary>
		/// Initialize the singleton instance.
		/// </summary>
		private void Awake ()
		{
			if (m_audioManager != null && m_audioManager != this)
			{
				Destroy (this.gameObject);
			}
			else
			{
				m_audioManager = this;
			}
		}

		/// <summary>
		/// Initialize member variables.
		/// </summary>
		private void Start()
		{
			m_playerManager = PlayerManager.Instance;

			// Register handlers to events
			m_playerManager.RespawnPlayer += respawnPlayerHandler;

			finishPoint.Finish += finishHandler;

			AudioClip mainTheme = MusicThemes.First (audio => audio.name.Contains ("Main"));
			MusicSource.clip = mainTheme;
			StartCoroutine(FadeInAudio (0.01f));
		}

		#endregion
	}
}
