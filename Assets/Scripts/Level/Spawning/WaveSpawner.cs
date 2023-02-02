using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    static WaveSpawner _instance;
    public static WaveSpawner Instance { get { return _instance; } } // static singleton

    void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    public EnemyManager enemyManager;
    public Transform spawnPoint;

    public GameObject SpawnEnemy(GameObject enemyPrefab, Vector2 spawnLocation)
    {
        Debug.Log("Spawning at " + spawnLocation);
        GameObject enemy = PoolManager.Instance.ReuseObject(enemyPrefab, spawnLocation, Quaternion.identity);
        enemyManager.numOfEnemiesInScene++;
        return enemy;
    }

    public GameObject SpawnEnemy(GameObject enemyPrefab, Vector2 spawnLocation, GameObject waveHolder, bool partOfWave)
    {
        // Debug.Log("Spawning at " + spawnLocation);
        GameObject enemy = PoolManager.Instance.ReuseObject(enemyPrefab, spawnLocation, Quaternion.identity);
        EnemyScript enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
        enemyScript.partOfWave = partOfWave;
        enemy.transform.parent = waveHolder.transform;

        enemyManager.numOfEnemiesInScene++;
        return enemy;
    }

    public void SpawnHorizontalLine(GameObject waveHolder, GameObject enemyPrefab, int numEnemies, float xGap, float xCenterSpawnPoint = 0, float yBottomPoint = 0){
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
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
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
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
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
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
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
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

    public void SpawnStatic(GameObject waveHolder, GameObject enemyPrefab, float xGap, float yGap, float xCenterSpawnPoint = 0, float yBottomSpawnPoint = 0){
        int[,] staticArray = new int[3,7];
        staticArray = new int[,] {
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0}
        };
        int numEnemies = staticArray.GetLength(0) * staticArray.GetLength(1);

        WaveHolder waveHolderScript = gameObject.GetComponent<WaveHolder>();
        PoolManager.Instance.CreatePool(enemyPrefab, numEnemies, "enemy");
        
        GameObject enemy;
        EnemyScript enemyScript;
        Vector2 spawnLocation = new Vector2(xCenterSpawnPoint, yBottomSpawnPoint);
        for(int r = 0; r < staticArray.GetLength(0); r++){
            spawnLocation.x -= (numEnemies - 1) * xGap / 2;
            for(int c = 0; c < staticArray.GetLength(1); c++){
                enemy = SpawnEnemy(enemyPrefab, spawnLocation, gameObject, true);
                enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
                enemyScript.waveHolderScript = waveHolderScript;
                
                waveHolderScript.numOfEnemiesInWave++;
                spawnLocation.x += xGap;
            }
            spawnLocation.y += yGap;
        }
    }
}
