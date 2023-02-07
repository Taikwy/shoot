using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Piece Data", menuName = "Wave Piece Data")]
public class WavePieceData : ScriptableObject
{
    [Header("Enemy Info")]
    public GameObject enemyPrefab;
    public GameObject attackSequence, movementSequence;
    
    public enum SPAWNTYPE{
        HORIZONTAL,
        VERTICAL,
        DIAGONAL,
        STREAM,
        VSTREAM,
        DSTREAM,
        STATIC
    }
    
    [Header("Wave Info")]
    public SPAWNTYPE spawnType;
    
    public int numEnemies;
    public Vector2 spawnPosition;
    public float xGap, yGap, spawnInterval;
    public bool mirrored;

    public int[,] staticArray;
        // staticArray = new int[,] {
        //     {0,0,0,0,0,0,0},
        //     {0,0,0,0,0,0,0},
        //     {0,0,0,0,0,0,0}
        // };    
}
