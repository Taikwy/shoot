using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePiece : MonoBehaviour
{
    WaveSpawner waveSpawner;
    public GameObject waveHolderPrefab;
    public Vector2 spawnPoint;
    public bool absoluteSpawn = false;

    [Header("Wave Piece Object")]
    public WavePieceInfo wavePieceInfo;
    [HideInInspector] public int numOfEnemiesInWave;
    
    [System.Serializable] public class WavePieceInfo{
        [Header("Enemy Info")]
        public GameObject enemyPrefab;
        public GameObject attackSequence, movementSequence;
        
        public enum SPAWNTYPE{
            HORIZONTAL,
            VERTICAL,
            DIAGONAL,
            STREAM,
            MANUALSTREAM,
            STATIC
        }
        
        [Header("Wave Info")]
        public SPAWNTYPE spawnType;
        
        public int numEnemies;
        public Vector2 spawnPosition;
        public float xGap, yGap, spawnInterval;
        public bool mirrored;

        public int[,] staticArray;
    }

    void Start(){
        // spawnPoint.x = gameObject.transform.position.x;
        // if(wavePieceInfo.spawnType != WavePieceInfo.SPAWNTYPE.MANUALSTREAM)
        //     spawnPoint = gameObject.transform.position;
    }
    
    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Spawnpoint")){
            waveSpawner = WaveSpawner.Instance;

            GameObject waveHolder = Instantiate(waveHolderPrefab);
            waveHolder.name = gameObject.transform.parent.gameObject.name + " Holder";
            waveHolder.transform.parent = waveSpawner.enemyWaveHolders.gameObject.transform;

            // if(wavePieceSpawnPoint.x < 100)
            //     SpawnPiece(waveHolder, wavePieceData, wavePieceSpawnPoint);
            // else
            //     SpawnPiece(waveHolder, wavePieceData);
            SpawnPiece(waveHolder);

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void SpawnPiece(GameObject waveHolder){

        if(absoluteSpawn){
            Debug.Log("spawnign piece as " + wavePieceInfo.spawnType);
            switch(wavePieceInfo.spawnType){
                case WavePieceInfo.SPAWNTYPE.HORIZONTAL:
                    waveSpawner.SpawnHorizontalLine(waveHolder, wavePieceInfo);
                    break;
                case WavePieceInfo.SPAWNTYPE.VERTICAL:
                    waveSpawner.SpawnVerticalLine(waveHolder, wavePieceInfo);
                    break;
                case WavePieceInfo.SPAWNTYPE.DIAGONAL:
                    waveSpawner.SpawnDiagonalLine(waveHolder, wavePieceInfo);
                    break;
                case WavePieceInfo.SPAWNTYPE.STREAM:
                    StartCoroutine(waveSpawner.SpawnStream(waveHolder, wavePieceInfo));
                    break;
                case WavePieceInfo.SPAWNTYPE.MANUALSTREAM:
                    Debug.Log("Cannot spawn using fullmanualstream without a manual position");
                    break;
            }
        }
        else{
            Debug.Log("spawnign piece using piece position");
            switch(wavePieceInfo.spawnType){
                case WavePieceInfo.SPAWNTYPE.HORIZONTAL:
                    waveSpawner.SpawnHorizontalLineManual(waveHolder, wavePieceInfo, spawnPoint);
                    break;
                case WavePieceInfo.SPAWNTYPE.VERTICAL:
                    waveSpawner.SpawnVerticalLineManual(waveHolder, wavePieceInfo, spawnPoint);
                    break;
                case WavePieceInfo.SPAWNTYPE.DIAGONAL:
                    waveSpawner.SpawnDiagonalLineManual(waveHolder, wavePieceInfo, spawnPoint);
                    break;
                case WavePieceInfo.SPAWNTYPE.STREAM:
                    StartCoroutine(waveSpawner.SpawnStreamHalfManual(waveHolder, wavePieceInfo, spawnPoint));
                    break;
                case WavePieceInfo.SPAWNTYPE.MANUALSTREAM:
                    StartCoroutine(waveSpawner.SpawnStreamFullManual(waveHolder, wavePieceInfo, spawnPoint));
                    break;
            }
        }
        
    }


    // public void SpawnPiece(GameObject waveHolder, WavePieceData pieceData){
    //     Debug.Log("spawnign piece");
    //     switch(pieceData.spawnType){
    //         case WavePieceData.SPAWNTYPE.HORIZONTAL:
    //             waveSpawner.SpawnHorizontalLine(waveHolder, pieceData);
    //             break;
    //         case WavePieceData.SPAWNTYPE.VERTICAL:
    //             waveSpawner.SpawnVerticalLine(waveHolder, pieceData);
    //             break;
    //         case WavePieceData.SPAWNTYPE.DIAGONAL:
    //             waveSpawner.SpawnDiagonalLine(waveHolder, pieceData);
    //             break;
    //         case WavePieceData.SPAWNTYPE.STREAM:
    //             StartCoroutine(waveSpawner.SpawnStream(waveHolder, pieceData));
    //             break;
    //         case WavePieceData.SPAWNTYPE.MANUALSTREAM:
    //             Debug.Log("Cannot spawn using fullmanualstream without a manual position");
    //             break;
    //     }
    // }

    // public void SpawnPiece(GameObject waveHolder, WavePieceData pieceData, Vector2 spawnPos){
    //     Debug.Log("spawnign piece w position");
    //     switch(pieceData.spawnType){
    //         case WavePieceData.SPAWNTYPE.HORIZONTAL:
    //             waveSpawner.SpawnHorizontalLineManual(waveHolder, pieceData, spawnPos);
    //             break;
    //         case WavePieceData.SPAWNTYPE.VERTICAL:
    //             waveSpawner.SpawnVerticalLineManual(waveHolder, pieceData, spawnPos);
    //             break;
    //         case WavePieceData.SPAWNTYPE.DIAGONAL:
    //             waveSpawner.SpawnDiagonalLineManual(waveHolder, pieceData, spawnPos);
    //             break;
    //         case WavePieceData.SPAWNTYPE.STREAM:
    //             StartCoroutine(waveSpawner.SpawnStreamHalfManual(waveHolder, pieceData, spawnPos));
    //             break;
    //         case WavePieceData.SPAWNTYPE.MANUALSTREAM:
    //             StartCoroutine(waveSpawner.SpawnStreamFullManual(waveHolder, pieceData, spawnPos));
    //             break;
    //     }
    // }




}
