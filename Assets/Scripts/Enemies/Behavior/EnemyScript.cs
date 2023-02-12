using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : PoolObject
{
    [Header("Enemy Info")]
    public int maxHealth;
    public int currentHealth;
    public float contactDOT = 1; 
    public int contactDamage = 1;
    Coroutine contactDamageCoroutine = null;
    [HideInInspector] public bool partOfWave;
    [HideInInspector] public TestWaveHolder testWaveHolderScript;
    [HideInInspector] public WaveHolder waveHolderScript;

    [Header("Attack and Movement")]
    public GameObject MovementHolder;
    public GameObject AttackHolder;
    public bool counterAttack = false;
    public Transform firingPoint;
    public List<Transform> firingPoints = new List<Transform>();

    [Header("Scrap Info")]
    public float numScraps;
    public GameObject scrapPrefab;

    public override void OnObjectRespawn(){}
    public virtual void SetupPatterns(){}

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Bullet")){
            BulletScript bulletScript = otherCollider.gameObject.GetComponent<BulletScript>();
            if(!bulletScript.isEnemyBullet && currentHealth > 0){
                TakeDamage(bulletScript);
                bulletScript.TakeDamage();
            }
        }
        if(otherCollider.CompareTag("Player Hurtbox")){
            PlayerScript player = otherCollider.gameObject.transform.parent.parent.GetComponent<PlayerScript>();
            // Debug.Log("COLLIDED WITH PALYER!");
            contactDamageCoroutine = StartCoroutine(DealContactDamage(player));
        }
    }

    void OnTriggerExit2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Player Hurtbox")){
            PlayerScript player = otherCollider.gameObject.GetComponent<PlayerScript>();
            // Debug.Log("UNCOLLIDED WITH PALYER!");
            StopCoroutine(contactDamageCoroutine);
        }
    }

    public IEnumerator DealContactDamage(PlayerScript player){
        for(int i = 0; i < 9999; i++){
            // Debug.Log("CURRENTLY COLLIDINBG WITH PALYER!");
            player.TakeContactDamage(this);
            yield return new WaitForSeconds(Time.deltaTime);
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
        if(contactDamageCoroutine != null)
            StopCoroutine(contactDamageCoroutine);

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
        EnemyManager.Instance.DestroyEnemy(gameObject);
    }

    public GameObject DropScrap(){
        Debug.Log("spawning scrap");
        return PoolManager.Instance.ReuseObject(scrapPrefab, gameObject.transform.position, Quaternion.identity);
        // return PoolManager.Instance.ReuseObject(newScrapPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
