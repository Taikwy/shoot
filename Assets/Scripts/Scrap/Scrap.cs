using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : PoolObject
{
    [Header("scrap movement stuff")]
    public Rigidbody2D rb;
    public float minScatterSpeed, maxScatterSpeed, acceleration;
    public float scatterSpeed, absorbSpeed;
    Vector2 moveDirection;
    Vector2 newPosition;
    // [Tooltip("Primary  |  Special")]
    [HideInInspector] public string ammoType;
    
    [Header("scrap ammo info")]
    public float equippedAmmo;
    public float subAmmo;
    Vector2 levelScrollSpeed = new Vector2(0, -8);
    [HideInInspector] public bool absorbed = false;
    Transform playerTransform;

    Vector2 explosionVec, playerVec, scrollVec = new Vector2(0f,0f);
    public virtual void SetData(Vector2 moveDir){
        playerTransform = SingletonManager.Instance.playerScript.gameObject.transform;
        moveDirection = moveDir.normalized;

        levelScrollSpeed = SingletonManager.Instance.levelManager.scrollSpeed;
    }

    // Update is called once per frame
    void Update(){
        //scrap will only dissapear if its not been absorbed
        if(!absorbed){
            // currentTimer += Time.deltaTime;
            // if(timed && currentTimer >= activeTimer){
            //     gameObject.SetActive(false);
            // }
            explosionVec = moveDirection * scatterSpeed * Time.deltaTime;
            scrollVec = levelScrollSpeed * Time.deltaTime;
            
            newPosition = rb.position + explosionVec + scrollVec;
        }
        else{
            explosionVec = moveDirection * scatterSpeed * Time.deltaTime;
            playerVec = (playerTransform.position - (Vector3)rb.position).normalized * absorbSpeed * Time.deltaTime;
            newPosition = rb.position + explosionVec + playerVec;
        }        
    }

    void FixedUpdate(){
        if(scatterSpeed < 0.05f)
            scatterSpeed = 0;
        else{
            // scatterSpeed += acceleration * Time.deltaTime;
            scatterSpeed *= acceleration;
        }
        rb.MovePosition(newPosition);

    }

    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){
        // currentTimer = 0;
        absorbed = false;
        // scatterSpeed = 0;
        scatterSpeed = Random.Range(minScatterSpeed, maxScatterSpeed);
    }

    public void OnAbsorb(float speed = 0f){
        absorbed = true;
        absorbSpeed = speed;
    }

    public void OnPickup(){
        gameObject.SetActive(false);
    }
}
