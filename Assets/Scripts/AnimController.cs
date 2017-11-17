using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimController : MonoBehaviour
{

	public int animNo;
	public Vector2 initPos;
	public Quaternion initRot;
	public float loopTime = 2.5f;

	public GameObject secondFrameWalls;
	public GameObject thirdFrameWalls;
	public GameObject enemy;

	void Start ()
	{

		initPos = transform.position;
		initRot = transform.rotation;
	}


	void OnEnable ()
	{

		switch (animNo) {

		default:
		case 1:
			StartCoroutine (TurnAndMove ());
			break;

		case 2:
			StartCoroutine (HitWallAndDie ());
			break;

		case 3:
			StartCoroutine (EnemyDemo ());
			break;


		}


	}


	IEnumerator TurnAndMove ()
	{

		while (true) {

			for (int i = 0; i < 20; i++) {
				transform.Rotate (new Vector3 (0, 0, 2f));
				yield return new WaitForEndOfFrame ();
			}

			for (int i = 0; i < 40; i++) {
				transform.Rotate (new Vector3 (0, 0, -2f));
				yield return new WaitForEndOfFrame ();
			}

			for (int i = 0; i < 20; i++) {
				transform.Rotate (new Vector3 (0, 0, 2f));
				yield return new WaitForEndOfFrame ();
			}

			print (GetComponent<Rigidbody2D> ());
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 200f);

			print (Time.timeScale);
			yield return new WaitForSecondsRealtime (loopTime);
			transform.SetPositionAndRotation (initPos, initRot);
		}

	}

	IEnumerator HitWallAndDie ()
	{

		SpriteRenderer sr = GetComponent<SpriteRenderer> ();

		while (true) {

			GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 310f);
			print (GetComponent<Rigidbody2D> ().velocity);

			yield return new WaitForSecondsRealtime (1.0f);

			float elapsedTime = 0;
			while (sr.color.a > 0) {
				sr.color = new Color (1f, 1f, 1f, Mathf.Lerp(1f, 0, elapsedTime/0.6f));
				elapsedTime += Time.fixedUnscaledDeltaTime;
				yield return null;
			}

			yield return new WaitForSecondsRealtime (loopTime / 1.5f);
			transform.SetPositionAndRotation (initPos, initRot);
			sr.color = new Color (1f, 1f, 1f, 1f);

			foreach (Transform child in secondFrameWalls.transform) {
				child.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
				yield return new WaitForSecondsRealtime(0.45f);
			}

			elapsedTime = 0;
			while (sr.color.a > 0) {
				sr.color = new Color (1f, 1f, 1f, Mathf.Lerp(1f, 0, elapsedTime/0.6f));
				elapsedTime += Time.fixedUnscaledDeltaTime;
				yield return null;
			}


			yield return new WaitForSecondsRealtime (loopTime);

			foreach (Transform child in secondFrameWalls.transform) {
				child.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			}
			transform.SetPositionAndRotation (initPos, initRot);
			sr.color = new Color (1f, 1f, 1f, 1f);
		}

	}

	IEnumerator EnemyDemo () {

		Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D> ();
		Vector2 enemyInitPos = enemy.transform.position;
		


		while (true) {

			// 1st movement
			ShowEnemy ();

			for (int i = 0; i < 4; i++) {
				enemy.transform.localScale += new Vector3 (0.1f, 0.1f, 1f);
				yield return new WaitForSecondsRealtime (0.2f);
			}

			enemyRb.AddForce (Vector2.right * 60f);
			yield return new WaitForSecondsRealtime (1.98f);
			HideEnemy (enemyInitPos);

		
			thirdFrameWalls.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			yield return new WaitForSecondsRealtime (0.6f);


			// 2nd movement
			ShowEnemy ();

			for (int i = 0; i < 4; i++) {
				enemy.transform.localScale += new Vector3 (0.1f, 0.1f, 1f);
				yield return new WaitForSecondsRealtime (0.2f);
			}

			enemyRb.AddForce (Vector2.right * 60f);
			yield return new WaitForSecondsRealtime (1.7f);
			HideEnemy (enemyInitPos);

			thirdFrameWalls.transform.GetChild (1).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			yield return new WaitForSecondsRealtime (0.6f);

			// 3rd movement
			ShowEnemy ();
			for (int i = 0; i < 4; i++) {
				enemy.transform.localScale += new Vector3 (0.1f, 0.1f, 1f);
				yield return new WaitForSecondsRealtime (0.2f);
			}

			enemyRb.AddForce (Vector2.right * 60f);
			GetComponent<Rigidbody2D> ().AddForce (Vector2.down * 200f);


			yield return new WaitForSecondsRealtime (0.5f);
			HideEnemy (enemyInitPos);

			yield return new WaitForSecondsRealtime (loopTime);
			foreach (Transform child in thirdFrameWalls.transform) {
				child.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			}

			transform.SetPositionAndRotation (initPos, initRot);
		}

	}

	private void HideEnemy(Vector2 pos) {
		enemy.GetComponent<SpriteRenderer> ().enabled = false;
		enemy.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		enemy.transform.position = pos;
	}

	private void ShowEnemy() {
		enemy.transform.localScale = new Vector2 (0.2f, 0.2f);
		enemy.GetComponent<SpriteRenderer> ().enabled = true;
	}
}
