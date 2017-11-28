using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


    public static GameController instance;
    public GameObject[] enemies;
    Transform player;
    public Vector2 spawnValues;
    public List<EnemyController> activeEnemies;
    public Text livesText;
    public Text scoreText;

    int numberOfEnemies = 1;
    int totalEnemies = 6;
    float spawnTime = 0.8f;
    float waveTime = 4f;

    bool increasedEnemies = false;
    public int score = 0;


	// Use this for initialization
	void Awake () {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);

        activeEnemies = new List<EnemyController>();
        StartCoroutine(SpawnEnemies());

	}
	
	// Update is called once per frame
	void Update () {
		if(player == null)
        {
            GameObject playerGO = GameObject.FindWithTag("Player");
            if (playerGO != null)
            {
                player = playerGO.transform;
            }
            else
                Debug.Log("Couldn't find player");

        }

      
    }

    IEnumerator SpawnEnemies()
    {


        yield return new WaitForSeconds(spawnTime);
        while(true)
        {
            

            for(int i = 0; i < numberOfEnemies; i++)
            {
                SpawnRandomEnemy();
                yield return new WaitForSeconds(spawnTime);
            }
            increasedEnemies = false;

            float x = Mathf.Pow(1.25f, totalEnemies);
            numberOfEnemies = (int)Mathf.Floor(x);

            yield return new WaitForSeconds(waveTime);
        }
    }

    void SpawnRandomEnemy()
    {
        spawnValues.x = player.transform.position.x + 11.5f;

        int rand = Random.Range(0, enemies.Length);
        Vector2 spawnPosition = new Vector2(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y));
        GameObject newEnemy = Instantiate(enemies[rand], spawnPosition, Quaternion.identity) as GameObject;
        EnemyController newEnemyController = newEnemy.GetComponent<EnemyController>();

        totalEnemies++;

        activeEnemies.Add(newEnemyController);

    }

    public void KillAllEnemies()
    {
        while(activeEnemies.Count > 0)
        {
            activeEnemies[0].Die();
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateLivesText()
    {
        livesText.text = "Lives: " + player.GetComponent<PlayerController>().GetLives();
    }
}
