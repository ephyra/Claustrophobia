using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerBoundGameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        print(other.gameObject.name);
        if (other.gameObject.CompareTag("Wall"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
    }
}
