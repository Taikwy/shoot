using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{    
    [Header("Reference stuff")]
    public PlayerScript playerScript;
    public PlayerUI playerUI;

    [Header("player data")]
    public PlayerData data;
    
    [Header("Absorbing stuff")]
    public GameObject normalPickupCollider;
    public GameObject absorbPickupCollider;

    [Header("Gun stuff")]
    public GameObject primaryGun;
    public int maxNumSpecialGuns;
    public List<GameObject> specialGuns = new List<GameObject>();
    [HideInInspector] public GameObject currentSpecialGun;
    public int currentSpecialIndex;
    [HideInInspector] public PrimaryGun primaryGunScript;
    [HideInInspector] public SpecialGun currentSpecialScript;
    [HideInInspector] public List<SpecialGun> specialGunScripts = new List<SpecialGun>();

    public void Setup(){
        currentSpecialIndex = 0;
        currentSpecialGun = specialGuns[currentSpecialIndex];

        primaryGunScript = primaryGun.GetComponent<PrimaryGun>();
        currentSpecialScript = currentSpecialGun.GetComponent<SpecialGun>();
        currentSpecialScript.equipped = true;
        foreach(GameObject g in specialGuns){
            specialGunScripts.Add(g.GetComponent<SpecialGun>());
        }
    }

    public void UpdateShooting()
    {
        if (Input.GetKeyDown("h")){
            currentSpecialScript.equipped = false;

            currentSpecialIndex = (currentSpecialIndex+1) % maxNumSpecialGuns;
            currentSpecialGun = specialGuns[currentSpecialIndex];
            currentSpecialScript = currentSpecialGun.GetComponent<SpecialGun>();
            playerUI.SetMaxSpecialAmmo();

            currentSpecialScript.equipped = true;
        }
        // if(!data.isDashing){
        //     data.isAbsorbing = Input.GetKey("u");               //is absorbing as long as key is being held
        //     if(data.isAbsorbing){
        //         normalPickupCollider.SetActive(false);
        //         absorbPickupCollider.SetActive(true);
        //     }

        //     if(!data.isAbsorbing){
        //         normalPickupCollider.SetActive(true);
        //         absorbPickupCollider.SetActive(false);

        //         primaryGunScript.UpdateShooting();
        //         currentSpecialScript.UpdateShooting();

        //         data.isShooting = Input.GetKey("j") || Input.GetKey("k");
        //     }
        // }

        if(!data.isDashing){
            data.isShooting = Input.GetKey("j") || Input.GetKey("k");
            data.isAbsorbing = Input.GetKey("u") && !data.isShooting;                   //is absorbing as long as key is being held

            if(data.isShooting){
                data.isAbsorbing = false;
                normalPickupCollider.SetActive(true);
                absorbPickupCollider.SetActive(false);

                primaryGunScript.UpdateShooting();
                currentSpecialScript.UpdateShooting();
            }

            if(!data.isShooting){
                if(data.isAbsorbing){
                    normalPickupCollider.SetActive(false);
                    absorbPickupCollider.SetActive(true);
                }
            }
        }
        
    }

    public void PickupAmmo(Scrap scrapScript){
        currentSpecialScript.RefillAmmo(scrapScript.equippedAmmo);
        foreach(SpecialGun specialGunScript in specialGunScripts){
            if(specialGunScript != currentSpecialScript)
                specialGunScript.RefillAmmo(scrapScript.subAmmo);
        }
    }

    public void PickupAmmo(string ammoType, float ammoAmount){
        if(ammoType == "Primary"){
            primaryGunScript.RefillAmmo();
        }
        if(ammoType == "Special"){
            currentSpecialScript.RefillAmmo();
        }
    }
}
