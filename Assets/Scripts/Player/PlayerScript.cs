using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
    [Header("script refs")]
    public PlayerMovement playerMovement;
    public PlayerController playerController;
    public PlayerShooting playerShooting;

    [Header("ui stuffs")]
    public GameObject playerUI;

    public delegate void ResourceBarAction();
    public static event ResourceBarAction SetMaxStats;
    public static event ResourceBarAction OnHealthChange;

    [Header("player data")]
    public PlayerData data;

    public bool shieldRecharging, shieldBroken;

    // Start is called before the first frame update
    void Start()
    {
        data.currentHealth = data.startingHealth;

        playerMovement.Setup();
        playerShooting.Setup();
        SetMaxStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(data.currentHealth <= 0){
            this.gameObject.SetActive(false);
        }
        playerMovement.UpdateMovement();
        playerShooting.UpdateShooting();
        playerController.UpdateSprite();
    }

    public void TakeDamage(int damage){
        // currentHealth -= damage;
        data.currentHealth -= damage;
        OnHealthChange();
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Bullet")){
            if(data.isInvincible){
                return;
            }
            BulletScript bullet = otherCollider.gameObject.GetComponent<BulletScript>();
            if(bullet.isEnemyBullet){
                TakeDamage(bullet.damage);
                bullet.TakeDamage();
            }
        }
    }
}
