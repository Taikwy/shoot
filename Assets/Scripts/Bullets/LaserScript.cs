using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : BulletScript
{
    public enum LASERPART{
        START,
        MIDDLE,
        END
    }
    
    [Header("Laser Stats")]
    public LASERPART laserPart;

    public override void SetData(bool isEnemy, Vector2 moveDir, float distance = -1, float timeAlive = -1){
        // currentHealth = maxHealth;
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

    public override void TakeDamage(){
        // currentHealth--;
        // if(currentHealth <= 0)
        //     gameObject.SetActive(false);
    }

    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){
        currentDistance = 0;
        currentTimeAlive = 0;
    }

    protected override void FixedUpdate(){
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

    // public override void Move(){
    //     rb.MovePosition(rb.position + moveDirection * movementSpeed);
    // }

    // public override void Shoot(bool isEnemy = false, Vector2 fireDirection = new Vector2()){
    //     isEnemyBullet = isEnemy;
    //     moveDirection = fireDirection;
    // }
}
