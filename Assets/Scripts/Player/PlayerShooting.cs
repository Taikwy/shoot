using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    [Header("Reference stuff")]
    public PlayerScript playerScript;
    public GameObject basicGun;
    public GameObject tripleGun;
    public GameObject laserGun;
    public Gun basicGunScript, tripleGunScript, laserGunScript;

    [Header("ammo info")]
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

    
    [Header("bullet info")]
    public Transform firingPoint;
    public GameObject basicPrefab, laserPrefab, triplePrefab;
    BulletScript basicScript, laserScript, tripleScript;
    
    enum ShotType{
        Basic,
        Skill
    }
    [Header("current skill shot")]
    public SkillType currentSkillType = SkillType.Laser;
    public enum SkillType{
        Laser,
        Triple
    }
    delegate void ShotDelegate();
    ShotDelegate currentSkillShot;

    [Header("laser shot data")]
    public float sizeIncreaseRate = .1f;
    
    [Header("triple shot data")]
    public int numShots = 3;
    public float gapBetween = 10;
    public float maxDistance = 10;

    [Header("player shooting info")]
    bool isRechargingDefault = false;
    bool isRechargingSkill = false;
    float timeSinceDefaultShot = 0f;
    float timeSinceSkillShot = 0f;


    void Start(){

        basicGunScript = basicGun.GetComponent<Gun>();
        tripleGunScript = tripleGun.GetComponent<Gun>();
        laserGunScript = laserGun.GetComponent<Gun>();

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

        PoolManager.Instance.CreatePool(basicPrefab, 50, "bullet");
        PoolManager.Instance.CreatePool(laserPrefab, 20, "bullet");
        PoolManager.Instance.CreatePool(triplePrefab, 30, "bullet");

        basicScript = basicPrefab.GetComponent<BulletScript>();
        laserScript = laserPrefab.GetComponent<BulletScript>();
        tripleScript = triplePrefab.GetComponent<BulletScript>();

        currentSkillShot = laserGunScript.Shoot;
        // currentSkillShot = ShootLaser;
    }

    // Update is called once per frame
    void Update()
    {
        RechargeAmmo();

        if (Input.GetKey("k")){
            basicGunScript.Shoot();
        }
        if (Input.GetKey("j")){
            currentSkillShot();
        }

        if (Input.GetKey("u")){
            basicGunScript.Shoot();
        }
        if (Input.GetKey("i")){
            tripleGunScript.Shoot();
        }
        if (Input.GetKey("o")){
            laserGunScript.Shoot();
        }
        if (Input.GetKey("p")){
            if(currentSkillType == SkillType.Laser){
                currentSkillType = SkillType.Triple;
                currentSkillShot = tripleGunScript.Shoot;
            }
            else if(currentSkillType == SkillType.Triple){
                currentSkillType = SkillType.Laser;
                currentSkillShot = laserGunScript.Shoot;
            }
        }
        return;

        if (Input.GetKey("k"))
        {
            if(basicScript.ammoCost <= currentDefaultAmmo && timeSinceDefaultShot >= basicScript.cooldown){
                ShootStraight();
                currentDefaultAmmo -= basicScript.ammoCost;
                isRechargingDefault = false;
                timeSinceDefaultShot = 0;
            }
        }

        if (Input.GetKey("j"))
        {
            currentSkillShot();
        }


        if (Input.GetKeyDown("u"))
        {
            if(basicScript.ammoCost <= currentDefaultAmmo && timeSinceDefaultShot >= basicScript.cooldown){
                ShootStraight();
                currentDefaultAmmo -= basicScript.ammoCost;
                isRechargingDefault = false;
                timeSinceDefaultShot = 0;
            }
        }
        if (Input.GetKeyDown("i"))
        {
            if(CheckSkill(SkillType.Triple))
                ShootTriple();
            
        }
        if (Input.GetKeyDown("o"))
        {
            if(CheckSkill(SkillType.Triple))
                ShootTriple();
        }

        if (Input.GetKeyDown("p"))
        {
            if(currentSkillType == SkillType.Laser){
                currentSkillType = SkillType.Triple;
                currentSkillShot = ShootTriple;
            }
            else if(currentSkillType == SkillType.Triple){
                currentSkillType = SkillType.Laser;
                currentSkillShot = ShootLaser;
            }
        }

    }

    bool CheckSkill(SkillType skillType){
        switch(skillType){
            case SkillType.Laser:
                if(laserScript.ammoCost <= currentSkillAmmo){
                    currentSkillAmmo -= laserScript.ammoCost;
                    isRechargingSkill = false;
                    timeSinceSkillShot = 0;
                    return true;
                }
                break;
            case SkillType.Triple:
                if(tripleScript.ammoCost <= currentSkillAmmo){
                    currentSkillAmmo -= tripleScript.ammoCost;
                    isRechargingSkill = false;
                    timeSinceSkillShot = 0;
                    return true;
                }
                break;
        }
        return false;
    }
    void RechargeAmmo(){
        if(isRechargingDefault && currentDefaultAmmo < maxDefaultAmmo){
            currentDefaultAmmo += defaultAmmoRechargeRate*Time.deltaTime;
            if(currentDefaultAmmo > maxDefaultAmmo)
                currentDefaultAmmo = maxDefaultAmmo;
        }
        timeSinceDefaultShot += Time.deltaTime;
            if(timeSinceDefaultShot >= defaultAmmoRechargePause)
                isRechargingDefault = true;

        if(isRechargingSkill && currentSkillAmmo < maxSkillAmmo){
            currentSkillAmmo += skillAmmoRechargeAmount*Time.deltaTime;
            if(currentSkillAmmo > maxSkillAmmo)
                currentSkillAmmo = maxSkillAmmo;
        }
        else{
            timeSinceSkillShot += Time.deltaTime;
            if(timeSinceSkillShot >= skillAmmoRechargePause)
                isRechargingSkill = true;
        }
    }

    public void RechargeAmmo(string ammoType, float defaultAmmo = 0, float skillAmount = 0){
        if(ammoType == "default"){
            currentDefaultAmmo += defaultAmmo;
            if(currentDefaultAmmo > maxDefaultAmmo)
                currentDefaultAmmo = maxDefaultAmmo;
        }
        if(ammoType == "skill"){
            currentSkillAmmo += skillAmount;
            if(currentSkillAmmo > maxSkillAmmo)
                currentSkillAmmo = maxSkillAmmo;
        }
        if(ammoType == "both"){
            currentDefaultAmmo += defaultAmmo;
            if(currentDefaultAmmo > maxDefaultAmmo)
                currentDefaultAmmo = maxDefaultAmmo;

            currentSkillAmmo += skillAmount;
            if(currentSkillAmmo > maxSkillAmmo)
                currentSkillAmmo = maxSkillAmmo;
        }
    }

    void ShootStraight(){
        GameObject bullet = PoolManager.Instance.ReuseObject(basicPrefab, firingPoint.position, firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(false, firingPoint.up);
    }

    void ShootLaser(){
        GameObject bullet = PoolManager.Instance.ReuseObject(laserPrefab, firingPoint.position, firingPoint.rotation);
        bullet.GetComponent<LaserScript>().SetData(false, firingPoint.up, sizeIncreaseRate);
    }

    void ShootTriple(){

        GameObject bullet;
        Quaternion angleDelta;
        float currentAngle = -1 * (numShots-1)/2 * gapBetween;
        Debug.Log("triple shot " + currentAngle + " " + numShots + " " + gapBetween);

        for(int i = 0; i < numShots; i++){
            angleDelta = Quaternion.AngleAxis(currentAngle, firingPoint.forward);
            bullet = PoolManager.Instance.ReuseObject(triplePrefab, firingPoint.position, firingPoint.rotation);
            bullet.GetComponent<TripleScript>().SetData(false, angleDelta * firingPoint.up, maxDistance);

            Debug.Log(angleDelta + " " + i);

            currentAngle += gapBetween;
        }
    }
}
