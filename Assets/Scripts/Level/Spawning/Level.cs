using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level : MonoBehaviour
{
    // [Header("waves")]
    public List<Wave> wavesInLevel = new List<Wave>();

    // [Header("enemy prefabs")]
    public List<GameObject> enemyPrefabsInLevel = new List<GameObject>();

    // [Header("scrap prefabs")]
    public List<GameObject> scrapPrefabsInLevel = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < wavesInLevel.Count; i++){
            foreach(WavePieceData data in wavesInLevel[i].wavePieces){
                enemyPrefabsInLevel.Add(data.enemyPrefab);
                scrapPrefabsInLevel.Add(data.enemyPrefab.GetComponent<EnemyScript>().scrapPrefab);
            }
        }
        enemyPrefabsInLevel = enemyPrefabsInLevel.Distinct().ToList();
        scrapPrefabsInLevel = scrapPrefabsInLevel.Distinct().ToList();

        for(int i = 0; i < enemyPrefabsInLevel.Count; i++){
            PoolManager.Instance.CreatePool(enemyPrefabsInLevel[i], 50, "enemy");
        }

        for(int i = 0; i < scrapPrefabsInLevel.Count; i++){
            PoolManager.Instance.CreatePool(scrapPrefabsInLevel[i], 100, "scrap");
        }
    }
}