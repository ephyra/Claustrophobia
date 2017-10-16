using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

    public GameObject enemy;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public float spawnCooldown;
    public float maxCooldown;
    public float minCooldown;
    public float padding;
    public int startWait;
    public bool stopSpawn;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject bottomWall;
    public GameObject topWall;

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawner());
        leftWall = GameObject.Find("LeftWall");
        rightWall = GameObject.Find("RightWall");
        bottomWall = GameObject.Find("BottomWall");
        topWall = GameObject.Find("TopWall");

    }
	
	// Update is called once per frame
	void Update () {
        spawnCooldown = Random.Range(minCooldown, maxCooldown);
        minX = leftWall.transform.position.x + padding;
        maxX = rightWall.transform.position.x - padding;
        minY = bottomWall.transform.position.y + padding;
        maxY = topWall.transform.position.y - padding;

    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stopSpawn)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY,maxY), 1);

            Instantiate(enemy, spawnPosition, Quaternion.Euler(0,0,Random.Range(0.0f,360.0f)));

            yield return new WaitForSeconds(spawnCooldown);
        }
    }
}
