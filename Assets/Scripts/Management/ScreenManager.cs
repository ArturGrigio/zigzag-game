using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ZigZag
{
	/// <summary>
	/// This class handles the UI components and scenes management of the game.
	/// </summary>
	public class ScreenManager : MonoBehaviour
	{
		#region Public Variables

		/// <summary>
		/// The Start Menu gameobject.
		/// </summary>
		[Tooltip("The Start Menu gameobject")]
		public GameObject StartMenu;

		/// <summary>
		/// The Game Over Menu gameobject.
		/// </summary>
		[Tooltip("The Game Over Menu gameobject")]
		public GameObject GameOverMenu;

		/// <summary>
		/// The ending of the game.
		/// </summary>
		[Tooltip("The ending gameobject")]
		public GameObject Ending;

		/// <summary>
		/// The fade in image.
		/// </summary>
		public RawImage FadeInImage;

		/// <summary>
		/// The player manager component.
		/// </summary>
		public PlayerManager playerManager;

		/// <summary>
		/// The audio manager.
		/// </summary>
		public AudioManager audioManager;

		/// <summary>
		/// The finish point script.
		/// </summary>
		public FinishPoint finishPoint;

		#endregion

		#region Private/Protected Variables

		/// <summary>
		/// List containing the end texts of the game.
		/// </summary>
		private static readonly List<string> EndTexts = new List<string> () 
		{
			"And so Cube released the Colors of The World from the Slime's imprisonment.",
			"Thus, the world of shapes had its color restored. Peace had returned...",
			"But, who was it that gave Cube its color...?",
			"The story continues..."
		};

		/// <summary>
		/// Array containing buttons that belong to the game over menu.
		/// </summary>
		private Button[] m_gameOverButtons;

		/// <summary>
		/// Array containing buttons that belong to the start menu.
		/// </summary>
		private Button[] m_startMenuButtons;

		#endregion

		#region Private/Protected Methods

		/// <summary>
		/// Unpause the game and begin the gameplay.
		/// </summary>
		private void clickPlay()
		{
			Time.timeScale = 1f;
			audioManager.PlaySoundEffect ("button");
			StartMenu.SetActive (false);
		}

		/// <summary>
		/// Restart the game when the button is clicked.
		/// </summary>
		private void clickRestart()
		{
			Debug.Log ("Restart game");
			audioManager.PlaySoundEffect ("button");
			//SceneManager.LoadScene ("Main-Test");
		}

		/// <summary>
		/// Load the latest save point in the game when the button is clicked.
		/// </summary>
		private void clickContinue()
		{
			Debug.Log ("Continue game");

			Time.timeScale = 1f;
			audioManager.PlaySoundEffect ("button");
			GameOverMenu.SetActive (false);

			playerManager.Respawn ();
		}

		/// <summary>
		/// Quit the game when the Quit button is clicked.
		/// </summary>
		private void clickQuit()
		{
			audioManager.PlaySoundEffect ("button");
			Application.Quit ();
		}

		/// <summary>
		/// Handle the player death event.
		/// </summary>
		private void playerDeathHandler()
		{
			Time.timeScale = 0f;

			Button continueButton = m_gameOverButtons.Single (button => button.name.Contains ("Continue"));
			if (!continueButton.interactable)
			{
				continueButton.interactable = true;
				Text continueText = continueButton.GetComponentInChildren<Text> ();
				continueText.color = new Color (continueText.color.r, continueText.color.g, continueText.color.b, 1f);
			}

			GameOverMenu.SetActive (true);
		}

		/// <summary>
		/// Play the ending and its animations.
		/// </summary>
		/// <returns>The ending coroutine.</returns>
		private IEnumerator playEnding()
		{
			yield return new WaitForSeconds (4f);

			Time.timeScale = 0f;
			Ending.SetActive (true);
			Text endTextComponent = Ending.GetComponentInChildren<Text> ();
			Animator animator = endTextComponent.GetComponent<Animator> ();

			foreach (string text in EndTexts)
			{
				endTextComponent.text = text;

				// Wait for the fade in transition to complete
				while (animator.GetCurrentAnimatorStateInfo (0).IsName ("FadeIn"))
				{
					yield return null;
				}

				// Wait for the fade out transition to complete
				while (animator.GetCurrentAnimatorStateInfo (0).IsName ("FadeOut"))
				{
					yield return null;
				}
			}

			animator.enabled = false;
			yield return audioManager.FadeOutAudio (0.015f);

			// Restart the game by loading Opening scene once the game is finished
			AsyncOperation asyncLoad = SceneManager.LoadSceneAsync ("Opening");
			while (!asyncLoad.isDone)
			{
				yield return null;
			}
		}

		/// <summary>
		/// Handle the finish event. Simply play the ending.
		/// </summary>
		private void finishHandler()
		{
			StartCoroutine (playEnding ());
		}

		#endregion

		#region Unity Methods

		// Use this for initialization
		private void Awake ()
		{
			Time.timeScale = 0f;

			playerManager.PlayerDeath += playerDeathHandler;
			finishPoint.Finish += finishHandler;

			// Register event handlers
			m_gameOverButtons = GameOverMenu.GetComponentsInChildren<Button>();
			m_startMenuButtons = StartMenu.GetComponentsInChildren<Button>();

			foreach (Button button in m_gameOverButtons)
			{
				if (button.name.Contains ("Continue"))
				{
					button.onClick.AddListener (clickContinue);
				}
				else if (button.name.Contains ("Restart"))
				{
					button.onClick.AddListener (clickRestart);
				}
				else if (button.name.Contains ("Quit"))
				{
					button.onClick.AddListener (clickQuit);
				}
			}

			foreach (Button button in m_startMenuButtons)
			{
				if (button.name.Contains ("Play"))
				{
					button.onClick.AddListener (clickPlay);
				}
				else if (button.name.Contains ("Quit"))
				{
					button.onClick.AddListener (clickQuit);
				}
			}
		}

		#endregion
	}
}