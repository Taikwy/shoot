using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("other scripts")]
    PlayerScript playerScript;
    PlayerShooting playerShooting;


    [Header("ui objects")]
    public GameObject healthDisplay;
    public GameObject dashDisplay;
    public GameObject movementInfoDisplay;
    public GameObject primaryAmmoDisplay;
    public GameObject specialAmmoDisplay;
    public GameObject infoDisplay;

    HealthBar healthScript;
    ResourceBar primaryAmmoScript;
    ResourceBar specialAmmoScript;
    DashCDBar dashCooldownScript;
    
    [Header("info display info")]

    public PlayerData playerData;

    void Awake()
    {
        SetComponents();
        // PlayerScript.SetMaxStats += SetMaxHealth;
        PlayerScript.SetMaxStats += healthScript.SetMaxHealth;
        PlayerScript.SetMaxStats += SetMaxPrimaryAmmo;
        PlayerScript.SetMaxStats += SetMaxSpecialAmmo;
        // PlayerScript.SetMaxStats += dashCooldownScript.SetMaxCooldown;
        // PlayerScript.OnHealthChange += UpdateHealth;
        PlayerScript.OnHealthChange += healthScript.UpdateHealth;
    }

    void SetComponents(){
        playerScript = SingletonManager.Instance.playerScript;
        playerShooting = SingletonManager.Instance.playerShooting;

        healthScript = healthDisplay.GetComponent<HealthBar>();
        primaryAmmoScript = primaryAmmoDisplay.GetComponent<ResourceBar>();
        specialAmmoScript = specialAmmoDisplay.GetComponent<ResourceBar>();
        // dashCooldownScript = dashDisplay.GetComponent<DashCDBar>();
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateAmmo(primaryAmmoScript, playerShooting.primaryGunScript);
        // UpdateAmmo(specialAmmoScript, playerShooting.currentSpecialScript);

        primaryAmmoScript.SetCurrentValue(playerShooting.primaryGunScript.currentHeat);
        specialAmmoScript.SetCurrentValue(playerShooting.currentSpecialScript.currentAmmo);
    }

    public void SetMaxHealth(){
        healthScript.SetMaxValue(playerData.maxHealth);
    }

    public void UpdateHealth(){
        healthScript.SetCurrentValue(playerData.currentHealth);
    }

    public void SetMaxPrimaryAmmo(){
        primaryAmmoScript.SetMaxValue(playerShooting.primaryGunScript.maxHeat);
        // Debug.Log("max primary ammo set");
    }
    
    public void UpdatePrimaryAmmo(){
        primaryAmmoScript.SetCurrentValue(playerData.currentPrimaryAmmo);
    }

    public void SetMaxSpecialAmmo(){
        specialAmmoScript.SetMaxValue(playerShooting.currentSpecialScript.maxAmmo);
        // Debug.Log("max special ammo set");
    }
    // public void SetMaxAmmo(ResourceBar ammoScript, Gun gunScript){
    //     ammoScript.SetMaxValue(gunScript.maxAmmo);
    // }

    // public void UpdateAmmo(ResourceBar ammoScript, Gun gunScript){
    //     ammoScript.SetCurrentValue(gunScript.currentAmmo);
    // }

    public void SetMaxDashCooldown(){}
}
