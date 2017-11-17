using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	public static MainMenuController Obj;

	private enum State
	{
		MAIN_MENU,
		INSTRUCTIONS,
		CREDITS
	}

	public GameObject instructionAnimations;
	public GameObject mainMenuAnimations;

	public CustomButton[] buttons;
	public int currentlySelectedButton = 0;

	public GameObject buttonPanel;
	public GameObject creditsPanel;
	public GameObject instructionsPanel;

	private State currentState;

	public AudioSource sfx;

	public Image fadeImage;
	public float fadeLength = 0.5f;

	void Awake ()
	{

		Obj = this;

		// Fade from black
		if (!Persistent.Obj.comingFromGame) {
			fadeImage.gameObject.SetActive (true);
			StartCoroutine (fadeFromBlack ());
		}

		// Reset the UI
		ShowButtonPanel ();
		HideInstructionsPanel ();
		HideCreditsPanel ();

		currentState = State.MAIN_MENU;

	}

	public void ShowInstructionsPanel ()
	{
		currentState = State.INSTRUCTIONS;
		instructionAnimations.SetActive (true);
		instructionsPanel.SetActive (true);
		HideButtonPanel ();
	}

	public void HideInstructionsPanel ()
	{
		currentState = State.MAIN_MENU;
		instructionAnimations.SetActive (false);
		instructionsPanel.SetActive (false);
	}

	public void ShowButtonPanel ()
	{
		currentState = State.MAIN_MENU;
		buttonPanel.SetActive (true);
		mainMenuAnimations.SetActive (true);
		HideCreditsPanel ();
		HideInstructionsPanel ();
	}

	public void HideButtonPanel ()
	{
		buttonPanel.SetActive (false);
		mainMenuAnimations.SetActive (false);
	}

	public void ShowCreditsPanel ()
	{
		currentState = State.CREDITS;
		creditsPanel.SetActive (true);
		HideButtonPanel ();
	}

	public void HideCreditsPanel ()
	{
		currentState = State.MAIN_MENU;
		creditsPanel.SetActive (false);
	}


	public void NextScene ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void Quit ()
	{
		Application.Quit ();
	}


	void Update ()
	{

		switch (currentState) {

		case State.MAIN_MENU:

			if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) && currentlySelectedButton > 0) { // Up
				sfx.Play ();
				currentlySelectedButton --;
			}

			if ((Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) && currentlySelectedButton < buttons.Length - 1) { // Down
				sfx.Play ();
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
					NextScene ();
					break;

				case 1:
					ShowInstructionsPanel ();
					//Persistent.Obj.hasReadInstructions = true;
					currentState = State.INSTRUCTIONS;
					break;

				case 2:
					ShowCreditsPanel ();
					currentState = State.CREDITS;
					break;

				case 3:
					Quit ();
					break;
				
				default:
					NextScene ();
					break;

				}

			}

			break;

		case State.INSTRUCTIONS:

			if (Input.GetKeyDown (KeyCode.Escape)) {
				ShowButtonPanel ();
				currentState = State.MAIN_MENU;
			}

			break;

		case State.CREDITS:

			if (Input.GetKeyDown (KeyCode.Escape)) {
				ShowButtonPanel ();
				currentState = State.MAIN_MENU;
			}

			break;

		default:
			break;


		}

	}

	IEnumerator fadeFromBlack () {

		float elapsedTime = 0;

		while (fadeImage.color.a > 0) {

			fadeImage.color = new Color (0, 0, 0, Mathf.SmoothStep (1f, 0, elapsedTime / fadeLength));
			elapsedTime += Time.deltaTime;

			yield return null;

		}

		fadeImage.transform.parent.gameObject.SetActive (false);

	}

	public void SetSelectedButton (int buttonNo) {
		currentlySelectedButton = buttonNo;
	}

}
