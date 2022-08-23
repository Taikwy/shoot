using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWaveHolder : MonoBehaviour
{
    // GameObject originalPoolHolder;
    int respawnCount;                           // 0 stands for the first time the wave got spawned
    bool respawnsDuringWave;                    //Even when the wave hasn't fully died/respawned, it still respawns enemies
    bool compressesRespawn;                     //Pushes enemies up when respawning so no gaps occur

    // List<PoolManager.ObjectInstance> objectInstances;


    // List<GameObject> respawningEnemies = new List<GameObject>();


    public TestWaveData testData;
    TestEnemyManager testEnemyManager;
    TestWaveSpawner testWaveSpawner;

    public int numOfEnemiesInWave;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public Vector2 centerSpawnPoint;
    [Tooltip("horizontal, vertical, diagonal, vertical stream")]
    public string spawnType;
    public int numEnemies;
    public float xGap;
    public float yGap;
    public float spawnInterval;

    void Awake(){
        numOfEnemiesInWave = 0;
        testEnemyManager = TestEnemyManager.Instance;
        testWaveSpawner = TestWaveSpawner.Instance;
        gameObject.transform.parent = testEnemyManager.enemyWavesTransform;
    }

    public void SpawnWave(Vector2 spawnPosition){
        switch(spawnType){
            case "horizontal":
                testWaveSpawner.SpawnHorizontalLine(gameObject, enemyPrefab, numEnemies, xGap, spawnPosition.x);
                break;
            case "vertical":
                testWaveSpawner.SpawnVerticalLine(gameObject, enemyPrefab, numEnemies, yGap, spawnPosition.x, spawnPosition.y);
                break;
            case "diagonal":
                testWaveSpawner.SpawnDiagonalLine(gameObject, enemyPrefab, numEnemies, xGap, yGap, spawnPosition.x, spawnPosition.y);
                break;
            case "vertical stream":
                StartCoroutine(testWaveSpawner.SpawnStream(gameObject, enemyPrefab, numEnemies, 0, spawnInterval, spawnPosition.x, spawnPosition.y));
                break;
            case "diagonal stream":
                StartCoroutine(testWaveSpawner.SpawnStream(gameObject, enemyPrefab, numEnemies, xGap, spawnInterval, spawnPosition.x, spawnPosition.y));
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

    // public void RespawnTest(GameObject enemyToDespawn){
    //     Debug.Log("despawning " + enemyToDespawn);
    //     EnemyScript enemyScript = enemyToDespawn.GetComponent<EnemyScript>();
    //     if(enemyScript is PoolObject){
    //         enemyToDespawn.SetActive(false);
    //         if(enemyScript.respawnable && enemyScript.respawnsLeft > 0){
    //             StartCoroutine(RespawnAfterDelay(enemyToDespawn, testEnemyManager.testData.respawnDelay));
    //             enemyScript.respawnsLeft--;
    //         }
    //         else{
    //             enemyToDespawn.transform.parent =  enemyScript.originalPoolHolder;
    //         }
    //     }
    //     numOfEnemiesInWave--;
    //     testEnemyManager.numOfEnemiesInScene--;
    // }

    // public void RespawnLine(List<GameObject> enemiesToRespawn){
    //     // GameObject enemy;
    //     EnemyScript enemyScript;
    //     Debug.Log("respawning line " + enemiesToRespawn.Count);
    //     foreach(GameObject enemy in enemiesToRespawn){
    //         enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
    //         StartCoroutine(Respawn(enemy));
    //     }
    //     respawningEnemies.Clear();
    // }
    
    // public void HandleRespawn(GameObject enemyToDespawn, EnemyScript enemyScript){
    //     testEnemyManager.numOfEnemiesInScene--;
    //     numOfEnemiesInWave--;
    //     respawningEnemies.Add(enemyToDespawn);
    //     if(numOfEnemiesInWave <= 0){
    //         RespawnLine(respawningEnemies);
    //     }
    //     // StartCoroutine(RespawnAfterDelay(enemyToDespawn, testEnemyManager.testData.respawnDelay));
    // }

    // public IEnumerator RespawnAfterDelay(GameObject enemyToSpawn, float delay){
    //     yield return new WaitForSeconds(delay);

    //     MajorEnemyScript majorEnemyScript = enemyToSpawn.gameObject.GetComponent<MajorEnemyScript>();
    //     enemyToSpawn.transform.position = majorEnemyScript.originalSpawnPosition;
    //     enemyToSpawn.transform.rotation = testEnemyManager.spawnPoint.rotation;
    //     majorEnemyScript.respawnsLeft--;

    //     enemyToSpawn.GetComponent<PoolObject>().OnObjectRespawn();
    //     enemyToSpawn.SetActive(true);

    //     numOfEnemiesInWave++;
    //     testEnemyManager.numOfEnemiesInScene++;
    // }

    // public IEnumerator Respawn(GameObject enemyToSpawn, float delay = 0){
    //     yield return null;

    //     MajorEnemyScript majorEnemyScript = enemyToSpawn.gameObject.GetComponent<MajorEnemyScript>();
    //     enemyToSpawn.transform.position = majorEnemyScript.originalSpawnPosition;
    //     enemyToSpawn.transform.rotation = testEnemyManager.spawnPoint.rotation;
    //     majorEnemyScript.respawnsLeft--;

    //     enemyToSpawn.GetComponent<PoolObject>().OnObjectRespawn();
    //     enemyToSpawn.SetActive(true);

    //     numOfEnemiesInWave++;
    //     testEnemyManager.numOfEnemiesInScene++;
    // }
}
