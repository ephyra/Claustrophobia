using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

	private enum State
	{
		MENU,
		INSTRUCTIONS,
		GAME_OVER
	}

	public GameObject pausePanel;
	public GameObject instructionsPanel;
	public GameObject gameOverPanel;

	public Text gameOverText;
	public CustomButton[] gameOverButtons;

	public Image background;

	public GameObject instructionAnims;

	public GameObject howToPause;

	public CustomButton[] buttons;
	public int currentlySelectedButton = 0;

	public bool gameIsPaused = false;

	private State currentState;

	public AudioSource menuSFX;

	private Color initBgColor;

	void Awake () {
		initBgColor = background.color;
	}


	void Start ()
	{
		background.color = new Color (0, 0, 0, 0);
		HidePauseMenu ();

	}


	public void ShowGameOverMenu (string cause)
	{
		currentState = State.GAME_OVER;
		background.color = initBgColor;
		howToPause.SetActive (false);
		pausePanel.SetActive (false);
		instructionsPanel.SetActive (false);
		gameOverPanel.SetActive (true);
		gameOverText.text = cause;
		currentlySelectedButton = 0;
	}

	public void ShowPauseMenu ()
	{
		howToPause.SetActive (false);
		currentState = State.MENU;
		background.color = initBgColor;
		instructionAnims.SetActive (false);
		instructionsPanel.SetActive (false);
		pausePanel.SetActive (true);
		currentlySelectedButton = 0;
		gameOverPanel.SetActive (false);
	}

	public void HidePauseMenu ()
	{
		Camera.main.gameObject.GetComponent<TitleShake> ().enabled = true;
		currentState = State.MENU;
		background.color = new Color (0, 0, 0, 0);
		instructionAnims.SetActive (false);
		instructionsPanel.SetActive (false);
		pausePanel.SetActive (false);
		gameOverPanel.SetActive (false);
	}

	public void ShowInstructionsPanel ()
	{
		Camera.main.gameObject.GetComponent<TitleShake> ().enabled = false;
		currentState = State.INSTRUCTIONS;
		instructionAnims.SetActive (true);
		pausePanel.SetActive (false);
		instructionsPanel.SetActive (true);
	}

	public void QuitToMenu ()
	{
		//Persistent.Obj.comingFromGame = true;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);

	}

	public void Quit ()
	{
		Application.Quit ();

	}

	public void SetSelectedButton (int buttonNo) {
		currentlySelectedButton = buttonNo;
	}


	void Update ()
	{
		if (gameIsPaused && currentState != State.GAME_OVER) {

			if (currentState == State.MENU) {

				if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) && currentlySelectedButton > 0) { // Up
					menuSFX.Play ();
					currentlySelectedButton--;
				}

				if ((Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) && currentlySelectedButton < buttons.Length - 1) { // Down
					menuSFX.Play ();
					currentlySelectedButton++; 
				}

				for (int i = 0; i < buttons.Length; i++) {

					if (i == currentlySelectedButton) {
						buttons [i].isSelected = true;
					} else {
						buttons [i].isSelected = false;
					}

				}

				if (Input.GetKeyDown (KeyCode.Return)) {

					switch (currentlySelectedButton) {

					case 0:
						currentState = State.INSTRUCTIONS;
						ShowInstructionsPanel ();
						break;

					case 1:
						QuitToMenu ();
						break;

					case 2:
						Quit ();
						break;

					default:
						currentState = State.INSTRUCTIONS;
						ShowInstructionsPanel ();
						break;

					}

				}

			}

		}

		if (currentState == State.GAME_OVER) {

			print (1);

			if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) && currentlySelectedButton > 0) { // Up
				menuSFX.Play ();
				currentlySelectedButton--;
			}

			if ((Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) && currentlySelectedButton < gameOverButtons.Length - 1) { // Down
				menuSFX.Play ();
				currentlySelectedButton++; 
			}

			for (int i = 0; i < buttons.Length; i++) {

				if (i == currentlySelectedButton) {
					gameOverButtons [i].isSelected = true;
				} else {
					gameOverButtons [i].isSelected = false;
				}

			}

			if (Input.GetKeyDown (KeyCode.Return)) {

				switch (currentlySelectedButton) {

				case 0:
					SceneManager.LoadScene ("Main");
					break;

				case 1:
					SceneManager.LoadScene ("Menu");
					break;

				case 2:
					Quit ();
					break;

				default:
					break;

				}

			}

		}

	}
}
