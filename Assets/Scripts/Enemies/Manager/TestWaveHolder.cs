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

}
