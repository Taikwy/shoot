using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCleanup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otherCollider){
        // //Change this to type later on so its easier
        // BulletScript bullet = otherCollider.gameObject.GetComponent<BulletScript>();
        // if (bullet != null){
            
        // }

        if(otherCollider.CompareTag("Bullet")){
            if(otherCollider.gameObject.GetComponent<BulletScript>() is PoolObject){
                otherCollider.gameObject.SetActive(false);
            }
            else{
                Destroy(otherCollider.gameObject);
            }
        }
        if(otherCollider.CompareTag("Scrap")){
            if(otherCollider.gameObject.GetComponent<BulletScript>() is PoolObject){
                otherCollider.gameObject.SetActive(false);
            }
            else{
                Destroy(otherCollider.gameObject);
            }
        }
        
        
    }
}
