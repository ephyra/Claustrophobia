﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float rotSpeed;
    private Rigidbody2D rb2d;
    private float initZ;
    public float maxSpeed;
    public SpawnEnemy spawner;

	public PauseMenuController pauser;
	AudioSource audio;
	public bool isPaused;

    // Use this for initialization
    void Start() {

		audio = GetComponent<AudioSource> ();
		isPaused = false;
		Time.timeScale = 1.0f;
        rb2d = GetComponent<Rigidbody2D>();
		print (rb2d);
        print( GetComponent<Renderer>().bounds.size);
		pauser.HidePauseMenu ();
    }
    
    void FixedUpdate()
	{
		float rotateFactor = pauser.gameIsPaused ? 0 : Input.GetAxis ("Horizontal");
        float moveForward = Input.GetAxis("Up");
        transform.rotation = Quaternion.Euler(0, 0, initZ);
        initZ -= rotateFactor * rotSpeed;
		rb2d.AddForce(transform.right * speed * moveForward);
        if(rb2d.velocity.magnitude >= maxSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
        }
    }

    // Update is called once per frame
    void Update () {
		//Pause function
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
			if (!isPaused) {
				PauseGame ();
			} else {
				UnpauseGame ();
			}
		}
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
			GameController.Obj.PlayerDeathNoise ();
			pauser.ShowGameOverMenu ("You died from touching a wall.");
			isPaused = true;
			SpawnEnemy.Obj.isPaused = true;
			GameController.Obj.SaveRigidbodies ();
			pauser.gameIsPaused = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
			//remove from game controller rb list
			collision.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
			collision.gameObject.GetComponent<Collider2D> ().isTrigger = true;
			GameController.Obj.RemoveRb (collision.gameObject.GetComponent<Rigidbody2D> ());
            //destroy enemy
			collision.gameObject.GetComponent<EnemyGrowAndMove> ().canMove = false;
			StartCoroutine (CallDeathAnim (collision.gameObject));
			audio.PlayOneShot (audio.clip, 1.0f);
            spawner.ReduceSpawnCooldown();
            spawner.enemiesKilled++;
        }
    }

	private void PauseGame () {
		isPaused = true;
		SpawnEnemy.Obj.isPaused = true;
		pauser.gameIsPaused = true;
		GameController.Obj.SaveRigidbodies ();
		pauser.ShowPauseMenu ();
	}

	private void UnpauseGame () {
		isPaused = false;
		SpawnEnemy.Obj.isPaused = false;
		pauser.gameIsPaused = false;
		GameController.Obj.LoadRigidbodies ();
		pauser.HidePauseMenu ();
	}

	public IEnumerator CallDeathAnim (GameObject obj) {
		obj.GetComponent<Animator> ().Play ("ghostDeath");
		yield return new WaitForSeconds(0.6f);
		Destroy(obj);
	}
}


