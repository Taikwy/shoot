using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    [Header("Reference stuff")]
    public PlayerScript playerScript;
    [Header("Reference stuff")]
    public int maxNumSpecialGuns;
    public GameObject primaryGun, currentSpecialGun;
    [HideInInspector] public PrimaryGun primaryGunScript;
    [HideInInspector] public Gun currentSpecialScript;
    // public GameObject tripleGun, laserGun;
    // Gun tripleGunScript, laserGunScript;

    public List<GameObject> specialGuns = new List<GameObject>();
    public List<SpecialGun> specialGunScripts = new List<SpecialGun>();
    public int currentSpecialIndex;

    [Header("bullet info")]
    public Transform firingPoint;

    // void Start(){
    //     maxNumSpecialGuns = 3;

    //     currentSpecialIndex = 0;
    //     currentSpecialGun = specialGuns[currentSpecialIndex];

    //     primaryGunScript = primaryGun.GetComponent<PrimaryGun>();
    //     currentSpecialScript = currentSpecialGun.GetComponent<Gun>();
    //     foreach(GameObject g in specialGuns){
    //         specialGunScripts.Add(g.GetComponent<SpecialGun>());
    //     }
    // }

    public void Setup(){
        maxNumSpecialGuns = 3;

        currentSpecialIndex = 0;
        currentSpecialGun = specialGuns[currentSpecialIndex];

        primaryGunScript = primaryGun.GetComponent<PrimaryGun>();
        currentSpecialScript = currentSpecialGun.GetComponent<Gun>();
        foreach(GameObject g in specialGuns){
            specialGunScripts.Add(g.GetComponent<SpecialGun>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        primaryGunScript.RechargeAmmo();
        for(int i = 0; i < specialGuns.Count; i++){
            if(i == currentSpecialIndex)
                currentSpecialScript.RechargeAmmo();
            else{
                specialGunScripts[i].RechargeInactiveAmmo();
            }
        }
        
        if (Input.GetKey("k")){
            primaryGunScript.Shoot();
        }
        if (Input.GetKey("j")){
            currentSpecialScript.Shoot();
        }
        if (Input.GetKeyDown("h")){
            currentSpecialIndex = (currentSpecialIndex+1) % maxNumSpecialGuns;
            currentSpecialGun = specialGuns[currentSpecialIndex];
            currentSpecialScript = currentSpecialGun.GetComponent<Gun>();
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
