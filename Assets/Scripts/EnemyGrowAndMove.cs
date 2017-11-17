	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrowAndMove : MonoBehaviour {

    public float speed;
    public float growth;
    public float maxSize;
    public float growthInterval;
    public bool stillGrowing = true;
    public float wallMovement;
	public bool canMove;
	public LightingScript myLight;
	public AudioSource walls;

	// Use this for initialization
	void Start () {
        StartCoroutine(Grow());
		canMove = true;
    }
	
	// Update is called once per frame
	void Update () {
        //if the spawned enemy has not reach max size, continue growing
		if (transform.localScale.x >= maxSize && stillGrowing == true) {
			StopCoroutine (Grow ());
			stillGrowing = false;
			//if reached max size, start moving
		} else if (stillGrowing == false && canMove) {
			transform.position -= transform.up * Time.deltaTime * speed;
		} else {
			// Don't do anything
		}
    }

    //Used for growing animation of newly spawned enemy units
    IEnumerator Grow()
    {
        
        while (true)
        {
			float elapsedTime = 0;
			while (elapsedTime < growthInterval) {
				if (canMove) {
					elapsedTime += Time.deltaTime;
				}
				yield return null;
			}
            if(transform.localScale.x < maxSize)
            {
                transform.localScale += new Vector3(growth, growth, growth);
            }
            
        }
        
    }

    //Used for setting speed of enemy, will be called by spawner script
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    //used to check for collision with walls to move them closer
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string objectName = collision.gameObject.name;
        switch (objectName)
        {
			case "Enemy(Clone)":
				Physics2D.IgnoreCollision (this.GetComponent<Collider2D>(), collision.collider);
				break;
			case "LeftWall":
				//remove from game controller rb list
				GameController.Obj.RemoveRb (this.GetComponent<Rigidbody2D> ());
				Destroy (gameObject);
				collision.gameObject.transform.localPosition += new Vector3 (wallMovement, 0, 0);
				myLight = Object.FindObjectOfType<LightingScript> ();
				walls = collision.gameObject.GetComponent<AudioSource> ();
				myLight.shiftLight (wallMovement / 2, "Left");
				walls.PlayOneShot (walls.clip, 1.0f);
                break;
            case "RightWall":
				//remove from game controller rb list
				GameController.Obj.RemoveRb (this.GetComponent<Rigidbody2D> ());
                Destroy(gameObject);
                collision.gameObject.transform.localPosition -= new Vector3(wallMovement, 0, 0);
				myLight = Object.FindObjectOfType<LightingScript> ();
				walls = collision.gameObject.GetComponent<AudioSource> ();
				myLight.shiftLight (wallMovement / 2, "Right");
				walls.PlayOneShot (walls.clip, 1.0f);
                break;
            case "BottomWall":
				//remove from game controller rb list
				GameController.Obj.RemoveRb(this.GetComponent<Rigidbody2D> ());
                Destroy(gameObject);
                collision.gameObject.transform.localPosition += new Vector3(0, wallMovement, 0);
				myLight = Object.FindObjectOfType<LightingScript> ();
				walls = collision.gameObject.GetComponent<AudioSource> ();
				myLight.shiftLight (wallMovement / 2, "Bottom");
				walls.PlayOneShot (walls.clip, 1.0f);
				break;
            case "TopWall":
				//remove from game controller rb list
				GameController.Obj.RemoveRb (this.GetComponent<Rigidbody2D> ());
                Destroy(gameObject);
                collision.gameObject.transform.localPosition -= new Vector3(0, wallMovement, 0);
				myLight = Object.FindObjectOfType<LightingScript> ();
				walls = collision.gameObject.GetComponent<AudioSource> ();
				myLight.shiftLight (wallMovement / 2, "Top");
				walls.PlayOneShot (walls.clip, 1.0f);
                break;
            default:
                break;

        }
    }



}
