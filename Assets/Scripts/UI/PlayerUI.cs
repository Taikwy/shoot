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
    public GameObject shieldDisplay;
    public GameObject dashDisplay;
    public GameObject movementInfoDisplay;
    public GameObject primaryAmmoDisplay;
    public GameObject specialAmmoDisplay;
    public GameObject infoDisplay;
    // public GameObject scrapDisplay;

    HealthBar healthScript;
    ShieldBar shieldScript;
    ResourceBar primaryAmmoScript;
    ResourceBar specialAmmoScript;
    PlayerInfo playerInfoScript;
    // DashCDBar dashCooldownScript;
    
    [Header("info display info")]

    public PlayerData playerData;

    void Awake()
    {
        SetComponents();
        SetEvents();
    }

    void SetComponents(){
        Debug.Log("settinmg components");
        playerScript = SingletonManager.Instance.playerScript;
        playerShooting = SingletonManager.Instance.playerShooting;

        healthScript = healthDisplay.GetComponent<HealthBar>();
        shieldScript = shieldDisplay.GetComponent<ShieldBar>();
        primaryAmmoScript = primaryAmmoDisplay.GetComponent<ResourceBar>();
        specialAmmoScript = specialAmmoDisplay.GetComponent<ResourceBar>();

        playerInfoScript = infoDisplay.GetComponent<PlayerInfo>();
    }

    void SetEvents(){
        PlayerScript.SetMaxStats += healthScript.SetMaxHealth;
        PlayerScript.SetMaxStats += shieldScript.SetMaxShield;
        PlayerScript.SetMaxStats += SetMaxPrimaryAmmo;
        PlayerScript.SetMaxStats += SetMaxSpecialAmmo;
        PlayerScript.OnHealthChange += healthScript.UpdateHealth;
        PlayerScript.OnShieldChange += shieldScript.UpdateShield;


        // PlayerScript.OnInfoChange += playerInfoScript.SetText;

        
        // PlayerScript.SetMaxStats += SetMaxHealth;
        // PlayerScript.SetMaxStats += dashCooldownScript.SetMaxCooldown;
        // PlayerScript.OnHealthChange += UpdateHealth;
    }

    void CleanEvents(){
        PlayerScript.SetMaxStats -= healthScript.SetMaxHealth;
        PlayerScript.SetMaxStats -= shieldScript.SetMaxShield;
        PlayerScript.SetMaxStats -= SetMaxPrimaryAmmo;
        PlayerScript.SetMaxStats -= SetMaxSpecialAmmo;
        PlayerScript.OnHealthChange -= healthScript.UpdateHealth;
        PlayerScript.OnShieldChange -= shieldScript.UpdateShield;
        Debug.Log("cleaned events");

    }

    // Update is called once per frame
    void Update()
    {
        // UpdateAmmo(primaryAmmoScript, playerShooting.primaryGunScript);
        // UpdateAmmo(specialAmmoScript, playerShooting.currentSpecialScript);

        primaryAmmoScript.SetCurrentValue(playerShooting.primaryGunScript.currentHeat);
        specialAmmoScript.SetCurrentValue(playerShooting.currentSpecialScript.currentAmmo);

        
        playerInfoScript.SetText(playerData.numScrap);
    }

    public void SetMaxPrimaryAmmo(){
        primaryAmmoScript.SetMaxValue(playerShooting.primaryGunScript.maxHeat);
    }
    
    public void UpdatePrimaryAmmo(){
        primaryAmmoScript.SetCurrentValue(playerData.currentPrimaryAmmo);
    }

    public void SetMaxSpecialAmmo(){
        specialAmmoScript.SetMaxValue(playerShooting.currentSpecialScript.maxAmmo);
    }

    
    public void OnDestroy(){
        // Debug.Log("destroyed player ui");
        CleanEvents();
    }
}
