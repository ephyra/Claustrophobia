using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour {

	public float onTime = 0.9f;
	public float offTime = 0.35f;

	public Color invisible = new Color(0, 0, 0, 0);
	public Color initColor;

	private Text _text;

	void Awake () {
		_text = GetComponent<Text> ();
		initColor = _text.color;
	}

	void OnEnable () {
		StartCoroutine (ToggleAfterSeconds ());
	}

	IEnumerator ToggleAfterSeconds () {

		while (true) {
			if (_text.color == initColor) {
				_text.color = invisible;
				yield return new WaitForSecondsRealtime (offTime);
			} else {
				_text.color = initColor;
				yield return new WaitForSecondsRealtime (onTime);
			}
		}
	}
}
