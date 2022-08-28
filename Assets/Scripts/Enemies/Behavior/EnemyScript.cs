using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : PoolObject
{

    [Header("Enemy Info")]
    public int maxHealth;
    public int currentHealth;
    public bool partOfWave;
    [HideInInspector]
    public TestWaveHolder waveHolderScript;

    [Header("Scrap Prefabs")]
    public GameObject scrapPrefab;
    public ScrapData defaultScrap;
    public ScrapData skillScrap;
    public ScrapData bothScrap;

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Bullet")){
            BulletScript bulletScript = otherCollider.gameObject.GetComponent<BulletScript>();
            if(!bulletScript.isEnemyBullet && currentHealth > 0){
                TakeDamage(bulletScript);
                bulletScript.TakeDamage();
            }
        }
    }

    public virtual void TakeDamage(BulletScript bulletScript){
        currentHealth -= bulletScript.damage;
        if(currentHealth <= 0){
            DropScrap(bulletScript.bulletType);
            TestEnemyManager.Instance.DestroyEnemy(gameObject);
            // gameObject.SetActive(false);
        }
    }

    public virtual void DropScrap(string bulletType){
        GameObject scrap = Instantiate(scrapPrefab, transform.position, Quaternion.identity);
        switch(bulletType){
            case "basic":
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
