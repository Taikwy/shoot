using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : PoolObject
{
    [Header("Enemy Info")]
    public int maxHealth;
    public int currentHealth;
    [HideInInspector] public bool partOfWave;
    [HideInInspector] public TestWaveHolder waveHolderScript;

    [Header("Attack Info")]
    public bool counterAttack = false;

    [Header("Scrap Info")]
    public float numScraps;
    public GameObject newScrapPrefab;
    public GameObject scrapPrefab;


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
            OnDeath();
        }
    }

    // public virtual void DropScrap(string bulletType){
    //     GameObject scrap = Instantiate(scrapPrefab, transform.position, Quaternion.identity);
    //     switch(bulletType){
    //         case "basic":
    //             scrap.GetComponent<ScrapScript>().SetData(defaultScrap);
    //             break;
    //         case "skill":
    //             scrap.GetComponent<ScrapScript>().SetData(skillScrap);
    //             break;
    //         case "both":
    //             scrap.GetComponent<ScrapScript>().SetData(bothScrap);
    //             break;
    //     }
    // }

    public virtual void OnDeath(){
        GameObject scrap;
        Scrap scrapScript;
        Vector2 direction;
        for(int i = 0; i < numScraps; i++){
            scrap = DropScrap();
            scrapScript = scrap.GetComponent<Scrap>();
            direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            direction.Normalize();
            scrapScript.SetData(direction);
        }
        TestEnemyManager.Instance.DestroyEnemy(gameObject);
    }

    public GameObject DropScrap(){
        Debug.Log("spawning scrap");
        return PoolManager.Instance.ReuseObject(scrapPrefab, gameObject.transform.position, Quaternion.identity);
        // return PoolManager.Instance.ReuseObject(newScrapPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
