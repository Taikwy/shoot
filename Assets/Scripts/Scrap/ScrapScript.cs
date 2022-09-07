using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapScript : PoolObject
{
    public ScrapData scrapData;

    public SpriteRenderer spriteRenderer;    
    public Rigidbody2D rb;
    public CircleCollider2D cd;
    public Animator animator;
    string ammoType;
    float amount;
    float defaultAmount;
    float skillAmount;

    float activeTimer;

    // Start is called before the first frame update
    void Start(){
        // SetData();
    }

    // Update is called once per frame
    void Update(){
        activeTimer -= Time.deltaTime;
        if(activeTimer <= 0){
            Destroy(gameObject);
        }
    }

    public virtual void SetData(ScrapData newScrapData){
        scrapData = newScrapData;

        gameObject.name = scrapData.name;
        spriteRenderer.sprite = scrapData.defaultSprite;

        ammoType = scrapData.ammoType;
        // amount = scrapData.amount;
        defaultAmount = scrapData.defaultAmount;
        skillAmount = scrapData.skillAmount;

        activeTimer = scrapData.activeTimer;
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Player")){
            Pickup(otherCollider);
        }
    }

    void Pickup(Collider2D playerCollider){
        // playerCollider.gameObject.GetComponent<PlayerShooting>().RechargeAmmo(ammoType, defaultAmount, skillAmount);
        playerCollider.gameObject.GetComponent<PlayerShooting>().PickupAmmo(ammoType, amount);
        Destroy(gameObject);
    }
}
