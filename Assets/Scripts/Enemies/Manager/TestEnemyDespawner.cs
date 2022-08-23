using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyDespawner : MonoBehaviour
{
    // TestEnemyManager enemyManager = TestEnemyManager.Instance;
    public TestEnemyManager testEnemeyManager;

    void Start(){
        testEnemeyManager = TestEnemyManager.Instance;
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Enemy")){                                                //check if its ane enmy
            // if(otherCollider.gameObject.GetComponent<EnemyScript>().exiting)                      //check if enemy isnt currently spawning
                testEnemeyManager.DespawnEnemy(otherCollider.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Enemy")){                                                //check if its ane enmy
            // if(otherCollider.gameObject.GetComponent<EnemyScript>().exiting)                      //check if enemy isnt currently spawning
                testEnemeyManager.DespawnEnemy(otherCollider.gameObject);
        }
    }
}
