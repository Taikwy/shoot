using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : PoolObject
{

    public int health = 5;
    // public Transform firingPoint;

    // public GameObject bulletPrefab;
    // public GameObject basicPrefab;
    // public GameObject laserPrefab;
    // public GameObject triplePrefab;

    // public GameObject emptyShot;
    // public BulletData basicData;
    // public BulletData laserData;
    // public BulletData tripleData;

    [Header("Scrap Info")]
    public GameObject scrapPrefab;
    public ScrapData defaultScrap;
    public ScrapData skillScrap;
    public ScrapData bothScrap;

    public TestWaveHolder waveHolderScript;
    public bool partOfWave;
    public bool waveLeader;
    public bool exiting;
    
    public virtual void TakeDamage(BulletScript bulletScript){
        health -= bulletScript.damage;
        if(health <= 0){
            DropScrap(bulletScript.bulletType);
            TestEnemyManager.Instance.DestroyEnemy(gameObject);
            // gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Bullet")){
            BulletScript bulletScript = otherCollider.gameObject.GetComponent<BulletScript>();
            if(!bulletScript.isEnemyBullet && health > 0){
                TakeDamage(bulletScript);
                bulletScript.TakeDamage();
            }
        }
    }

    // void Shoot(string bulletType)
    // {
    //     GameObject bullet;

    //     switch(bulletType){
    //         case "basic":
    //             bullet = PoolManager.instance.ReuseObject(basicPrefab, firingPoint.position, firingPoint.rotation);
    //             bullet.GetComponent<BulletScript>().SetData(true, -firingPoint.up);
    //             break;
    //         case "laser":
    //             bullet = PoolManager.instance.ReuseObject(laserPrefab, firingPoint.position, firingPoint.rotation);
    //             bullet.GetComponent<BulletScript>().SetData(true, -firingPoint.up);
    //             break;
    //         case "triple":
    //             bullet = PoolManager.instance.ReuseObject(triplePrefab, firingPoint.position, firingPoint.rotation);
    //             bullet.GetComponent<BulletScript>().SetData(true, -firingPoint.up);
    //             break;
    //     }
    // }

    // void Shoot(){
    //     // GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    //     // bullet.GetComponent<BulletMovement>().direction = -1;

    //     GameObject bullet = PoolManager.instance.ReuseObject(bulletPrefab, firingPoint.position, firingPoint.rotation);
    //     bullet.GetComponent<BulletScript>().Shoot(true, new Vector2(0,-1));
    // }

    // void Shoot(BulletData bulletData)
    // {
    //     // GameObject bullet = PoolManager.instance.ReuseObject(emptyShot, firingPoint.position, firingPoint.rotation);
    //     // bullet.GetComponent<BulletScript>().SetData(bulletData);
    //     // bullet.GetComponent<BulletScript>().isEnemyBullet = true;
    //     // bullet.GetComponent<BulletScript>().moveDirection = new Vector2(0,-1);
    // }

    public virtual void DropScrap(string bulletType){
        GameObject scrap = Instantiate(scrapPrefab, transform.position, Quaternion.identity);
        switch(bulletType){
            case "default":
                scrap.GetComponent<ScrapScript>().SetData(defaultScrap);
                break;
            case "skill":
                scrap.GetComponent<ScrapScript>().SetData(skillScrap);
                break;
            case "both":
                scrap.GetComponent<ScrapScript>().SetData(bothScrap);
                break;
        }
    }
}
