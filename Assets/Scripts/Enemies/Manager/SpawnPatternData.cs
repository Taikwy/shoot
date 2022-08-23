using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Pattern Data", menuName = "Spawn Pattern Data")]
public class SpawnPatternData : ScriptableObject
{
    public string spawnType;
    public float gapBetween;
    public float spawnInterval;
    public int numEnemies;
    public float respawnDelay;
}
