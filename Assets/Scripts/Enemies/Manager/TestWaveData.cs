using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Test Wave Data", menuName = "Test Wave Data")]
public class TestWaveData : ScriptableObject
{
    public string waveName;
    public GameObject enemyPrefab;
    // public Vector2 centerSpawnPoint;
    public int numEnemies;
    public float gapBetween;
    public float spawnInterval;
    public float respawnDelay;
    public int numRespawns;
}
