using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel : MonoBehaviour
{
    public List<GameObject> enemyPrefabsInLevel = new List<GameObject>();
    public List<int> numOfEnemyType = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < enemyPrefabsInLevel.Count; i++){
            PoolManager.Instance.CreatePool(enemyPrefabsInLevel[i], numOfEnemyType[i], "enemy");
        }
    }
}
