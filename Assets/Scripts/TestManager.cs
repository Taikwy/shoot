using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        PoolManager.Instance.CreatePool(prefab, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            PoolManager.Instance.ReuseObject(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}
