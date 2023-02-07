using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerScript playerScript;
    public PlayerData data;
    
    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Bullet")){
            if(data.isInvincible){
                return;
            }
            BulletScript bullet = otherCollider.gameObject.GetComponent<BulletScript>();
            if(bullet.isEnemyBullet){
                playerScript.TakeDamage(bullet.damage);
                bullet.TakeDamage();
            }
        }
        if(otherCollider.CompareTag("Scrap")){
            Scrap scrap = otherCollider.gameObject.GetComponent<Scrap>();
            OnPickupScrap(scrap);
        }
    }

    void OnPickupScrap(Scrap scrap){
        data.numScrap++;
        scrap.OnPickup();
    }
}
