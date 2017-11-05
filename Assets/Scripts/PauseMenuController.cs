using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

	private enum State
	{
		MENU, INSTRUCTIONS
	}

	public GameObject pausePanel;
	public GameObject instructionsPanel;
	public Image background;

	public GameObject howToPause;

	public CustomButton[] buttons;
	public int currentlySelectedButton = 0;

	public bool gameIsPaused = false;

	private State currentState;

	public AudioSource menuSFX;


	void Start () {

		background.color = new Color (0, 0, 0, 0);
		HidePauseMenu ();

	}

	public void ShowPauseMenu () {
		howToPause.SetActive (false);
		currentState = State.MENU;
		background.color = new Color (0, 0, 0, 0.9f);
		instructionsPanel.SetActive (false);
		pausePanel.SetActive (true);
		currentlySelectedButton = 0;
	}

	public void HidePauseMenu () {
		currentState = State.MENU;
		background.color = new Color (0, 0, 0, 0);
		instructionsPanel.SetActive (false);
		pausePanel.SetActive (false);

	}

	void ShowInstructionsPanel () {
		currentState = State.INSTRUCTIONS;
		pausePanel.SetActive (false);
		instructionsPanel.SetActive (true);
	}

	void QuitToMenu () {

		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);

	}

	void Quit () {

		Application.Quit ();

	}

	void Update () {

		if (gameIsPaused) {

			if (currentState == State.MENU) {

				if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) && currentlySelectedButton > 0) { // Up
					menuSFX.Play ();
					currentlySelectedButton --;
				}

				if ((Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) && currentlySelectedButton < buttons.Length - 1) { // Down
					menuSFX.Play ();
					currentlySelectedButton ++; 
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

	}
}
