using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public EnemyScript enemyScript;

    public float contactDOT, contactDamage;

    void Start(){
        contactDOT = enemyScript.contactDOT;
        contactDamage = enemyScript.contactDamage;
    }
    
    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Player")){
            // BulletScript bullet = otherCollider.gameObject.GetComponent<BulletScript>();
            // if(!bullet.isEnemyBullet){
            //     enemyScript.TakeDamage(bullet);
            //     bullet.TakeDamage();
            // }
            PlayerScript player = otherCollider.gameObject.GetComponent<PlayerScript>();
            // player.TakeDamage();
            Debug.Log("COLLIDED WITH PALYER!");
        }
    }
}
