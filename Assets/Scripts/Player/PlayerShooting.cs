using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    [Header("Reference stuff")]
    public PlayerScript playerScript;
    public GameObject basicGun, currentSkillGun;
    public GameObject tripleGun, laserGun;
    Gun basicGunScript, currentSkillScript;
    Gun tripleGunScript, laserGunScript;

    public List<GameObject> specialAttacks = new List<GameObject>();
    public List<Gun> specialScripts = new List<Gun>();
    public int maxNumOfSpecialAttack = 2;
    public int equippedSpecialAttack;


    [Header("bullet info")]
    public Transform firingPoint;

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


    void Start(){
        // currentSkillGun = laserGun;
        equippedSpecialAttack = 0;
        currentSkillGun = specialAttacks[equippedSpecialAttack];

        basicGunScript = basicGun.GetComponent<Gun>();
        tripleGunScript = tripleGun.GetComponent<Gun>();
        laserGunScript = laserGun.GetComponent<Gun>();
        currentSkillScript = currentSkillGun.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Gun g in specialScripts){
            g.RechargeAmmo();
        }
        if (Input.GetKey("k")){
            basicGunScript.Shoot();
        }
        if (Input.GetKey("j")){
            // currentSkillShot();
            currentSkillScript.Shoot();
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
            equippedSpecialAttack = (equippedSpecialAttack+1)%maxNumOfSpecialAttack;
            currentSkillGun = specialAttacks[equippedSpecialAttack];
            currentSkillScript = currentSkillGun.GetComponent<Gun>();

            // if(currentSkillType == SkillType.Laser){
            //     currentSkillType = SkillType.Triple;
            //     currentSkillGun = laserGun;
            // }
            // else if(currentSkillType == SkillType.Triple){
            //     currentSkillType = SkillType.Laser;
            //     currentSkillGun = tripleGun;
            // }
            // currentSkillScript = currentSkillGun.GetComponent<Gun>();
        }
    }

    public void PickupAmmo(string ammoType, float ammoAmount){
        if(ammoType == "default"){
            basicGunScript.RefillAmmo();
        }
        if(ammoType == "skill"){
            currentSkillScript.RefillAmmo();
        }
    }
}
