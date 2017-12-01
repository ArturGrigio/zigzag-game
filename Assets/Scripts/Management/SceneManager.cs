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
		/// The Start Menu gameobject.
		/// </summary>
		[Tooltip("The Start Menu gameobject")]
		public GameObject StartMenu;

		/// <summary>
		/// The play button in the Start Menu.
		/// </summary>
		[Tooltip("The play button in the Start Menu")]
		public Button PlayButton;

		/// <summary>
		/// The quit button in the Start Menu.
		/// </summary>
		[Tooltip("The quit button in the Start Menu")]
		public Button QuitStartButton;

		/// <summary>
		/// The fade in image.
		/// </summary>
		public RawImage FadeInImage;

		/// <summary>
		/// The Game Over Menu gameobject.
		/// </summary>
		[Tooltip("The Game Over Menu gameobject")]
		public GameObject GameOverMenu;

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
		/// The quit button in the Game Over Menu.
		/// </summary>
		[Tooltip("The quit button in the Game Over Menu")]
		public Button QuitGameOverButton;

		/// <summary>
		/// The player manager component.
		/// </summary>
		public PlayerManager playerManager;

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Unpause the game and begin the gameplay.
		/// </summary>
		private void clickPlay()
		{
			Time.timeScale = 1f;
			StartMenu.SetActive (false);
		}

		/// <summary>
		/// Restart the game when the button is clicked.
		/// </summary>
		private void clickRestart()
		{
			Debug.Log ("Restart game");
			RestartButton.GetComponent<AudioSource> ().Play ();
			//USM.SceneManager.LoadScene ("Main-Test");
		}

		/// <summary>
		/// Load the latest save point in the game when the button is clicked.
		/// </summary>
		private void clickContinue()
		{
			Debug.Log ("Continue game");
			ContinueButton.GetComponent<AudioSource> ().Play ();
			Time.timeScale = 1f;
			GameOverMenu.SetActive (false);

			playerManager.LoadPositions ();
		}

		/// <summary>
		/// Quit the game when the Quit button is clicked.
		/// </summary>
		private void clickQuit()
		{
			QuitGameOverButton.GetComponent<AudioSource> ().Play ();
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

			GameOverMenu.SetActive (true);
			GameOverMenu.GetComponent<AudioSource> ().Play ();
		}

		private void fadeIn()
		{
			
		}

		#endregion

		#region Unity Methods

		// Use this for initialization
		private void Awake ()
		{
			Time.timeScale = 0f;

			playerManager.PlayerDeath += playerDeathHandler;

			// Register event handlers
			PlayButton.onClick.AddListener(clickPlay);
			QuitStartButton.onClick.AddListener(clickQuit);

			RestartButton.onClick.AddListener (clickRestart);
			ContinueButton.onClick.AddListener (clickContinue);
			QuitGameOverButton.onClick.AddListener (clickQuit);
		}
			
//		float timer = 0f;
//		private void Update()
//		{
//			// Set the FadeInImage inactive after 5 and before 6 seconds
//
//			timer = Time.unscaledTime * timer;
//			if (FadeInImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length < timer)
//			{
//				FadeInImage.gameObject.SetActive (false);
//			}
//		}

		#endregion
	}
}