using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static EnemyManager _instance;
    public static EnemyManager Instance { get { return _instance; } } // static singleton

    void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }
    
    // public TestWaveSpawner testWaveSpawner;
    public Transform enemyWavesTransform;

    public int numOfEnemiesInScene = 0;

    public void DespawnEnemy(GameObject enemyToDespawn){
        Debug.Log("despawning " + enemyToDespawn);
        EnemyScript enemyScript = enemyToDespawn.GetComponent<EnemyScript>();
        if(enemyScript is PoolObject){
            enemyToDespawn.SetActive(false);
            enemyToDespawn.transform.parent = enemyScript.originalPoolHolder;
            if(enemyScript.partOfWave){
                enemyScript.waveHolderScript.numOfEnemiesInWave--;
                if(enemyScript.waveHolderScript.numOfEnemiesInWave <= 0)
                    Destroy(enemyScript.waveHolderScript.gameObject);
            }
        }
        else{
            Destroy(enemyToDespawn.gameObject);
        }
        numOfEnemiesInScene--;
    }

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
