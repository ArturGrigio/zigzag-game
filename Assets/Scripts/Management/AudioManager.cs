using UnityEngine;
using System.Collections;
using System.Linq;

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
		/// The ending theme audio clip.
		/// </summary>
		[Tooltip("The ending theme audio clip")]
		public AudioClip EndingTheme;

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
		/// The player manager.
		/// </summary>
		public PlayerManager playerManager;

		/// <summary>
		/// The boss trigger when player is about to fight the boss.
		/// </summary>
		public BossTrigger bossTrigger;

		/// <summary>
		/// The finish point script.
		/// </summary>
		public FinishPoint finishPoint;

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
		/// Fade in the current music audio clip.
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
		/// Fade out the current music audio clip.
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
		/// Handle the player death event.
		/// </summary>
		private void playerDeathHandler()
		{
			StartCoroutine(FadeOutAudio (0.02f));

			AudioClip gameOverAudio = SoundEffects.First (sound => sound.name.Contains ("Game Over"));
			PlaySoundEffect (gameOverAudio);
		}

		/// <summary>
		/// Handle the respawn player event.
		/// </summary>
		private void respawnPlayerHandler()
		{
			if (MusicSource.clip != SavePoint.SavedMusicTheme)
			{
				StartCoroutine (switchAudio (SavePoint.SavedMusicTheme, 0.01f, 0.02f));
			}
			else
			{
				StartCoroutine(FadeInAudio (0.02f));
			}
		}

		/// <summary>
		/// Handle the boss event. Play the boss theme.
		/// </summary>
		private void beforeBossHandler()
		{
			StartCoroutine(switchAudio (BossTheme, 0.01f, 0.03f));
			bossTrigger.BeforeBoss -= beforeBossHandler;
		}

		/// <summary>
		/// Handle the finish event. Play the ending theme.
		/// </summary>
		private void finishHandler ()
		{
			StartCoroutine(switchAudio(EndingTheme, 0.01f, 0.04f));
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

		// Use this for initialization
		private void Awake ()
		{
			playerManager.PlayerDeath += playerDeathHandler;
			playerManager.RespawnPlayer += respawnPlayerHandler;
			bossTrigger.BeforeBoss += beforeBossHandler;
			finishPoint.Finish += finishHandler;

			MusicSource.clip = MainTheme;
			MusicSource.Play ();
		}

		#endregion
	}
}
