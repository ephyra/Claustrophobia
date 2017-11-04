using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour {

	public float onTime = 0.9f;
	public float offTime = 0.35f;

	private Text _text;
	private string initText;

	void OnEnable () {
		_text = GetComponent<Text> ();
		initText = GetComponent<Text> ().text;

		StartCoroutine (ToggleAfterSeconds ());
	}

	IEnumerator ToggleAfterSeconds () {

		while (true) {
			if (_text.text.Length == 0) {
				_text.text = initText;
				yield return new WaitForSecondsRealtime (onTime);
			} else {
				_text.text = "";
				yield return new WaitForSecondsRealtime (offTime);
			}

		}
	}
}
