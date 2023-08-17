using System.Colleciton;
using System.Collection.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehavior
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public int spawnCost;
    }

    public int availablePoints = 10;
    public List<EnemySpawnINfo> enemyPrefabs = new List<EnemySpawnInfo>();
    private bool canFollow = true;
    private bool canShoot = false;

    void Update() {
        if (canFollow) SetDestination();
        if (canShoot) ShootPlayer();
    }

    private List<EnemySpawnInfo> DecideEnemyTypes(int currentWave) {
        List<EnemySpawnInfo> chosenEnemyTypes = new List<EnemySpawnInfo>();
        switch(currentWave) {
            case 1:
                chosenEnemyTypes.Add(enemyPrefabs[0]);
                break;
            case 2:
                chosenEnemyTypes.Add(enemyPrefabs[0]);
                chosenEnemyTypes.Add(enemyPrefabs[1]);
                break;
            case 3:
                chosenEnemyTypes.Add(enemyPrefabs[0]);
                chosenEnemyTypes.Add(enemyPrefabs[1]);
                chosenEnemyTypes.Add(enemyPrefabs[2]);
                break;
            default:
                break;
        }
        return chosenEnemyTypes;
    }

    void StartShooting() {
        canFollow = false;
        ResetPath();

        canShoot = true;
    }

    void StartFollowing() {
        canFollow = true;
        canShoot = false;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == 'Inner') {
            // DO SHOOTING LOGIC HERE
            // Call Function
            StartShooting();
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Outter") {
            // DO FOLLOW LOGIC HERE
            // Call Function
            StartFollowing();
        }
    }
}