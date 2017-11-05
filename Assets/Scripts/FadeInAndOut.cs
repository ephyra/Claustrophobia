using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAndOut : MonoBehaviour {

	public float fadeTime = 3.0f;
	public float holdTime = 5.0f;

	private Text _text;

	void Start () {
	
		_text = GetComponent<Text> ();
		_text.color = new Color (0, 0, 0, 0);

		StartCoroutine (FadeAndHold ());

	}

	IEnumerator FadeAndHold () {

		float elapsedTime = 0;
		bool isFadingIn = true;

		while (isFadingIn) {

			if (elapsedTime < fadeTime) {
				_text.color = new Color (1f, 1f, 1f, Mathf.SmoothStep (0, 1f, elapsedTime / fadeTime));
				elapsedTime += Time.deltaTime;
			} else {
				isFadingIn = false;
				elapsedTime = 0;
			}
	
			yield return null;
		}

		yield return new WaitForSecondsRealtime (holdTime);

		while (elapsedTime < fadeTime) {
			_text.color = new Color (1f, 1f, 1f, Mathf.SmoothStep (1f, 0, elapsedTime / fadeTime));
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		gameObject.SetActive (false);

	}
}
