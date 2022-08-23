using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    [Header("Reference stuff")]
    public PlayerScript playerScript;
    public Transform firingPoint;
    public GameObject bulletPrefab;

    
    [Header("bullet prefabs")]
    public GameObject defaultPrefab;
    public GameObject skillPrefab;

    public GameObject testPrefab;
    public GameObject basicPrefab;
    public GameObject laserPrefab;
    public GameObject triplePrefab;

    
    [Header("bullet data")]
    public GameObject defaultData;
    public GameObject skillData;
    // public GameObject emptyShot;
    public BulletData basicData;
    public BulletData laserData;
    public BulletData tripleData;

    [Header("player shooting info")]
    bool isRechargingDefault = false;
    bool isRechargingSkill = false;
    float timeSinceDefaultShot = 0f;
    float timeSinceSkillShot = 0f;


    void Start(){
        // PoolManager.instance.CreatePool(bulletPrefab, 10);
        // PoolManager.instance.CreatePool(emptyShot, 20);

        // PoolManager.instance.CreatePool(testPrefab, 20);
        PoolManager.Instance.CreatePool(basicPrefab, 20, "bullet");
        PoolManager.Instance.CreatePool(laserPrefab, 10, "bullet");
        PoolManager.Instance.CreatePool(triplePrefab,20, "bullet");
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown("k"))
        // {
        //     Shoot(bulletPrefab);
        // }
        if (Input.GetKeyDown("j"))
        {
            //Shoot(bulletPrefab);
        }
        if (Input.GetKeyDown("k"))
        {
            //Shoot(basicData);
        }

        RechargeAmmo();

        if (Input.GetKey("k"))
        {
            if(basicData.ammoCost <= playerScript.currentDefaultAmmo && timeSinceDefaultShot >= basicData.cooldown){
                Shoot("test");
                playerScript.currentDefaultAmmo -= basicData.ammoCost;
                isRechargingDefault = false;
                timeSinceDefaultShot = 0;
            }
        }

        if (Input.GetKeyDown("u"))
        {
            if(basicData.ammoCost <= playerScript.currentDefaultAmmo && timeSinceDefaultShot >= basicData.cooldown){
                Shoot("basic");
                playerScript.currentDefaultAmmo -= basicData.ammoCost;
                isRechargingDefault = false;
                timeSinceDefaultShot = 0;
            }
        }
        if (Input.GetKeyDown("i"))
        {
            if(laserData.ammoCost <= playerScript.currentSkillAmmo){
                Shoot("laser");
                playerScript.currentSkillAmmo -= laserData.ammoCost;
                isRechargingSkill = false;
                timeSinceSkillShot = 0;
            }
        }
        if (Input.GetKeyDown("o"))
        {
            if(tripleData.ammoCost <= playerScript.currentSkillAmmo){
                Shoot("triple");
                playerScript.currentSkillAmmo -= tripleData.ammoCost;
                isRechargingSkill = false;
                timeSinceSkillShot = 0;
            }
        }

    }

    void RechargeAmmo(){
        if(isRechargingDefault && playerScript.currentDefaultAmmo < playerScript.maxDefaultAmmo){
            playerScript.currentDefaultAmmo += playerScript.defaultAmmoRechargeRate*Time.deltaTime;
            if(playerScript.currentDefaultAmmo > playerScript.maxDefaultAmmo)
                playerScript.currentDefaultAmmo = playerScript.maxDefaultAmmo;
        }
        timeSinceDefaultShot += Time.deltaTime;
            if(timeSinceDefaultShot >= playerScript.defaultAmmoRechargePause)
                isRechargingDefault = true;

        if(isRechargingSkill && playerScript.currentSkillAmmo < playerScript.maxSkillAmmo){
            playerScript.currentSkillAmmo += playerScript.skillAmmoRechargeAmount*Time.deltaTime;
            if(playerScript.currentSkillAmmo > playerScript.maxSkillAmmo)
                playerScript.currentSkillAmmo = playerScript.maxSkillAmmo;
        }
        else{
            timeSinceSkillShot += Time.deltaTime;
            if(timeSinceSkillShot >= playerScript.skillAmmoRechargePause)
                isRechargingSkill = true;
        }
    }

    public void RechargeAmmo(string ammoType, float defaultAmmo = 0, float skillAmount = 0){
        if(ammoType == "default"){
            playerScript.currentDefaultAmmo += defaultAmmo;
            if(playerScript.currentDefaultAmmo > playerScript.maxDefaultAmmo)
                playerScript.currentDefaultAmmo = playerScript.maxDefaultAmmo;
        }
        if(ammoType == "skill"){
            playerScript.currentSkillAmmo += skillAmount;
            if(playerScript.currentSkillAmmo > playerScript.maxSkillAmmo)
                playerScript.currentSkillAmmo = playerScript.maxSkillAmmo;
        }
        if(ammoType == "both"){
            playerScript.currentDefaultAmmo += defaultAmmo;
            if(playerScript.currentDefaultAmmo > playerScript.maxDefaultAmmo)
                playerScript.currentDefaultAmmo = playerScript.maxDefaultAmmo;

            playerScript.currentSkillAmmo += skillAmount;
            if(playerScript.currentSkillAmmo > playerScript.maxSkillAmmo)
                playerScript.currentSkillAmmo = playerScript.maxSkillAmmo;
        }
    }

    void Shoot(string bulletType)
    {
        GameObject bullet;

        switch(bulletType){
            case "test":
                bullet = PoolManager.Instance.ReuseObject(testPrefab, firingPoint.position, firingPoint.rotation);
                bullet.GetComponent<BulletScript>().SetData(false, firingPoint.up);
                break;
            case "basic":
                bullet = PoolManager.Instance.ReuseObject(basicPrefab, firingPoint.position, firingPoint.rotation);
                bullet.GetComponent<BulletScript>().SetData(false, firingPoint.up);
                break;
            case "laser":
                bullet = PoolManager.Instance.ReuseObject(laserPrefab, firingPoint.position, firingPoint.rotation);
                bullet.GetComponent<BulletScript>().SetData(false, firingPoint.up);
                break;
            case "triple":
                Quaternion angleDeltaL = Quaternion.AngleAxis(-15, firingPoint.forward);
                Quaternion angleDeltaR = Quaternion.AngleAxis(15, firingPoint.forward);

                bullet = PoolManager.Instance.ReuseObject(triplePrefab, firingPoint.position, firingPoint.rotation);
                bullet.GetComponent<BulletScript>().SetData(false, firingPoint.up);

                bullet = PoolManager.Instance.ReuseObject(triplePrefab, firingPoint.position, angleDeltaL);
                bullet.GetComponent<BulletScript>().SetData(false, angleDeltaL * firingPoint.up);

                bullet = PoolManager.Instance.ReuseObject(triplePrefab, firingPoint.position, angleDeltaR);
                bullet.GetComponent<BulletScript>().SetData(false, angleDeltaR * firingPoint.up);
                break;
        }
    }

    void Shoot(GameObject bulletType)
    {
        // GameObject bullet = PoolManager.instance.ReuseObject(bulletType, firingPoint.position, firingPoint.rotation);
        // bullet.GetComponent<BulletScript>().SetData();
        // bullet.GetComponent<BulletScript>().isEnemyBullet = false;
        // bullet.GetComponent<BulletScript>().moveDirection = new Vector2(0,1);
    }
    // void Shoot(BulletData bulletData)
    // {
    //     GameObject bullet = PoolManager.instance.ReuseObject(emptyShot, firingPoint.position, firingPoint.rotation);
    //     bullet.GetComponent<BulletScript>().SetData(bulletData);
    //     bullet.GetComponent<BulletScript>().isEnemyBullet = false;
    //     bullet.GetComponent<BulletScript>().moveDirection = new Vector2(0,1);
    // }
}
