using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("movement info")]
    public float movementSpeed;
    public float dashDistance;
    public float dashSpeed;

    public bool isDashing = false;
    public int maxHealth = 5;
    public int currentHealth;
    public HealthBar healthBar;
    public PlayerInfo playerInfo;
    public DefaultBar defaultBar;
    public SkillBar skillBar;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 8f;
        dashDistance = 4;
        dashSpeed = 600f;

        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
        //defaultBar.SetMaxAmmo(maxDefaultAmmo);
        //skillBar.SetMaxAmmo(maxSkillAmmo);

        //playerInfo.SetText(movementSpeed, dashDistance, dashSpeed, isDashing, maxHealth, currentHealth, currentDefaultAmmo, currentSkillAmmo);
        // currentPlayerData.currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0){
            this.gameObject.SetActive(false);
        }
        //playerInfo.SetText(movementSpeed, dashDistance, dashSpeed, isDashing, maxHealth, currentHealth, currentDefaultAmmo, currentSkillAmmo);
        
        //defaultBar.SetCurrentAmmo(currentDefaultAmmo);
        //skillBar.SetCurrentAmmo(currentSkillAmmo);
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        // healthBar.SetCurrentHealth(currentHealth);
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Bullet")){
            BulletScript bullet = otherCollider.gameObject.GetComponent<BulletScript>();
            if(bullet.isEnemyBullet){
                Debug.Log("uh oh");
                TakeDamage(bullet.damage);
                bullet.TakeDamage();
            }
        }
    }
}
