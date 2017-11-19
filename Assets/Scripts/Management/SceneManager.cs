using UnityEngine;
using UnityEngine.UI;
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
		}

		/// <summary>
		/// Load the latest save point in the game when the button is clicked.
		/// </summary>
		private void clickContinue()
		{
			Debug.Log ("Continue game");
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
			playerManager.OnPlayerDeath += playerDeathHandler;
			RestartButton.onClick.AddListener (clickRestart);
			ContinueButton.onClick.AddListener (clickContinue);
		}

		#endregion
	}
}