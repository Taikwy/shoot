using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [HideInInspector]
    public Transform originalPoolHolder;
    public virtual void OnObjectReuse(){}
    public virtual void OnObjectRespawn(){}

    protected void Destroy(){
        gameObject.SetActive(false);
    }
}
