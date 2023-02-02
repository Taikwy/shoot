using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHolder : MonoBehaviour
{
    // GameObject originalPoolHolder;
    int respawnCount;                           // 0 stands for the first time the wave got spawned
    bool respawnsDuringWave;                    //Even when the wave hasn't fully died/respawned, it still respawns enemies
    bool compressesRespawn;                     //Pushes enemies up when respawning so no gaps occur


    [HideInInspector] public int numOfEnemiesInWave, numEnemies;
}
