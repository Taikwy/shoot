using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : PoolObject
{
    [Header("scrap movement stuff")]
    public Rigidbody2D rb;
    public float moveSpeed;
    public float acceleration;
    Vector2 moveDirection;
    Vector2 newPosition;
    // [Tooltip("Primary  |  Special")]
    
    [Header("scrap ammo data")]
    [HideInInspector] public string ammoType;
    public float amount;
    public float activeTimer;
    protected float currentTimer = 0;

    public virtual void SetData(Vector2 moveDir){
        moveDirection = moveDir;
    }

    // Update is called once per frame
    void Update(){
        currentTimer += Time.deltaTime;
        if(currentTimer >= activeTimer){
            gameObject.SetActive(false);
        }
        newPosition = rb.position + moveDirection * moveSpeed * Time.deltaTime;
        newPosition += new Vector2(0, -5) * Time.deltaTime;
    }

    void FixedUpdate(){
        rb.MovePosition(newPosition);

        if(moveSpeed < 0.1f)
            moveSpeed = 0;
        else
            moveSpeed += acceleration * Time.deltaTime;
    }

    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){
        currentTimer = 0;
    }
}
