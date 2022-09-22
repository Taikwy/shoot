using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("bullet info")]
    public Transform firingPoint;
    public List<Transform> firingPoints;
    public GameObject currentBulletPrefab;
    // [HideInInspector]
    // public BulletScript currentBulletScript;

    // [Header("reloading Stats")]
    // public float maxAmmo;
    // public float ammoRechargeRate;
    // public float subRechargeRate;
    // public float ammoRechargePause;
    // public float ammoRechargeAmount;
    // public float currentAmmo;

    [Header("Bullet Stats")]
    // public int maxHealth;
    // public int currentHealth;
    public float ammoCost;
    public float roundsPerSecond;

    // [Header("gun shooting info")]
    [HideInInspector] public bool isRecharging = true;
    [HideInInspector] public float timeSinceShot = 999f;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // currentBulletScript = currentBulletPrefab.GetComponent<BulletScript>();
        CreateBulletPool();
    }
    
    public virtual void CreateBulletPool(){
        PoolManager.Instance.CreatePool(currentBulletPrefab, 100, "bullet");
    }

    public virtual void Cooldown(){
    }

    public virtual void RechargeAmmo(){
    }

    public virtual void RefillAmmo(float refillAmount = 0){
    }

    public virtual void Shoot(){}
}
