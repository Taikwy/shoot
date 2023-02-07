using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    EnemyManager enemyManager;
    WaveSpawner waveSpawner;
    public GameObject waveHolderPrefab;
    public List<WavePieceData> wavePieces = new List<WavePieceData>();
    [HideInInspector] public int numOfEnemiesInWave;
    

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Spawnpoint")){
            waveSpawner = WaveSpawner.Instance;

            GameObject waveHolder = Instantiate(waveHolderPrefab);
            waveHolder.name = gameObject.name + " Holder";
            waveHolder.transform.parent = waveSpawner.enemyWaveHolders.gameObject.transform;

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
        switch(pieceData.spawnType){
            case WavePieceData.SPAWNTYPE.HORIZONTAL:
                waveSpawner.SpawnHorizontalLine(waveHolder, pieceData);
                break;
            case WavePieceData.SPAWNTYPE.VERTICAL:
                waveSpawner.SpawnVerticalLine(waveHolder, pieceData);
                break;
            case WavePieceData.SPAWNTYPE.DIAGONAL:
                waveSpawner.SpawnDiagonalLine(waveHolder, pieceData);
                break;
            case WavePieceData.SPAWNTYPE.STREAM:
                StartCoroutine(waveSpawner.SpawnStream(waveHolder, pieceData));
                break;
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
        // if(mirrored)
        //     xGap *= -1;
        // switch(spawnType){
        //     case SPAWNTYPE.HORIZONTAL:
        //         waveSpawner.SpawnHorizontalLine(waveHolder, enemyPrefab, numEnemies, xGap, spawnPosition.x);
        //         break;
        //     case SPAWNTYPE.VERTICAL:
        //         waveSpawner.SpawnVerticalLine(waveHolder, enemyPrefab, numEnemies, yGap, spawnPosition.x, spawnPosition.y);
        //         break;
        //     case SPAWNTYPE.DIAGONAL:
        //         waveSpawner.SpawnDiagonalLine(waveHolder, enemyPrefab, numEnemies, xGap, yGap, spawnPosition.x, spawnPosition.y);
        //         break;
        //     case SPAWNTYPE.VSTREAM:
        //         StartCoroutine(waveSpawner.SpawnStream(waveHolder, enemyPrefab, numEnemies, 0, spawnInterval, spawnPosition.x, spawnPosition.y));
        //         break;
        //     case SPAWNTYPE.DSTREAM:
        //         StartCoroutine(waveSpawner.SpawnStream(waveHolder, enemyPrefab, numEnemies, xGap, spawnInterval, spawnPosition.x, spawnPosition.y));
        //         break;
        //     case SPAWNTYPE.STATIC:
        //         waveSpawner.SpawnStatic(waveHolder, enemyPrefab, xGap, yGap, spawnPosition.x, spawnPosition.y);
        //         break;
        //     default:
        //         break;
        // }
    }
}
