using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();

    static PoolManager _instance;

    public static PoolManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<PoolManager>();
            }
            return _instance;
        }
    }

    public Transform bulletPoolsTransform;
    public Transform enemyPoolsTransform, scrapPoolsTransform;
    public Transform movementPoolsTransform, attackPoolsTransform;

    public Queue<ObjectInstance> CreatePool(GameObject prefab, int poolSize, string poolType = ""){
        int poolKey = prefab.GetInstanceID();

        if(poolDictionary.ContainsKey(poolKey)){
            return IncreasePoolSize(prefab, poolSize, poolType);
        }
        GameObject poolHolder = new GameObject(prefab.name + " pool");
        switch(poolType){
            case "bullet":
                poolHolder.transform.parent = bulletPoolsTransform;
                break;
            case "enemy":
                poolHolder.transform.parent = enemyPoolsTransform;
                Debug.Log("making enemy pool");
                break;
            case "scrap":
                poolHolder.transform.parent = scrapPoolsTransform;
                break;
            case "movement":
                poolHolder.transform.parent = movementPoolsTransform;
                break;
            case "attack":
                poolHolder.transform.parent = attackPoolsTransform;
                break;
            default:
                poolHolder.transform.parent = transform;
                break;
        }
        poolDictionary.Add(poolKey, new Queue<ObjectInstance>());

        for(int i = 0; i < poolSize; i++){
            ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
            newObject.gameObject.name = prefab.name;
            // newObject.SetActive(false);
            poolDictionary[poolKey].Enqueue(newObject);
            newObject.SetParent(poolHolder);
        }
        return poolDictionary[poolKey];
    }

    public Queue<ObjectInstance> IncreasePoolSize(GameObject prefab, int poolSize, string poolType = ""){
        int poolKey = prefab.GetInstanceID();

        if(!poolDictionary.ContainsKey(poolKey)){
            return CreatePool(prefab, poolSize);
        }
        if(poolSize > poolDictionary[poolKey].Count){
            GameObject poolHolder;
            switch(poolType){
                case "bullet":
                    poolHolder = bulletPoolsTransform.Find(prefab.name + " pool").gameObject;
                    break;
                case "enemy":
                    poolHolder = enemyPoolsTransform.Find(prefab.name + " pool").gameObject;
                    break;
                case "scrap":
                    poolHolder = scrapPoolsTransform.Find(prefab.name + " pool").gameObject;
                    break;
                case "movement":
                    poolHolder = movementPoolsTransform.Find(prefab.name + " pool").gameObject;
                    break;
                case "attack":
                    poolHolder = attackPoolsTransform.Find(prefab.name + " pool").gameObject;
                    break;
                default:
                    poolHolder = transform.Find(prefab.name + " pool").gameObject;
                    break;
            }
            int numToAdd = poolSize - poolDictionary[poolKey].Count;

            for(int i = 0; i < numToAdd; i++){
                ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
                poolDictionary[poolKey].Enqueue(newObject);
                newObject.SetParent(poolHolder);
            }
        }
        return poolDictionary[poolKey];
    }

    //Used to add X amount of prefabs to an existing pool
    public Queue<ObjectInstance> AddToPool(GameObject prefab, int poolSize, string poolType = ""){
        int poolKey = prefab.GetInstanceID();

        if(poolDictionary.ContainsKey(poolKey)){
            // Debug.Log("Adding to pool " + poolSize + " " +  poolType);
            return IncreasePoolSize(prefab, poolDictionary[poolKey].Count + poolSize, poolType);
        }
        // Debug.Log("No pool to add to, creating new pool " + poolSize + " " +  poolType);
        return CreatePool(prefab, poolSize, poolType);
    }

    //For spawning pool objects
    public GameObject ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation){
        int poolKey = prefab.GetInstanceID();

        if(poolDictionary.ContainsKey(poolKey)){
            ObjectInstance objectToReuse  = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToReuse);

            objectToReuse.Reuse(position, rotation);

            return objectToReuse.gameObject;
        }
        Debug.Log("No pool for object yet");
        return null;
    }

    //Same as reuseObject but returns the object instance, used for enemies currently
    public ObjectInstance ReuseEnemy(GameObject prefab, Vector3 position, Quaternion rotation){
        int poolKey = prefab.GetInstanceID();

        if(poolDictionary.ContainsKey(poolKey)){
            ObjectInstance objectToReuse  = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToReuse);

            objectToReuse.Reuse(position, rotation);

            return objectToReuse;
        }
        Debug.Log("No pool for object yet");
        return null;
    }

    public GameObject RespawnObject(GameObject prefab, GameObject respawningObject, Vector3 position, Quaternion rotation){
        int poolKey = respawningObject.GetInstanceID();
        if(poolDictionary.ContainsKey(poolKey)){
            ObjectInstance objectToRespawn  = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToRespawn);

            objectToRespawn.Respawn(position, rotation);

            return objectToRespawn.gameObject;
        }
        Debug.Log("Object not in pool");
        return null;
    }

    public class ObjectInstance{
        public GameObject gameObject;
        GameObject originalPool;
        Transform transform;

        bool hasPoolObjectComponent;
        PoolObject poolObjectScript;

        public ObjectInstance(GameObject objectInstance){
            gameObject = objectInstance;
            transform = gameObject.transform;
            gameObject.SetActive(false);

            if(gameObject.GetComponent<PoolObject>()){
                hasPoolObjectComponent = true;
                poolObjectScript = gameObject.GetComponent<PoolObject>();
            }
        }

        public void Reuse(Vector2 position, Quaternion rotation){
            transform.position = position;
            transform.rotation = rotation;
            
            if(hasPoolObjectComponent){
                poolObjectScript.OnObjectReuse();
            }
            gameObject.SetActive(true);
        }

        public void Respawn(Vector2 position, Quaternion rotation){
            transform.position = position;
            transform.rotation = rotation;
            
            if(hasPoolObjectComponent){
                poolObjectScript.OnObjectRespawn();
            }
            gameObject.SetActive(true);
        }

        public void SetParent(GameObject parent){
            originalPool = parent;
            transform.parent = parent.transform;
            if(hasPoolObjectComponent){
                switch (poolObjectScript){
                    case EnemyScript:
                        poolObjectScript.originalPoolHolder = parent.transform;
                        break;
                }
            }
        }
    }
}
