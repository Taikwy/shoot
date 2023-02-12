using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    public EnemyScript enemyScript;
    
    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Bullet")){
            // if(enemyScript.isInvincible){
            //     return;
            // }
            BulletScript bullet = otherCollider.gameObject.GetComponent<BulletScript>();
            if(!bullet.isEnemyBullet){
                enemyScript.TakeDamage(bullet);
                bullet.TakeDamage();
            }
        }
    }
}
