using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController Obj;
	

	private List<Rigidbody2D> savedRbs = new List <Rigidbody2D> ();
	public Rigidbody2D playerRb;

	private Vector2 playerVelocity;
	private float playerAngularVelocity;

	void Start () {
		Obj = this;
		AddRb (playerRb);
	}


	public void SaveRigidbodies () {
		for (int i = 0; i < savedRbs.Count; i++) {
			Rigidbody2D rb = savedRbs [i];
			if (rb.gameObject.CompareTag("Enemy")) {
				rb.gameObject.GetComponent <EnemyGrowAndMove> ().canMove = false;
			}
			if (rb.gameObject.CompareTag("Player")) {
				playerVelocity = rb.velocity;
				playerAngularVelocity = rb.angularVelocity;
			}
			rb.isKinematic = true;
			rb.Sleep ();
		}
	}

	public void LoadRigidbodies () {
		for (int i = 0; i < savedRbs.Count; i++) {
			Rigidbody2D rb = savedRbs [i];
			if (rb.gameObject.CompareTag("Enemy")) {
				rb.gameObject.GetComponent <EnemyGrowAndMove> ().canMove = true;
			} 
			rb.isKinematic = false;
			rb.WakeUp ();
		}
		playerRb.velocity = playerVelocity;
		playerRb.angularVelocity = playerAngularVelocity;
	}

	public void AddRb (Rigidbody2D rb) {
		savedRbs.Add (rb);
	}

	public void RemoveRb (Rigidbody2D rb) {
		savedRbs.Remove (rb);
	}


	public void PlayerDeathNoise () {
		GetComponent<AudioSource> ().Play ();
	}

}
