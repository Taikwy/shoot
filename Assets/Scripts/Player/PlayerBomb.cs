using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    public PlayerData playerData;
    public CircleCollider2D circleCollider2D;

    public float defaultRadius, shieldRadius, healthRadius, bombLength;
    float currentRadius = .1f;

    Coroutine bombCoroutine;

    void Start(){
        Reset();
    }

    void Reset(){
        circleCollider2D.radius = defaultRadius;
        circleCollider2D.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Bullet")){
            BulletScript bullet = otherCollider.gameObject.GetComponent<BulletScript>();
            if(bullet.isEnemyBullet){
                bullet.TakeDamage(99);
            }
        }
    }

    public void ShieldBomb(){
        if(bombCoroutine!=null)
            StopCoroutine(bombCoroutine);
        bombCoroutine = StartCoroutine(BombRoutine(shieldRadius));
    }

    
    public void HealthBomb(){
        if(bombCoroutine!=null)
            StopCoroutine(bombCoroutine);
        bombCoroutine = StartCoroutine(BombRoutine(healthRadius));
    }

    public IEnumerator BombRoutine(float targetRadius){
        playerData.isBombing = true;
        float increment = targetRadius/playerData.bombLength;

        circleCollider2D.radius = defaultRadius;
        circleCollider2D.enabled = true;
        while(circleCollider2D.radius < targetRadius){
            circleCollider2D.radius += increment * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        circleCollider2D.radius = defaultRadius;
        circleCollider2D.enabled = false;

        
        playerData.isBombing = false;
    }

}
