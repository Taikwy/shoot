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

    public virtual void UpdateSequence(bool canAttack = false){
        if(canAttack){
            if(!isBursting && burstTimer >= timeBetweenBursts){
                isBursting = true;
                burstTimer = 0;
            }
            if(isBursting && shotTimer >= timeBetweenShots){
                Shoot();
                shotsTaken++;
                shotTimer = 0;
                if(shotsTaken >= burstSize){
                    isBursting = false;
                    shotsTaken = 0;
                    shotTimer = timeBetweenShots;
                }
            }
        }
        
        shotTimer += Time.deltaTime;
        burstTimer += Time.deltaTime;
    }

    // public IEnumerator Burst(){
    //     timeBetweenBursts = 0;
    //     canBurst = false;
    //     isBursting = true;
    //     for(int i = 0; i < burstSize; i++){
    //         Shoot();
    //         yield return new WaitForSeconds(timeBetweenShots);
    //     }
    //     canBurst = true;
    //     isBursting = false;
    // }

    // public virtual void Attack(){
    //     timeBetweenBursts = 0;
    //     canBurst = false;
    //     isBursting = true;
    // }

    public virtual void Shoot(){}

    // public virtual void ShootForward(){}
    // public virtual void ShootAt(){}
    // public virtual void ShootAtPlayer(){}

}
