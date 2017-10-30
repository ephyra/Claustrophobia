using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashscreenController : MonoBehaviour {

	public Sprite[] frames;

	[Tooltip("Order of frames for the opening animation.")]
	public int[] frameOrder;
	[Tooltip("Duration of frames for the opening animation.")]
	public float[] frameDuration;

	public Image img;

	void Start () {

		StartCoroutine (PlayAnimation ());



	}

	IEnumerator PlayAnimation () {
		
		for (int i = 0; i < frameOrder.Length; i++) {

			print (frames[frameOrder[i]]);

			img.sprite = frames [frameOrder [i]];
			yield return new WaitForSecondsRealtime (frameDuration [i]);

		}

		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

}
