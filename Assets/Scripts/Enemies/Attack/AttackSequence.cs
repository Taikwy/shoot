using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSequence : MonoBehaviour
{
    public List<Transform> firingPoints = new List<Transform>();
    public List<GameObject> bulletPrefabs = new List<GameObject>();
    protected Transform firingPoint;
    protected GameObject bulletPrefab;
    protected Transform playerTransform;
    public int poolSize = 25;

    [Header("Burst Info")]
    public int burstSize = 1;
    public float timeBetweenBursts = 0;
    public float timeBetweenShots;
    [HideInInspector] public int shotsTaken = 0;
    [HideInInspector] public float shotTimer, burstTimer = 0;
    [HideInInspector] public bool isBursting = false;

    [Header("Sequence Info")]
    public bool requireLOS;
    public int numBullets = 1;

    public virtual void Reset(){
        firingPoint = firingPoints[0];
        bulletPrefab = bulletPrefabs[0];
        playerTransform = SingletonManager.Instance.playerScript.transform;

        burstTimer = timeBetweenBursts;
        shotTimer = timeBetweenShots;
        shotsTaken = 0;
        isBursting = false;
    }

    public virtual void Setup(){
        Reset();

        foreach(GameObject bullet in bulletPrefabs){
            PoolManager.Instance.CreatePool(bullet, poolSize, "bullet");
        }
    }

    public virtual void UpdateSequence(){
        if(!isBursting && burstTimer >= timeBetweenBursts){
            isBursting = true;
            burstTimer = 0;
        }
        if(isBursting && shotTimer >= timeBetweenShots){
            StartCoroutine(BurstFire());
            isBursting = false;
            // Shoot();
            // shotsTaken++;
            // Debug.Log(shotTimer + " " + shotsTaken + " " + timeBetweenShots);
            // shotTimer = 0;
            // if(shotsTaken >= burstSize){
            //     isBursting = false;
            //     shotsTaken = 0;
            //     shotTimer = timeBetweenShots;
            // }
            // Debug.Log(shotTimer + " " + shotsTaken  + " afterwards=================================");
        }
    }

    IEnumerator BurstFire(){
        // isBursting = true;
        for(int i = 0; i < burstSize; i++){
            Shoot();
            // Debug.Log(shotTimer + " " + shotsTaken + " " + timeBetweenShots);
            // shotTimer = 0;
            // if(shotsTaken >= burstSize){
            //     isBursting = false;
            //     shotsTaken = 0;
            //     shotTimer = timeBetweenShots;
            // }
            // Debug.Log(shotTimer + " " + shotsTaken  + " afterwards=================================");

            yield return new WaitForSeconds(timeBetweenShots);
        }
        // isBursting = false;
    }

    public virtual void UpdateTimers(){
        if(isBursting){
            shotTimer += Time.deltaTime;
        }
            // if(shotTimer >= .06)
            //     Debug.Log(shotTimer + " " + Time.deltaTime + " \\\\\\\\\\");
        burstTimer += Time.deltaTime;
    }

    public virtual bool SingleUpdate(){
        if(!isBursting){
            if(burstTimer >= timeBetweenBursts){
                isBursting = true;
                burstTimer = 0;
            }
            else{
                return true;
            }
        }
        if(isBursting && shotTimer >= timeBetweenShots){
            Shoot();
            shotsTaken++;
            shotTimer = 0;
            if(shotsTaken >= burstSize){
                isBursting = false;
                shotsTaken = 0;
                shotTimer = timeBetweenShots;
                return true;
            }
        }
        return false;
    }

    public virtual void Shoot(){}
}
