using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform firingPoint;
    Vector2 playerPosition;
    Transform playerTransform;
    public void Start(){
        playerTransform = SingletonManager.Instance.player.transform;
    }
    public void ShootForward(GameObject bulletPrefab){
        GameObject bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position, firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(true,-firingPoint.up);
    }

    public void ShootTowards(GameObject bulletPrefab){
        // playerPosition = SingletonManager.Instance.player.transform.position;
        playerPosition = playerTransform.position;
        float angle = Vector2.SignedAngle(-firingPoint.up, playerPosition - (Vector2)firingPoint.position);
        Quaternion angleDelta = Quaternion.AngleAxis(angle, firingPoint.forward);

        GameObject bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position, angleDelta);
        bullet.GetComponent<BulletScript>().SetData(true, angleDelta * -firingPoint.up);
    }

    public void ShootTriple(GameObject bulletPrefab){
        GameObject bullet;
        Quaternion angleDeltaL = Quaternion.AngleAxis(-15, firingPoint.forward);
        Quaternion angleDeltaR = Quaternion.AngleAxis(15, firingPoint.forward);

        bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position, firingPoint.rotation);
        bullet.GetComponent<BulletScript>().SetData(true, -firingPoint.up);

        bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position, angleDeltaL);
        bullet.GetComponent<BulletScript>().SetData(true, angleDeltaL * -firingPoint.up);

        bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position, angleDeltaR);
        bullet.GetComponent<BulletScript>().SetData(true, angleDeltaR * -firingPoint.up);
    }

    public void ShootCircle(GameObject bulletPrefab, int numShots = 1, bool centered = true){
        GameObject bullet;
        Quaternion angleDeltaR = Quaternion.identity;

        float angleBetween = 360/numShots;

        for(int i = 0; i < numShots; i++){
            if(centered){
                angleDeltaR = Quaternion.AngleAxis(angleBetween*i, firingPoint.forward);
            }
            else{
                angleDeltaR = Quaternion.AngleAxis(angleBetween*i + angleBetween/2, firingPoint.forward);
            }

            bullet = PoolManager.Instance.ReuseObject(bulletPrefab, firingPoint.position, angleDeltaR);
            bullet.GetComponent<BulletScript>().SetData(true, angleDeltaR * -firingPoint.up);
        }
    }
}
