using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWave : MonoBehaviour
{
    TestEnemyManager testEnemyManager;
    TestWaveSpawner testWaveSpawner;
    public GameObject waveHolderPrefab;

    [Header("Wave Info")]
    public int numOfEnemiesInWave;
    public GameObject enemyPrefab;
    // public GameObject enemyPrefab2;
    // public Vector2 centerSpawnPoint;
    [Tooltip("horizontal, vertical, diagonal, vertical stream, diagonal stream")]
    public string spawnType;
    public int numEnemies;
    public float xGap;
    public float yGap;
    public float spawnInterval;

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Spawnpoint")){
            GameObject waveHolder = Instantiate(waveHolderPrefab);
            waveHolder.name = gameObject.name + " Holder";

            testWaveSpawner = TestWaveSpawner.Instance;
            SpawnWave(waveHolder, gameObject.transform.position);

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            // gameObject.SetActive(false);
        }
    }

    public void SpawnWave(GameObject waveHolder, Vector2 spawnPosition){
        switch(spawnType){
            case "horizontal":
                testWaveSpawner.SpawnHorizontalLine(waveHolder, enemyPrefab, numEnemies, xGap, spawnPosition.x);
                break;
            case "vertical":
                testWaveSpawner.SpawnVerticalLine(waveHolder, enemyPrefab, numEnemies, yGap, spawnPosition.x, spawnPosition.y);
                break;
            case "diagonal":
                testWaveSpawner.SpawnDiagonalLine(waveHolder, enemyPrefab, numEnemies, xGap, yGap, spawnPosition.x, spawnPosition.y);
                break;
            case "vertical stream":
                StartCoroutine(testWaveSpawner.SpawnStream(waveHolder, enemyPrefab, numEnemies, 0, spawnInterval, spawnPosition.x, spawnPosition.y));
                break;
            case "diagonal stream":
                StartCoroutine(testWaveSpawner.SpawnStream(waveHolder, enemyPrefab, numEnemies, xGap, spawnInterval, spawnPosition.x, spawnPosition.y));
                break;
            case "static":
                SpawnStatic(xGap, yGap, spawnPosition.x, spawnPosition.y);
                break;
            default:
                break;
        }
    }

    public void SpawnStatic(float xGap, float yGap, float xCenterSpawnPoint = 0, float yBottomSpawnPoint = 0){
        int[,] staticArray = new int[3,7];
        staticArray = new int[,] {
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0}
        };
        int numEnemies = staticArray.GetLength(0) * staticArray.GetLength(1);

        TestWaveHolder waveHolderScript = gameObject.GetComponent<TestWaveHolder>();
        PoolManager.Instance.CreatePool(enemyPrefab, numEnemies, "enemy");
        
        GameObject enemy;
        EnemyScript enemyScript;
        Vector2 spawnLocation = new Vector2(xCenterSpawnPoint, yBottomSpawnPoint);
        for(int r = 0; r < staticArray.GetLength(0); r++){
            spawnLocation.x -= (numEnemies - 1) * xGap / 2;
            for(int c = 0; c < staticArray.GetLength(1); c++){
                enemy = testWaveSpawner.SpawnEnemy(enemyPrefab, spawnLocation, gameObject, true);
                enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
                enemyScript.waveHolderScript = waveHolderScript;
                
                waveHolderScript.numOfEnemiesInWave++;
                spawnLocation.x += xGap;
            }
            spawnLocation.y += yGap;
        }
    }
}
