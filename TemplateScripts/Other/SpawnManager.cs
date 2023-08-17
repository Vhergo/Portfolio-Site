using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public TextMeshProUGUI waveNumberText;

    public EnemyObjectPool enemyObjectPool;

    public Transform[] spawnLocation;
    public int spawnIndex;

    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    public List<GameObject> spawnedEnemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();
    }

    // Modify this method to use the object pool
    // Modify this method to use the object pool
    private void SpawnEnemy()
    {
        if (enemiesToSpawn.Count > 0)
        {
            GameObject enemy = EnemyObjectPool.instance.GetPooledEnemy();
            if (enemy == null)
            {
                Debug.LogWarning("Enemy pool is empty! You may need to increase the pool size.");
                return;
            }

            enemy.transform.position = spawnLocation[spawnIndex].position;
            enemy.SetActive(true);
            enemiesToSpawn.RemoveAt(0); // Remove from the list
            spawnedEnemies.Add(enemy);
            spawnTimer = spawnInterval;

            if (spawnIndex + 1 <= spawnLocation.Length - 1)
            {
                spawnIndex++;
            }
            else
            {
                spawnIndex = 0;
            }
        }
        else
        {
            waveTimer = 0; // If no enemies remain, end wave
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedEnemies.Count == 0 && waveTimer <= 0) {
            
            GenerateWave();
        }

        if (spawnTimer <= 0) {
            // Call the modified SpawnEnemy method
            SpawnEnemy();
        }else {
            spawnTimer -= Time.deltaTime;
            waveTimer -= Time.deltaTime;
        }
    }

    public void GenerateWave()
    {
        currWave++;
        waveValue = currWave * 10;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count; // gives a fixed time between each enemies
        waveTimer = waveDuration; // wave duration is read only

        // Update the wave number in the UI
        if (waveNumberText != null)
        {
            waveNumberText.text = "Wave: " + currWave.ToString();
        }
    }

    public void GenerateEnemies()
    {
        // Create a temporary list of enemies to generate
        // 
        // in a loop grab a random enemy 
        // see if we can afford it
        // if we can, add it to our list, and deduct the cost.

        // repeat... 

        //  -> if we have no points left, leave the loop

        List<GameObject> generatedEnemies = new List<GameObject>();

        while (waveValue > 0 && generatedEnemies.Count < 50)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;

        //Debug.Log("Current waveValue: " + waveValue);
    }

    public void KillEnemy(GameObject enemy)
    {
        // Perform any other logic related to killing the enemy

        // Return the enemy to the object pool
        EnemyObjectPool.instance.ReturnEnemyToPool(enemy);
    }

}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}