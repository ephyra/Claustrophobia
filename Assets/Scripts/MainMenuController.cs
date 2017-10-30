using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public GameObject buttonPanel;
	public GameObject creditsPanel;
	public GameObject instructionsPanel;

	void Start () {
		// Reset the UI
		ShowButtonPanel ();
		HideInstructionsPanel ();
		HideCreditsPanel ();
	}

	public void ShowInstructionsPanel () {
		instructionsPanel.SetActive (true);
	}

	public void HideInstructionsPanel () {
		instructionsPanel.SetActive (false);
	}

	public void ShowButtonPanel () {
		buttonPanel.SetActive (true);
	}

	public void HideButtonPanel () {
		buttonPanel.SetActive (false);
	}

	public void ShowCreditsPanel () {
		creditsPanel.SetActive (true);
	}

	public void HideCreditsPanel () {
		creditsPanel.SetActive (false);
	}


	public void NextScene () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void Quit () {
		Application.Quit ();
	}

}
