using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerBoundGameOver : MonoBehaviour {

	public PauseMenuController pauser;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	private void OnTriggerEnter2D(Collider2D other)
	{
		print (other.name);
		if (other.gameObject.CompareTag("Wall"))
		{
			pauser.ShowGameOverMenu ("You died of claustrophobia.");
			Time.timeScale = 0.0001f;
		}
	}
}
