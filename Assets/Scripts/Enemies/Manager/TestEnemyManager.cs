using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyManager : MonoBehaviour
{
    static TestEnemyManager _instance;
    public static TestEnemyManager Instance { get { return _instance; } } // static singleton

    void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }
    
    public TestWaveSpawner testWaveSpawner;
    public Transform enemyWavesTransform;
    public GameObject basicEnemyPrefab;
    public GameObject altEnemyPrefab;
    public GameObject circlingEnemyPrefab;
    // public Transform spawnPoint;


    public GameObject waveHolderPrefab;
    public TestWaveData testData;
    // public GameObject waveHolder;

    float enemyInterval = 4f;
    public int numOfEnemiesInScene = 0;

    
    delegate void TestDelegate(Vector2 leftMostSpawnLocation, int numEnemies, TestWaveData data);
    TestDelegate testDelegate;

    delegate IEnumerator RespawnDelegate(GameObject enemyToSpawn, float delay = 0);
    RespawnDelegate waveDelegate, lineDelegate;


    // Start is called before the first frame update
    void Start()
    {
        PoolManager.Instance.CreatePool(basicEnemyPrefab, 10, "enemy");
        PoolManager.Instance.CreatePool(altEnemyPrefab, 10, "enemy");
        PoolManager.Instance.CreatePool(circlingEnemyPrefab, 10, "enemy");

        //StartCoroutine(SpawnNumEnemies(circlingEnemyPrefab,spawnPoint.position, .2f, 5));
        // StartCoroutine(SpawnEnemiesRandom(spawnPoint.position, 6));
        // SpawnEnemy(spawnPoint.position);

        // SpawnEnemyWave(spawnPoint.position, testData);
        // SpawnHorizontalLine(spawnPoint.position, 5, testData);


        // TestEnemyManager.Instance.testWaveSpawner.SpawnWave(waveHolderPrefab, "Test Wave Holder");

        // PoolManager.Instance.CreatePool(testData.enemyPrefab, testData.numEnemies);
        // GameObject waveHolder = Instantiate(waveHolderPrefab, transform);
        // waveHolder.name = "Test Wave Holder";

        // // testDelegate = SpawnHorizontalLine;
        // // waveHolder.GetComponent<TestWaveHolder>().SpawnEnemyWave(spawnPoint.position, testData);
        // waveHolder.GetComponent<TestWaveHolder>().SpawnHorizontalLine(spawnPoint.position, 5, testData);
    }

    //use this later as spawn patterns in level containing waves
    public IEnumerator SpawnWaveCoroutine(){
        yield return null;
    }

    public void DespawnEnemy(GameObject enemyToDespawn){
        Debug.Log("despawning " + enemyToDespawn);
        EnemyScript enemyScript = enemyToDespawn.GetComponent<EnemyScript>();
        if(enemyScript is PoolObject){
            enemyToDespawn.SetActive(false);
            enemyToDespawn.transform.parent = enemyScript.originalPoolHolder;
            if(enemyScript.partOfWave){
                //if(&& enemyScript.respawnable && enemyScript.respawnsLeft > 0)
                // enemyScript.waveHolderScript.HandleRespawn(enemyToDespawn, enemyScript);
                enemyScript.waveHolderScript.numOfEnemiesInWave--;
                // Debug.Log(enemyScript.waveHolderScript.numOfEnemiesInWave);
                if(enemyScript.waveHolderScript.numOfEnemiesInWave <= 0)
                    Destroy(enemyScript.waveHolderScript.gameObject);
            }
        }
        else{
            Destroy(enemyToDespawn.gameObject);
        }
        numOfEnemiesInScene--;
    }

    // public void RespawnTest(GameObject enemyToDespawn){
    //     Debug.Log("despawning " + enemyToDespawn);
    //     EnemyScript enemyScript = enemyToDespawn.GetComponent<EnemyScript>();
    //     if(enemyScript is PoolObject){
    //         enemyToDespawn.SetActive(false);
    //         if(enemyScript.respawnable && enemyScript.respawnsLeft > 0){
    //             StartCoroutine(RespawnAfterDelay(enemyToDespawn, testData.respawnDelay));
    //             enemyScript.respawnsLeft--;
    //         }
    //         else{
    //             enemyToDespawn.transform.parent =  enemyScript.originalPoolHolder;
    //         }
    //     }
    //     numOfEnemiesInScene--;
    // }


    //when enemy is killed this method gets called
    
    public void DestroyEnemy(GameObject enemyToDespawn){
        Debug.Log("enemy destroyted " + enemyToDespawn);
        EnemyScript enemyScript = enemyToDespawn.GetComponent<EnemyScript>();
        if(enemyScript is PoolObject){
            enemyToDespawn.SetActive(false);
            enemyToDespawn.transform.parent =  enemyScript.originalPoolHolder;

            enemyScript.waveHolderScript.numOfEnemiesInWave--;
            Debug.Log(enemyScript.waveHolderScript.numOfEnemiesInWave);
            if(enemyScript.waveHolderScript.numOfEnemiesInWave <= 0)
                Destroy(enemyScript.waveHolderScript.gameObject);
        }
        else{
            Destroy(enemyToDespawn.gameObject);
        }
        numOfEnemiesInScene--;
    }
}
