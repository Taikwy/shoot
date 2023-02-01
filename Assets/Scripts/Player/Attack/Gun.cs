using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [HideInInspector] public PlayerData playerData;
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
    public int bulletPoolSize = 100;
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
        playerData = SingletonManager.Instance.playerScript.data;
    }
    
    public virtual void CreateBulletPool(){}

    public virtual void Cooldown(){
    }

    public virtual void RechargeAmmo(){
    }

    public virtual void RefillAmmo(float refillAmount = 0){
    }

    public virtual void UpdateShooting(){}
    public virtual void Shoot(){}
}
