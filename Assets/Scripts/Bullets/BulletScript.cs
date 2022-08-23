using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletScript : PoolObject
{
    // public BulletData bulletData;
 
    public Rigidbody2D rb;
    // public Animator animator;
    
    public string bulletType;
    
    [Header("Bullet Stats")]
    public int maxHealth;
    public int currentHealth;
    public int damage;
    public float movementSpeed;
    public float ammoCost;
    public float cooldown;

    [Header("Extra Stats")]
    public bool isEnemyBullet;
    protected Vector2 moveDirection, initialPosition, nextPosition;

    public virtual void SetData(bool isEnemy, Vector2 moveDir){
        currentHealth = maxHealth;

        isEnemyBullet = isEnemy;
        moveDirection = moveDir;

        // initialPosition = transform.position;
        // nextPosition = transform.position;
    }

    public virtual void TakeDamage(){
        currentHealth--;
        if(currentHealth <= 0)
            gameObject.SetActive(false);
    }

    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){
        
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        rb.MovePosition(rb.position + moveDirection * movementSpeed);
    }

    public virtual void Shoot(bool isEnemy = false, Vector2 fireDirection = new Vector2()){
        isEnemyBullet = isEnemy;
        moveDirection = fireDirection;
    }
}
