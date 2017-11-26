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
		/// The quit button.
		/// </summary>
		[Tooltip("The quit button")]
		public Button QuitButton;

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
			Debug.Log ("Continue game");
			ContinueButton.GetComponent<AudioSource> ().Play ();
			Time.timeScale = 1f;
			GameOver.SetActive (false);

			playerManager.LoadPositions ();
		}

		/// <summary>
		/// Quit the game when the Quit button is clicked.
		/// </summary>
		private void clickQuit()
		{
			QuitButton.GetComponent<AudioSource> ().Play ();
			Application.Quit ();
		}

		/// <summary>
		/// Handle the player death event.
		/// </summary>
		private void playerDeathHandler()
		{
			Time.timeScale = 0f;

			if (!ContinueButton.interactable && SavePoint.SavedPlayerPositions.Count > 0)
			{
				ContinueButton.interactable = true;
				Text continueText = ContinueButton.GetComponentInChildren<Text> ();
				continueText.color = new Color (continueText.color.r, continueText.color.g, continueText.color.b, 1f);
			}

			GameOver.SetActive (true);
			GameOver.GetComponent<AudioSource> ().Play ();
		}

		#endregion

		#region Unity Methods

		// Use this for initialization
		private void Awake ()
		{
			playerManager.PlayerDeath += playerDeathHandler;
			RestartButton.onClick.AddListener (clickRestart);
			ContinueButton.onClick.AddListener (clickContinue);
			QuitButton.onClick.AddListener (clickQuit);
		}

		#endregion
	}
}