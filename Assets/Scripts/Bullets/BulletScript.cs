using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletScript : PoolObject
{
    public Rigidbody2D rb;
    public Transform bulletTransform;
    
    [Tooltip("Primary  |  Special  |  Both  |  Enemy")]
    public string bulletType;
    
    [Header("Bullet Stats")]
    public float movementSpeed;
    public int maxHealth, damage;
    int currentHealth;

    [HideInInspector] public float maxTimeAlive, currentTimeAlive;
    [HideInInspector] public float maxDistance, currentDistance;

    [Header("Extra Stats")]
    public bool isEnemyBullet;
    protected Vector2 moveDirection, startPosition, lastPosition;

    public virtual void SetData(bool isEnemy, Vector2 moveDir, float distance = -1, float timeAlive = -1){
        currentHealth = maxHealth;
        lastPosition = bulletTransform.position;
        startPosition = bulletTransform.position;

        isEnemyBullet = isEnemy;
        moveDirection = moveDir;

        if(distance != -1){
            maxDistance = distance;
        }
        else
            maxDistance = 999f;

        if(timeAlive != -1)
            maxTimeAlive = timeAlive;
        else
            maxTimeAlive = 999f;
    }

    public virtual void TakeDamage(){
        Debug.Log("taking damage " + currentHealth);
        currentHealth--;
        if(currentHealth <= 0)
            gameObject.SetActive(false);
    }

    public virtual void TakeDamage(int damage){
        currentHealth-= damage;
        if(currentHealth <= 0)
            gameObject.SetActive(false);
    }

    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){
        currentDistance = 0;
        currentTimeAlive = 0;
        // Debug.Log("reusing");
    }

    protected virtual void FixedUpdate()
    {
        Move();
        UpdateBullet();
        if(currentDistance >= maxDistance || currentTimeAlive >= maxTimeAlive){
            gameObject.SetActive(false);
        }
    }

    public void UpdateBullet(){
        currentTimeAlive += Time.deltaTime;
        currentDistance += Vector2.Distance(bulletTransform.position, lastPosition);
        lastPosition = bulletTransform.position;
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
