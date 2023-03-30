using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level : MonoBehaviour
{
    public float scrollSpeed;
    public Vector2 scrollDirection;
    [Header("wave stuff")]
    public List<Wave> wavesInLevel = new List<Wave>();

    // [Header("enemy prefabs")]
    public List<GameObject> enemyPrefabsInLevel = new List<GameObject>();

    // [Header("scrap prefabs")]
    public List<GameObject> scrapPrefabsInLevel = new List<GameObject>();
    public List<GameObject> movementSequencesInLevel = new List<GameObject>();
    public List<GameObject> attackSequencesInLevel = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        enemyPrefabsInLevel.Clear();
        scrapPrefabsInLevel.Clear();
        movementSequencesInLevel.Clear();
        attackSequencesInLevel.Clear();
        for(int i = 0; i < wavesInLevel.Count; i++){
            foreach(WavePieceData data in wavesInLevel[i].wavePieces){
                enemyPrefabsInLevel.Add(data.enemyPrefab);
                Debug.Log(data.enemyPrefab + " " + data.numEnemies);
                PoolManager.Instance.AddToPool(data.enemyPrefab, data.numEnemies, "enemy");

                scrapPrefabsInLevel.Add(data.enemyPrefab.GetComponent<EnemyScript>().scrapPrefab);
                if(data.movementSequence){
                    PoolManager.Instance.AddToPool(data.movementSequence, data.numEnemies, "movement");
                    // movementSequencesInLevel.Add(data.movementSequence);
                }
                if(data.attackSequence){
                    PoolManager.Instance.AddToPool(data.attackSequence, data.numEnemies, "attack");
                    // attackSequencesInLevel.Add(data.attackSequence);
                }
            }
        }
        enemyPrefabsInLevel = enemyPrefabsInLevel.Distinct().ToList();
        scrapPrefabsInLevel = scrapPrefabsInLevel.Distinct().ToList();
        movementSequencesInLevel = movementSequencesInLevel.Distinct().ToList();
        attackSequencesInLevel = attackSequencesInLevel.Distinct().ToList();

        for(int i = 0; i < enemyPrefabsInLevel.Count; i++){
            // PoolManager.Instance.AddToPool(enemyPrefabsInLevel[i], 50, "enemy");
        }

        for(int i = 0; i < scrapPrefabsInLevel.Count; i++){
            PoolManager.Instance.AddToPool(scrapPrefabsInLevel[i], 100, "scrap");
        }
        // for(int i = 0; i < movementSequencesInLevel.Count; i++){
        //     PoolManager.Instance.AddToPool(movementSequencesInLevel[i], 100, "movement sequence");
        // }
        // for(int i = 0; i < attackSequencesInLevel.Count; i++){
        //     PoolManager.Instance.AddToPool(attackSequencesInLevel[i], 100, "attack sequence");
        // }
    }
}
