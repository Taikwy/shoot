using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Spawnpoint")){
            Debug.Log("level end");
        }
    }
}
