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
    public Transform waveSpawnPoint;
    public GameObject enemyWaveHolders;

    public GameObject SpawnEnemy(GameObject enemyPrefab, Vector2 spawnLocation)
    {
        Debug.Log("Spawning at " + spawnLocation);
        GameObject enemy = PoolManager.Instance.ReuseObject(enemyPrefab, spawnLocation, Quaternion.identity);
        enemyManager.numOfEnemiesInScene++;
        return enemy;
    }
    
    //not using this one currently
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

    //Makes other spawnenemy obsolete pretty much, current one im using
    public GameObject SpawnEnemy(GameObject waveHolder, WavePiece.WavePieceInfo d, Vector2 spawnLocation, WaveHolder waveHolderScript)
    {
        GameObject enemy = SpawnEnemy(d.enemyPrefab, spawnLocation, waveHolder, true);
        // Debug.Log("spawning " + enemy + " at " + spawnLocation);

        EnemyScript enemyScript = enemy.gameObject.GetComponent<EnemyScript>();
        enemyScript.waveHolderScript = waveHolderScript;
        waveHolderScript.numOfEnemiesInWave++;

        GameObject movementSequence = d.movementSequence;
        GameObject attackSequence = d.attackSequence;

        movementSequence = PoolManager.Instance.ReuseObject(d.movementSequence, Vector2.zero, Quaternion.identity);
        movementSequence.transform.parent = enemyScript.MovementHolder.transform;        
        enemy.gameObject.GetComponent<MovementPattern>().spawnSequence = movementSequence.GetComponent<MovementSequence>();

        //If there's no attack sequence, it means it's using the default attacks
        if(attackSequence != null){
            attackSequence = PoolManager.Instance.ReuseObject(d.attackSequence, Vector2.zero, Quaternion.identity);
            attackSequence.transform.parent = enemyScript.AttackHolder.transform;
            if(enemy.gameObject.GetComponent<AttackPattern>().mainSequences.Count <= 0)
                enemy.gameObject.GetComponent<AttackPattern>().mainSequences.Add(attackSequence.GetComponent<AttackSequence>());
            else
                enemy.gameObject.GetComponent<AttackPattern>().mainSequences[0] = attackSequence.GetComponent<AttackSequence>();
        }

        enemyScript.SetupPatterns();
        return enemy;
    }

    //Default version for spawning horizontal line
    public void SpawnHorizontalLine(GameObject waveHolder, WavePiece.WavePieceInfo d){
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
        Vector2 spawnLocation = new Vector2(d.spawnPosition.x, waveSpawnPoint.position.y);
        spawnLocation.x -= (d.numEnemies - 1) * d.xGap / 2;

        for(int i = 0; i < d.numEnemies; i++){
            SpawnEnemy(waveHolder, d, spawnLocation, waveHolderScript);
            spawnLocation.x += d.xGap;
        }
        // Debug.Log(xCenterSpawnPoint);
    }
    //Uses a manual position for spawning horizointal line
    public void SpawnHorizontalLineManual(GameObject waveHolder, WavePiece.WavePieceInfo d, Vector2 manualSpawnPos){
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
        Vector2 spawnLocation = new Vector2(manualSpawnPos.x,  waveSpawnPoint.position.y);
        spawnLocation.x -= (d.numEnemies - 1) * d.xGap / 2;

        for(int i = 0; i < d.numEnemies; i++){
            SpawnEnemy(waveHolder, d, spawnLocation, waveHolderScript);
            spawnLocation.x += d.xGap;
        }
        // Debug.Log(xCenterSpawnPoint);
    }

    public void SpawnHorizontalLine(GameObject waveHolder, GameObject enemyPrefab, int numEnemies, float xGap, float xCenterSpawnPoint = 0, float yBottomPoint = 0){
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
        PoolManager.Instance.CreatePool(enemyPrefab, numEnemies, "enemy");
        
        GameObject enemy;
        EnemyScript enemyScript;
        Vector2 spawnLocation = new Vector2(xCenterSpawnPoint, waveSpawnPoint.position.y);
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

    public void SpawnVerticalLine(GameObject waveHolder, WavePiece.WavePieceInfo d){
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
        Vector2 spawnLocation = new Vector2(d.spawnPosition.x, waveSpawnPoint.position.y);

        for(int i = 0; i < d.numEnemies; i++){
            SpawnEnemy(waveHolder, d, spawnLocation, waveHolderScript);
            spawnLocation.y += d.yGap;
        }
    }
    public void SpawnVerticalLineManual(GameObject waveHolder, WavePiece.WavePieceInfo d, Vector2 manualSpawnPos){
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
        Vector2 spawnLocation = new Vector2(manualSpawnPos.x, waveSpawnPoint.position.y);

        for(int i = 0; i < d.numEnemies; i++){
            SpawnEnemy(waveHolder, d, spawnLocation, waveHolderScript);
            spawnLocation.y += d.yGap;
        }
    }


    public void SpawnDiagonalLine(GameObject waveHolder, WavePiece.WavePieceInfo d){
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
        Vector2 spawnLocation = new Vector2(d.spawnPosition.x, waveSpawnPoint.position.y);

        for(int i = 0; i < d.numEnemies; i++){
            SpawnEnemy(waveHolder, d, spawnLocation, waveHolderScript);
            spawnLocation.x += d.xGap;
            spawnLocation.y += d.yGap;
        }
    }
    public void SpawnDiagonalLineManual(GameObject waveHolder, WavePiece.WavePieceInfo d, Vector2 manualSpawnPos){
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
        Vector2 spawnLocation = new Vector2(manualSpawnPos.x, waveSpawnPoint.position.y);

        for(int i = 0; i < d.numEnemies; i++){
            SpawnEnemy(waveHolder, d, spawnLocation, waveHolderScript);
            spawnLocation.x += d.xGap;
            spawnLocation.y += d.yGap;
        }
    }

    public IEnumerator SpawnStream(GameObject waveHolder, WavePiece.WavePieceInfo d){
        // Debug.Log("stream " + d.numEnemies);
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
        Vector2 spawnLocation = new Vector2(d.spawnPosition.x, waveSpawnPoint.position.y);

        for(int i = 0; i < d.numEnemies; i++){
            SpawnEnemy(waveHolder, d, spawnLocation, waveHolderScript);
            spawnLocation.x += d.xGap;
            yield return new WaitForSeconds(d.spawnInterval);
        }
    }
    public IEnumerator SpawnStreamHalfManual(GameObject waveHolder, WavePiece.WavePieceInfo d, Vector2 manualSpawnPos){
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
        Vector2 spawnLocation = new Vector2(manualSpawnPos.x, waveSpawnPoint.position.y);

        for(int i = 0; i < d.numEnemies; i++){
            SpawnEnemy(waveHolder, d, spawnLocation, waveHolderScript);
            spawnLocation.x += d.xGap;
            yield return new WaitForSeconds(d.spawnInterval);
        }
    }

    public IEnumerator SpawnStreamFullManual(GameObject waveHolder, WavePiece.WavePieceInfo d, Vector2 manualSpawnPos){
        WaveHolder waveHolderScript = waveHolder.GetComponent<WaveHolder>();
        Vector2 spawnLocation = new Vector2(manualSpawnPos.x, manualSpawnPos.y);

        for(int i = 0; i < d.numEnemies; i++){
            SpawnEnemy(waveHolder, d, spawnLocation, waveHolderScript);
            spawnLocation.x += d.xGap;
            yield return new WaitForSeconds(d.spawnInterval);
        }
    }

    public void SpawnStatic(GameObject waveHolder, WavePiece.WavePieceInfo d){
        int[,] staticArray = new int[3,7];
        staticArray = new int[,] {
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0}
        };
        int numEnemies = staticArray.GetLength(0) * staticArray.GetLength(1);

        WaveHolder waveHolderScript = gameObject.GetComponent<WaveHolder>();
        Vector2 spawnLocation = new Vector2(d.spawnPosition.x, waveSpawnPoint.position.y);


        for(int r = 0; r < staticArray.GetLength(0); r++){
            spawnLocation.x -= (d.numEnemies - 1) * d.xGap / 2;
            for(int c = 0; c < staticArray.GetLength(1); c++){
                spawnLocation.x += d.xGap;
            }
            spawnLocation.y += d.yGap;
        }
    }
}
