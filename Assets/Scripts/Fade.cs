using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

	public float fadeTime = 1.0f;

	private Color black = new Color (0, 0, 0, 1f);
	private Color invis = new Color (0, 0, 0, 0);

	void Start () {
		StartCoroutine (FadeIn ());
	}

	IEnumerator FadeIn () {
		float elapsedTime = 0;
		while (elapsedTime < fadeTime) {
			GetComponent<SpriteRenderer> ().color = Color.Lerp (black, invis, elapsedTime / fadeTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		gameObject.SetActive (false);
	}

}
