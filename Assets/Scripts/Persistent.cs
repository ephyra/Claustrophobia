﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent : MonoBehaviour {

	public static Persistent Obj;

	public bool comingFromGame =false;

	public bool hasReadInstructions;

	void Awake () {

		if (Obj == null) {

			hasReadInstructions = false;
			Obj = this;

		} else {

			Destroy (this.gameObject);

		}

		DontDestroyOnLoad (this.gameObject);
			
	}


}
