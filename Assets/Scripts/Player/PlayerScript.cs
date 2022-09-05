using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("movement info")]
    // public float movementSpeed;
    // public float dashDistance;
    // public float dashSpeed;
    // public bool isDashing = false;
    
    // [Header("body info")]
    // public int maxHealth = 5;
    // public int currentHealth;
    
    [Header("ui stuffs")]
    public GameObject playerUI;

    public delegate void ResourceBarAction();
    public static event ResourceBarAction SetMaxStats;
    public static event ResourceBarAction OnHealthChange;

    [Header("player data")]
    public PlayerData data;


    // Start is called before the first frame update
    void Start()
    {
        // movementSpeed = 8f;
        // dashDistance = 4;
        // dashSpeed = 600f;

        // currentHealth = maxHealth;
        data.currentHealth = data.startingHealth;

        SetMaxStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(data.currentHealth <= 0){
            this.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage){
        // currentHealth -= damage;
        data.currentHealth -= damage;
        OnHealthChange();
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Bullet")){
            BulletScript bullet = otherCollider.gameObject.GetComponent<BulletScript>();
            if(bullet.isEnemyBullet){
                TakeDamage(bullet.damage);
                bullet.TakeDamage();
            }
        }
    }
}
