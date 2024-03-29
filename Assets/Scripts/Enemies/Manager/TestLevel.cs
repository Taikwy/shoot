using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestLevel : MonoBehaviour
{
    [Header("waves")]
    public List<TestWave> wavesInLevel = new List<TestWave>();

    [Header("enemy prefabs")]
    public List<GameObject> enemyPrefabsInLevel = new List<GameObject>();
    public List<int> numOfEnemyType = new List<int>();

    [Header("scrap prefabs")]
    public List<GameObject> scrapPrefabsInLevel = new List<GameObject>();
    public List<int> numOfScrapType = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < wavesInLevel.Count; i++){
            enemyPrefabsInLevel.Add(wavesInLevel[i].enemyPrefab);
            scrapPrefabsInLevel.Add(wavesInLevel[i].enemyPrefab.GetComponent<EnemyScript>().scrapPrefab);
        }
        enemyPrefabsInLevel = enemyPrefabsInLevel.Distinct().ToList();
        scrapPrefabsInLevel = scrapPrefabsInLevel.Distinct().ToList();

        for(int i = 0; i < enemyPrefabsInLevel.Count; i++){
            PoolManager.Instance.CreatePool(enemyPrefabsInLevel[i], numOfEnemyType[i], "enemy");
        }

        for(int i = 0; i < scrapPrefabsInLevel.Count; i++){
            PoolManager.Instance.CreatePool(scrapPrefabsInLevel[i], numOfScrapType[i], "scrap");
        }
    }
}
