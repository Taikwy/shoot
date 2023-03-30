using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerScript playerScript;
    public PlayerData data;
    
    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Bullet")){
            if(data.isInvincible || data.isBombing){
                return;
            }
            BulletScript bullet = otherCollider.gameObject.GetComponent<BulletScript>();
            if(bullet.isEnemyBullet){
                playerScript.TakeDamage(bullet);
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
        playerScript.playerShooting.PickupAmmo(scrap);
        scrap.OnPickup();
    }
}
