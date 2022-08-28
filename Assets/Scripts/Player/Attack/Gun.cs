using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("bullet info")]
    public Transform firingPoint;
    public GameObject bulletPrefab;
    public BulletScript bulletScript;

    [Header("ammo info")]
    public float maxAmmo;
    public float currentAmmo;
    public float ammoRechargePause;
    public float ammoRechargeRate;
    public float ammoRechargeAmount;

    [Header("gun shooting info")]
    public bool isRecharging = false;
    public float timeSinceShot = 0f;

    // Start is called before the first frame update
    public virtual void Start()
    {
        bulletScript = bulletPrefab.GetComponent<BulletScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // RechargeAmmo();
    }

    public void RechargeAmmo(){
        if(isRecharging && currentAmmo < maxAmmo){
            currentAmmo += ammoRechargeRate*Time.deltaTime;
            if(currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;
        }
        timeSinceShot += Time.deltaTime;
            if(timeSinceShot >= ammoRechargePause)
                isRecharging = true;
    }

    public virtual void Shoot(){}
}
