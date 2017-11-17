using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerBoundGameOver : MonoBehaviour {

	public PauseMenuController pauser;

	private void OnTriggerEnter2D(Collider2D other)
	{
		print (other.name);
		if (other.gameObject.CompareTag("Wall"))
		{
			pauser.ShowGameOverMenu ("You died of claustrophobia.");
			SpawnEnemy.Obj.isPaused = true;
			pauser.gameIsPaused = true;
			GameController.Obj.SaveRigidbodies ();
		}
	}
}
