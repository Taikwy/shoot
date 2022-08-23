using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollisionSpawn : MonoBehaviour
{
    public GameObject wavePrefab;
    // TestWaveHolder waveHolderScript;
    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Spawnpoint")){
            GameObject waveHolder = Instantiate(wavePrefab);
            waveHolder.name = gameObject.name + " Holder";
            waveHolder.GetComponent<TestWaveHolder>().SpawnWave(gameObject.transform.position);
            // waveHolder.GetComponent<TestWaveHolder>().Setup(gameObject.transform.position.x);

            gameObject.SetActive(false);
            // gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
