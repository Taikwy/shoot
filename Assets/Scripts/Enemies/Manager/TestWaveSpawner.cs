using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWaveSpawner : MonoBehaviour
{
    static TestWaveSpawner _instance;
    public static TestWaveSpawner Instance { get { return _instance; } } // static singleton

    void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    public TestEnemyManager testEnemyManager;
    public Transform spawnPoint;

    public void SpawnWave(GameObject waveHolderPrefab, string waveHolderName){
        // GameObject waveHolder = Instantiate(waveHolderPrefab, testEnemyManager.enemyWavesTransform);
        // waveHolder.name = waveHolderName;

        // TestWaveData testData = waveHolder.GetComponent<TestWaveHolder>().testData;
        // PoolManager.Instance.CreatePool(testData.enemyPrefab, testData.numEnemies);
        // SpawnHorizontalLine(testEnemyManager.spawnPoint.position, 5, testData);

        // GameObject enemy;
        // EnemyScript enemyScript;
        // Vector2 spawnLocation = testData.centerSpawnPoint;
        // spawnLocation.x -= (testData.numEnemies - 1) * testData.gapBetween / 2;
        // for(int i = 0; i < testData.numEnemies; i++){
        //     enemy = SpawnEnemy(testData.enemyPrefab, spawnLocation);
        //     enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
        //     enemyScript.partOfWave = true;
        //     // enemyScript.respawnable = true;
        //     // enemyScript.originalSpawnPosition = spawnLocation;
        //     // enemyScript.respawnsLeft = testData.numRespawns;
            
        //     enemy.transform.parent = waveHolder.transform;
        //     spawnLocation.x += testData.gapBetween;
        // }
    }

    public GameObject SpawnEnemy(GameObject enemyPrefab, Vector2 spawnLocation)
    {
        Debug.Log("Spawning at " + spawnLocation);
        GameObject enemy = PoolManager.Instance.ReuseObject(enemyPrefab, spawnLocation, Quaternion.identity);
        testEnemyManager.numOfEnemiesInScene++;
        return enemy;
    }

    public GameObject SpawnEnemy(GameObject enemyPrefab, Vector2 spawnLocation, GameObject waveHolder, bool partOfWave)
    {
        // Debug.Log("Spawning at " + spawnLocation);
        GameObject enemy = PoolManager.Instance.ReuseObject(enemyPrefab, spawnLocation, Quaternion.identity);
        EnemyScript enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
        enemyScript.partOfWave = partOfWave;
        enemy.transform.parent = waveHolder.transform;

        testEnemyManager.numOfEnemiesInScene++;
        return enemy;
    }

    public IEnumerator SpawnNumEnemies(GameObject enemyPrefab, Vector2 spawnLocation, float spawnInterval, int numEnemies)
    {
        for(int i = 0 ;i < numEnemies; i++){
            SpawnEnemy(enemyPrefab, spawnLocation);
            // StartCoroutine(enemyScript.ShootTowardPlayer(delay, numOfBullets, bulletSpeed));

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public IEnumerator SpawnWaveEnemies(Vector2 spawnLocation, TestWaveData data, GameObject waveHolder)
    {
        // GameObject enemy;
        GameObject enemy;
        EnemyScript enemyScript;
        for(int i = 0 ; i < data.numEnemies; i++){
            enemy = SpawnEnemy(data.enemyPrefab, spawnLocation);
            enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
            enemyScript.partOfWave = true;
            // enemyScript.respawnable = true;
            // enemyScript.originalSpawnPosition = spawnLocation;
            // enemyScript.respawnsLeft = testData.numRespawns;
            
            enemy.transform.parent = waveHolder.transform;
            // enemies.Add(enemy);

            yield return new WaitForSeconds(data.spawnInterval);
        }
    }
    
    // public IEnumerator SpawnEnemiesRandom(GameObject enemyPrefab, Vector2 spawnCenter, float xRadius)
    // {
    //     Vector2 spawnLocation = spawnCenter;
    //     while(true){
    //         spawnLocation.x = Random.Range(spawnCenter.x + xRadius, spawnCenter.x - xRadius);
    //         Debug.Log("Spawning at " + spawnLocation);
    //         GameObject enemy = PoolManager.Instance.ReuseObject(enemyPrefab, spawnLocation, Quaternion.identity);
    //         // StartCoroutine(enemyScript.ShootTowardPlayer(delay, numOfBullets, bulletSpeed));

    //         yield return new WaitForSeconds(enemyInterval);
    //         numOfEnemiesInScene++;
    //     }
    // }

    public void SpawnEnemyWave(Vector2 spawnLocation, TestWaveData data){
        PoolManager.Instance.CreatePool(data.enemyPrefab, data.numEnemies);
        // List<GameObject> waveEnemies = new List<GameObject>(); 
        // List<PoolManager.ObjectInstance> waveEnemyInstances = new List<PoolManager.ObjectInstance>(); 
        
        GameObject waveHolder = new GameObject("Test Wave");
        waveHolder.transform.parent = transform;

        StartCoroutine(SpawnWaveEnemies(spawnLocation, data, waveHolder));
    }

    public void OldSpawnHorizontalLine(Vector2 leftMostSpawnLocation, int numEnemies, TestWaveData data){
        PoolManager.Instance.CreatePool(data.enemyPrefab, data.numEnemies);
        // List<GameObject> waveEnemies = new List<GameObject>(); 
        
        GameObject waveHolder = new GameObject("Test Horizontal Line");
        waveHolder.transform.parent = transform;

        GameObject enemy;
        EnemyScript enemyScript;
        Vector2 spawnLocation = leftMostSpawnLocation;
        for(int i = 0; i < numEnemies; i++){
            enemy = SpawnEnemy(data.enemyPrefab, spawnLocation);
            enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
            enemyScript.partOfWave = true;
            // enemyScript.respawnable = true;
            // enemyScript.originalSpawnPosition = spawnLocation;
            // enemyScript.respawnsLeft = testData.numRespawns;
            
            enemy.transform.parent = waveHolder.transform;
            spawnLocation.x += 4;
        }
    }

    public void SpawnHorizontalLine(GameObject waveHolder, TestWaveData data, float xCenterSpawnPoint){
        TestWaveHolder waveHolderScript = waveHolder.GetComponent<TestWaveHolder>();
        PoolManager.Instance.CreatePool(data.enemyPrefab, data.numEnemies, "enemy");
        
        GameObject enemy;
        EnemyScript enemyScript;
        Vector2 spawnLocation = new Vector2(xCenterSpawnPoint, spawnPoint.position.y);
        spawnLocation.x -= (data.numEnemies - 1) * data.gapBetween / 2;
        for(int i = 0; i < data.numEnemies; i++){
            enemy = SpawnEnemy(data.enemyPrefab, spawnLocation);
            enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
            enemyScript.partOfWave = true;
            enemyScript.waveHolderScript = waveHolderScript;
            
            waveHolderScript.numOfEnemiesInWave++;
            enemy.transform.parent = waveHolder.transform;
            spawnLocation.x += data.gapBetween;
        }
        Debug.Log(xCenterSpawnPoint);
    }

    public void SpawnHorizontalLine(GameObject waveHolder, GameObject enemyPrefab, int numEnemies, float xGap, float xCenterSpawnPoint = 0, float yBottomPoint = 0){
        TestWaveHolder waveHolderScript = waveHolder.GetComponent<TestWaveHolder>();
        PoolManager.Instance.CreatePool(enemyPrefab, numEnemies, "enemy");
        
        GameObject enemy;
        EnemyScript enemyScript;
        Vector2 spawnLocation = new Vector2(xCenterSpawnPoint, spawnPoint.position.y);
        spawnLocation.x -= (numEnemies - 1) * xGap / 2;
        for(int i = 0; i < numEnemies; i++){
            enemy = SpawnEnemy(enemyPrefab, spawnLocation, waveHolder, true);
            enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
            enemyScript.waveHolderScript = waveHolderScript;
            
            waveHolderScript.numOfEnemiesInWave++;
            spawnLocation.x += xGap;
        }
        // Debug.Log(xCenterSpawnPoint);
    }

    public void SpawnVerticalLine(GameObject waveHolder, GameObject enemyPrefab, int numEnemies, float yGap, float xCenterSpawnPoint = 0, float yBottomPoint = 0){
        TestWaveHolder waveHolderScript = waveHolder.GetComponent<TestWaveHolder>();
        PoolManager.Instance.CreatePool(enemyPrefab, numEnemies, "enemy");
        
        GameObject enemy;
        EnemyScript enemyScript;
        Vector2 spawnLocation = new Vector2(xCenterSpawnPoint, yBottomPoint);
        for(int i = 0; i < numEnemies; i++){
            enemy = SpawnEnemy(enemyPrefab, spawnLocation, waveHolder, true);
            enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
            enemyScript.waveHolderScript = waveHolderScript;
            
            waveHolderScript.numOfEnemiesInWave++;
            spawnLocation.y += yGap;
        }
    }

    public void SpawnDiagonalLine(GameObject waveHolder, GameObject enemyPrefab, int numEnemies, float xGap, float yGap, float xStartPoint = 0, float yBottomPoint = 0){
        TestWaveHolder waveHolderScript = waveHolder.GetComponent<TestWaveHolder>();
        PoolManager.Instance.CreatePool(enemyPrefab, numEnemies, "enemy");
        
        GameObject enemy;
        EnemyScript enemyScript;
        Vector2 spawnLocation = new Vector2(xStartPoint, yBottomPoint);
        for(int i = 0; i < numEnemies; i++){
            enemy = SpawnEnemy(enemyPrefab, spawnLocation, waveHolder, true);
            enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
            enemyScript.waveHolderScript = waveHolderScript;
            
            waveHolderScript.numOfEnemiesInWave++;
            spawnLocation.x += xGap;
            spawnLocation.y += yGap;
        }
    }

    public IEnumerator SpawnStream(GameObject waveHolder, GameObject enemyPrefab, int numEnemies, float xGap, float spawnInterval, float xCenterSpawnPoint = 0, float yBottomPoint = 0){
        TestWaveHolder waveHolderScript = waveHolder.GetComponent<TestWaveHolder>();
        PoolManager.Instance.CreatePool(enemyPrefab, numEnemies, "enemy");

        GameObject enemy;
        EnemyScript enemyScript;
        Vector2 spawnLocation = new Vector2(xCenterSpawnPoint, yBottomPoint);
        for(int i = 0 ; i < numEnemies; i++){
            enemy = SpawnEnemy(enemyPrefab, spawnLocation, waveHolder, true);
            enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
            enemyScript.waveHolderScript = waveHolderScript;
            
            waveHolderScript.numOfEnemiesInWave++;
            spawnLocation.x += xGap;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
