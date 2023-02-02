using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    EnemyManager enemyManager;
    WaveSpawner waveSpawner;
    public GameObject waveHolderPrefab;

    
    public List<WavePieceData> wavePieces = new List<WavePieceData>();

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
            SpawnWave(waveHolder);

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void SpawnWave(GameObject waveHolder){
        foreach(WavePieceData pieceData in wavePieces){
            SpawnPiece(waveHolder, pieceData);
        }
    }

    public void SpawnPiece(GameObject waveHolder, WavePieceData pieceData){
        Debug.Log("spawnign piece");
        if(mirrored)
            xGap *= -1;
        switch(spawnType){
            case SPAWNTYPE.HORIZONTAL:
                waveSpawner.SpawnHorizontalLine(waveHolder, pieceData);
                break;
            // case SPAWNTYPE.VERTICAL:
            //     waveSpawner.SpawnVerticalLine(waveHolder, enemyPrefab, numEnemies, yGap, spawnPosition.x, spawnPosition.y);
            //     break;
            // case SPAWNTYPE.DIAGONAL:
            //     waveSpawner.SpawnDiagonalLine(waveHolder, enemyPrefab, numEnemies, xGap, yGap, spawnPosition.x, spawnPosition.y);
            //     break;
            // case SPAWNTYPE.VSTREAM:
            //     StartCoroutine(waveSpawner.SpawnStream(waveHolder, enemyPrefab, numEnemies, 0, spawnInterval, spawnPosition.x, spawnPosition.y));
            //     break;
            // case SPAWNTYPE.DSTREAM:
            //     StartCoroutine(waveSpawner.SpawnStream(waveHolder, enemyPrefab, numEnemies, xGap, spawnInterval, spawnPosition.x, spawnPosition.y));
            //     break;
            // case SPAWNTYPE.STATIC:
            //     waveSpawner.SpawnStatic(waveHolder, enemyPrefab, xGap, yGap, spawnPosition.x, spawnPosition.y);
            //     break;
            // default:
            //     break;
        }
    }

    public void SpawnPiece(GameObject waveHolder, Vector2 spawnPosition){
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
                waveSpawner.SpawnStatic(waveHolder, enemyPrefab, xGap, yGap, spawnPosition.x, spawnPosition.y);
                break;
            default:
                break;
        }
    }
}
