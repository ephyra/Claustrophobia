using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour {

	public GameObject canvas;
	public bool gameIsPaused = false;


	public void ShowPauseMenu () {

		ResetPauseMenu ();
		canvas.SetActive (true);

	}

	public void HidePauseMenu () {
		canvas.SetActive (false);
	}

	private void ResetPauseMenu () {
	}

	void Update () {

		if (gameIsPaused) {



		}

	}
}
