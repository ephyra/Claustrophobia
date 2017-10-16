using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float rotSpeed;
    private Rigidbody2D rb2d;
    private float initZ;
    public float maxSpeed;

    // Use this for initialization
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();

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
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //perform game over screen
            //Destroy(gameObject);
            Application.LoadLevel(0);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            //destroy enemy
            Destroy(collision.gameObject);
        }
    }
}
