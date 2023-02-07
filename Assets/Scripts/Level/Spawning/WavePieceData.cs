using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Piece Data", menuName = "Wave Piece Data")]
public class WavePieceData : ScriptableObject
{
    [Header("Wave Info")]
    public GameObject enemyPrefab;
    public enum SPAWNTYPE{
        HORIZONTAL,
        VERTICAL,
        DIAGONAL,
        VSTREAM,
        DSTREAM,
        STATIC
    }
    public SPAWNTYPE spawnType;
    
    public int numEnemies;
    public Vector2 spawnPosition;
    public float xGap, yGap, spawnInterval;
    public bool mirrored;

    public GameObject attackSequence;
    public GameObject movementSequence;

    // public AttackSequence pieceAttackSequence;
    // public MovementSequence pieceMovementSequence;
    
}
