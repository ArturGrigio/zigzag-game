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
		/// The boss trigger when player is about to fight the boss.
		/// </summary>
		public BossTrigger bossTrigger;

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

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Handle the boss event.
		/// </summary>
		private void BeforeBossHandler()
		{
			StartCoroutine(switchAudio (BossTheme, 0.01f));
			bossTrigger.BeforeBoss -= BeforeBossHandler;
		}

		/// <summary>
		/// Fade out the audio and play the boss theme audio clip.
		/// </summary>
		/// <returns>Coroutine.</returns>
		/// <param name="fadeTime">Fade time.</param>
		private IEnumerator switchAudio(AudioClip nextAudioClip, float fadeTime)
		{
			float startVolume = MusicSource.volume;

			// Fade out the current theme
			while (MusicSource.volume > 0f)
			{
				MusicSource.volume -= fadeTime;
				yield return null;
			}

			MusicSource.Stop ();
			MusicSource.volume = startVolume;

			// Play the next audio clip
			MusicSource.clip = nextAudioClip;
			MusicSource.Play ();
		}

		#endregion

		#region Unity Methods

		// Use this for initialization
		private void Awake ()
		{
			bossTrigger.BeforeBoss += BeforeBossHandler;

			MusicSource.clip = MainTheme;
			MusicSource.Play ();
		}

		#endregion
	}
}
