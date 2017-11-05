using System.Collections;
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

    // Use this for initialization
    void Start() {

		pauser.HidePauseMenu ();

        rb2d = GetComponent<Rigidbody2D>();
        print( GetComponent<Renderer>().bounds.size);

    }
    
    void FixedUpdate()
    {
        float rotateFactor = Input.GetAxis("Horizontal");
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
			if (Time.timeScale == 1.0f) {
				Time.timeScale = 0.0001f;
				pauser.gameIsPaused = true;
				pauser.ShowPauseMenu ();
			} else {
				Time.timeScale = 1.0f;
				pauser.gameIsPaused = false;
				pauser.HidePauseMenu ();
			}
		}
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //perform game over screen
            //Destroy(gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            //destroy enemy
            Destroy(collision.gameObject);
            spawner.ReduceSpawnCooldown();
            spawner.enemiesKilled++;
        }
    }
}
