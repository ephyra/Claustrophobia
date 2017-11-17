using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

	public static SpawnEnemy Obj;

    public GameObject enemy;
    private float minX;
    private float minY;
    private float maxX;
    private float maxY;
    private float spawnCooldown;
    private float enemySpawnSpeed;
    private float speedScalingFactor;
    private int enemiesSpawned;
    public int enemiesKilled;
    public float speedScalingThreshold;
	public float intermediateSpeedScalingThreshold;
    public float minSpawnCooldownThreshold;
	public float spawnCooldownSoftCap;
    public float spawnCooldownReductionAmount;
    public float enemySpeedIncrement;
    public float currentEnemyMaxSpeed;
    public float maxCooldown;
    public float minCooldown;
    public float padding;
    public float startWait;
    public bool stopSpawn;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject bottomWall;
    public GameObject topWall;
    private enum Quadrant { bottomRight = 0, topRight = 90, topLeft = 180, bottomLeft = 270};

	public bool isPaused;

	// Use this for initialization
	void Start () {
		Obj = this;
		isPaused = false;
        speedScalingFactor = 0.9f;
        enemySpawnSpeed = currentEnemyMaxSpeed * speedScalingFactor;
        enemiesSpawned = 0;
		StartCoroutine(Spawner());

    }
	
	// Update is called once per frame
	void Update () {
        //checking wall locations, used to calibrate possible spawn coordinates
        minX = leftWall.transform.position.x + padding;
        maxX = rightWall.transform.position.x - padding;
        minY = bottomWall.transform.position.y + padding;
        maxY = topWall.transform.position.y - padding;

    }

    IEnumerator Spawner()
    {
		float elapsedTime = 0;
		while (elapsedTime < startWait) {
			if (!isPaused) {
				elapsedTime += Time.deltaTime;
			}
			yield return null;
		}

        while (!stopSpawn)
        {
            //randomize a spawncooldown and location from our spawn cooldown and location range
            spawnCooldown = Random.Range(minCooldown, maxCooldown);
            float spawnX = Random.Range(minX, maxX);
            float spawnY = Random.Range(minY, maxY);
            
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 1);
            //Quadrant is used to check where our enemy is spawning and direct its random direction of move towards further end of game map
            //This is to prevent feelings of unfairness if ghost spawns near wall and travels towards the closest wall leaving players little time to kill
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

            GameObject newSpawn = (GameObject)Instantiate(enemy, spawnPosition, Quaternion.Euler(0, 0, Random.Range((float)currQuadrant, (float)(currQuadrant + 90))));
			GameController.Obj.AddRb (newSpawn.GetComponent<Rigidbody2D> ());

			enemiesSpawned++;
            newSpawn.GetComponent<EnemyGrowAndMove>().speed = enemySpawnSpeed;
            UpdateSpawnSpeed();
            
			elapsedTime = 0;
			while (elapsedTime < spawnCooldown) {
				if (!isPaused) {
					elapsedTime += Time.deltaTime; // if not paused, start counting down
				}
				yield return null;
			}

        }
    }

    //Spawn Cooldown is reduced per enemy killed, will be called by playercontroller as and when he kills enemies
    public void ReduceSpawnCooldown()
    {
		if (enemiesKilled < spawnCooldownSoftCap) {
			maxCooldown -= spawnCooldownReductionAmount;
			minCooldown -= spawnCooldownReductionAmount;
		} else {
			maxCooldown = (1.0f + Mathf.Exp (-0.014f * enemiesKilled));
			minCooldown = (1.5f + Mathf.Exp (-0.014f * enemiesKilled));
		}
    }

    //Adaptive difficulty on enemy speed, calculation is enemiesKilled/enemiesSpawned * currentMaxSpeed, the % of enemies killed is bounded by speedScalingThreshold
    void UpdateSpawnSpeed()
    {
        currentEnemyMaxSpeed += enemySpeedIncrement;
		if (enemiesSpawned >= 10) {
			speedScalingFactor = (float)enemiesKilled / (float)enemiesSpawned;
		} else if (enemiesSpawned >= 5) {
			speedScalingFactor = Mathf.Max (((float)enemiesKilled / (float)enemiesSpawned), intermediateSpeedScalingThreshold);
		}

        if(speedScalingFactor <= speedScalingThreshold)
        {
            enemySpawnSpeed = speedScalingThreshold * currentEnemyMaxSpeed;
        } else
        {
            enemySpawnSpeed = speedScalingFactor * currentEnemyMaxSpeed;
        }

    }
}
