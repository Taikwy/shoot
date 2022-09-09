using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [HideInInspector]
    public Transform originalPoolHolder;
    public virtual void OnObjectReuse(){}
    
    //Used for enemies that get respawned
    public virtual void OnObjectRespawn(){}

    protected void Destroy(){
        gameObject.SetActive(false);
    }
}
