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

	// Use this for initialization
	void Start () {
        StartCoroutine(Grow());
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.x >= maxSize && stillGrowing == true)
        {
            print("I am done");
            StopCoroutine(Grow());
            stillGrowing = false;
        } else if(stillGrowing == false)
        {
            transform.position -= transform.up * Time.deltaTime * speed;
        }
    }

    IEnumerator Grow()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(growthInterval);
            if(transform.localScale.x < maxSize)
            {
                transform.localScale += new Vector3(growth, growth, growth);
            }
            
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string objectName = collision.gameObject.name;
        switch (objectName)
        {
            case "LeftWall":
                Destroy(gameObject);
                collision.gameObject.transform.localPosition += new Vector3(wallMovement, 0, 0);
                break;
            case "RightWall":
                Destroy(gameObject);
                collision.gameObject.transform.localPosition -= new Vector3(wallMovement, 0, 0);
                break;
            case "BottomWall":
                Destroy(gameObject);
                collision.gameObject.transform.localPosition += new Vector3(0, wallMovement, 0);
                break;
            case "TopWall":
                Destroy(gameObject);
                collision.gameObject.transform.localPosition -= new Vector3(0, wallMovement, 0);
                break;
            default:
                break;

        }
    }


}
