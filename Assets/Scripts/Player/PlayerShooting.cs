using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    [Header("Reference stuff")]
    public PlayerScript playerScript;
    public PlayerUI playerUI;
    [Header("Reference stuff")]
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

    // Update is called once per frame
    void Update()
    {
        // primaryGunScript.RechargeAmmo();
        // for(int i = 0; i < specialGuns.Count; i++){
        //     if(i == currentSpecialIndex)
        //         currentSpecialScript.RechargeAmmo();
        //     else{
        //         specialGunScripts[i].RechargeInactiveAmmo();
        //     }
        // }
        
        if (Input.GetKey("k")){
            primaryGunScript.Shoot();
            // Debug.Log("primary shoot");
        }
        if (Input.GetKey("j")){
            currentSpecialScript.Shoot();
            Debug.Log("special shoot");
        }
        if (Input.GetKeyDown("h")){
            currentSpecialScript.equipped = false;

            currentSpecialIndex = (currentSpecialIndex+1) % maxNumSpecialGuns;
            currentSpecialGun = specialGuns[currentSpecialIndex];
            currentSpecialScript = currentSpecialGun.GetComponent<SpecialGun>();
            playerUI.SetMaxSpecialAmmo();

            currentSpecialScript.equipped = true;
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Scrap")){
            Scrap scrapScript = otherCollider.gameObject.GetComponent<Scrap>();
            PickupAmmo(scrapScript);
            if(scrapScript is PoolObject){
                otherCollider.gameObject.SetActive(false);
                Debug.Log("picked up");
            }
            else{
                Debug.Log("destroying");
                Destroy(otherCollider.gameObject);
            }
        }
    }

    void PickupAmmo(Scrap scrapScript){
        if(scrapScript.ammoType == "Primary"){
            primaryGunScript.RefillAmmo(scrapScript.amount);
        }
        if(scrapScript.ammoType == "Special"){
            currentSpecialScript.RefillAmmo(scrapScript.amount);
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
