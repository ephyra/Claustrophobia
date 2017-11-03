using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour {

	public bool isSelected;

	public Sprite unselectedSprite;
	public Sprite selectedSprite;

	void Start () {
	
		isSelected = false;
		GetComponent<Image> ().sprite = unselectedSprite;

	}

	void Update () {

		if (isSelected) {

			GetComponent<Image> ().sprite = selectedSprite;

		} else {

			GetComponent<Image> ().sprite = unselectedSprite;

		}

	}
}
