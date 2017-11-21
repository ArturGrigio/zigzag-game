using UnityEngine;
using UnityEngine.UI;
using USM = UnityEngine.SceneManagement;
using System.Collections;

namespace ZigZag
{
	/// <summary>
	/// This class handles the UI components and scenes management of the game.
	/// </summary>
	public class SceneManager : MonoBehaviour
	{
		#region Public Variables

		/// <summary>
		/// The parent GameOver object that contains other related game over objects.
		/// </summary>
		[Tooltip("The parent GameOver object that contains other related game over objects")]
		public GameObject GameOver;

		/// <summary>
		/// The restart button.
		/// </summary>
		[Tooltip("The restart button")]
		public Button RestartButton;

		/// <summary>
		/// The continue button.
		/// </summary>
		[Tooltip("The continue button")]
		public Button ContinueButton;

		/// <summary>
		/// The player manager component.
		/// </summary>
		public PlayerManager playerManager;

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Restart the game when the button is clicked.
		/// </summary>
		private void clickRestart()
		{
			Debug.Log ("Restart game");
			RestartButton.GetComponent<AudioSource> ().Play ();
			//USM.SceneManager.LoadScene ("Main");
		}

		/// <summary>
		/// Load the latest save point in the game when the button is clicked.
		/// </summary>
		private void clickContinue()
		{
			// Restart the game if there are no saved positions
			if (SavePoint.SavedPlayerPositions.Count == 0)
			{
				clickRestart ();
			}
			else
			{
				Debug.Log ("Continue game");
				ContinueButton.GetComponent<AudioSource> ().Play ();
				Time.timeScale = 1f;
				GameOver.SetActive (false);

				playerManager.LoadPositions ();
			}
		}

		/// <summary>
		/// Quit the game when the Quit button is clicked.
		/// </summary>
		private void clickQuit()
		{
			Application.Quit ();
		}

		/// <summary>
		/// Handle the player death event.
		/// </summary>
		private void playerDeathHandler()
		{
			Time.timeScale = 0f;
			GameOver.SetActive (true);
		}

		#endregion

		#region Unity Methods

		// Use this for initialization
		private void Awake ()
		{
			playerManager.PlayerDeath += playerDeathHandler;
			RestartButton.onClick.AddListener (clickRestart);
			ContinueButton.onClick.AddListener (clickContinue);
		}

		#endregion
	}
}