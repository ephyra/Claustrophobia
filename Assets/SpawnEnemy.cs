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
    private enum Quadrant { bottomRight = 0, topRight = 90, topLeft = 180, bottomLeft = 270};

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawner());

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
            float spawnX = Random.Range(minX, maxX);
            float spawnY = Random.Range(minY, maxY);
            
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 1);
            Quadrant currQuadrant;
            if (spawnX < 0 && spawnY < 0)
            {
                currQuadrant = Quadrant.topRight;
            } else if (spawnX < 0 && spawnY > 0)
            {
                currQuadrant = Quadrant.bottomRight;
            } else if (spawnX > 0 && spawnY < 0)
            {
                currQuadrant = Quadrant.topLeft;
            } else
            {
                currQuadrant = Quadrant.bottomLeft;
            }

            print((int)currQuadrant);
            
            Instantiate(enemy, spawnPosition, Quaternion.Euler(0,0,Random.Range((float)currQuadrant,(float)(currQuadrant+90))));
            
            yield return new WaitForSeconds(spawnCooldown);
        }
    }
}
