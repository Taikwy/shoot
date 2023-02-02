using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    EnemyManager enemyManager;
    WaveSpawner waveSpawner;
    public GameObject waveHolderPrefab;

    [Header("Wave Info")]
    public GameObject enemyPrefab;
    [HideInInspector] public int numOfEnemiesInWave;
    
    [Header("Spawning Info")]
    public int numEnemies;
    public enum SPAWNTYPE{
        HORIZONTAL,
        VERTICAL,
        DIAGONAL,
        VSTREAM,
        DSTREAM,
        STATIC
    }
    public SPAWNTYPE spawnType;
    
    public float xGap, yGap, spawnInterval;
    public bool mirrored;

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Spawnpoint")){
            GameObject waveHolder = Instantiate(waveHolderPrefab);
            waveHolder.name = gameObject.name + " Holder";

            waveSpawner = WaveSpawner.Instance;
            SpawnWave(waveHolder, gameObject.transform.position);

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            // gameObject.SetActive(false);
        }
    }

    public void SpawnWave(GameObject waveHolder, Vector2 spawnPosition){
        if(mirrored)
            xGap *= -1;
        switch(spawnType){
            case SPAWNTYPE.HORIZONTAL:
                waveSpawner.SpawnHorizontalLine(waveHolder, enemyPrefab, numEnemies, xGap, spawnPosition.x);
                break;
            case SPAWNTYPE.VERTICAL:
                waveSpawner.SpawnVerticalLine(waveHolder, enemyPrefab, numEnemies, yGap, spawnPosition.x, spawnPosition.y);
                break;
            case SPAWNTYPE.DIAGONAL:
                waveSpawner.SpawnDiagonalLine(waveHolder, enemyPrefab, numEnemies, xGap, yGap, spawnPosition.x, spawnPosition.y);
                break;
            case SPAWNTYPE.VSTREAM:
                StartCoroutine(waveSpawner.SpawnStream(waveHolder, enemyPrefab, numEnemies, 0, spawnInterval, spawnPosition.x, spawnPosition.y));
                break;
            case SPAWNTYPE.DSTREAM:
                StartCoroutine(waveSpawner.SpawnStream(waveHolder, enemyPrefab, numEnemies, xGap, spawnInterval, spawnPosition.x, spawnPosition.y));
                break;
            case SPAWNTYPE.STATIC:
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

        WaveHolder waveHolderScript = gameObject.GetComponent<WaveHolder>();
        PoolManager.Instance.CreatePool(enemyPrefab, numEnemies, "enemy");
        
        GameObject enemy;
        EnemyScript enemyScript;
        Vector2 spawnLocation = new Vector2(xCenterSpawnPoint, yBottomSpawnPoint);
        for(int r = 0; r < staticArray.GetLength(0); r++){
            spawnLocation.x -= (numEnemies - 1) * xGap / 2;
            for(int c = 0; c < staticArray.GetLength(1); c++){
                enemy = waveSpawner.SpawnEnemy(enemyPrefab, spawnLocation, gameObject, true);
                enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
                enemyScript.waveHolderScript = waveHolderScript;
                
                waveHolderScript.numOfEnemiesInWave++;
                spawnLocation.x += xGap;
            }
            spawnLocation.y += yGap;
        }
    }
}
