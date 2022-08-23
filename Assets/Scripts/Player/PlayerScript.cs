using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
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


    public float maxDefaultAmmo;
    public float currentDefaultAmmo;
    public float defaultAmmoRechargePause;
    public float defaultAmmoRechargeRate;
    public float defaultAmmoRechargeAmount;
    public float maxSkillAmmo;
    public float currentSkillAmmo;
    public float skillAmmoRechargePause;
    public float skillAmmoRechargeRate;
    public float skillAmmoRechargeAmount;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 8f;
        dashDistance = 4;
        dashSpeed = 600f;

        maxDefaultAmmo = 100;
        currentDefaultAmmo = maxDefaultAmmo;
        defaultAmmoRechargePause = 0.25f;
        defaultAmmoRechargeRate = 20;
        defaultAmmoRechargeAmount = 20;

        maxSkillAmmo = 100;
        currentSkillAmmo = maxSkillAmmo;
        skillAmmoRechargePause = 1;
        skillAmmoRechargeRate = 10;
        skillAmmoRechargeAmount = 15;

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
                TakeDamage(bullet.damage);
                bullet.TakeDamage();
            }
        }
    }
}
