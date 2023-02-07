using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : SpecialGun
{
    [Header("Laser stuff")]
    GameObject laserStart, laserMiddle, laserEnd;

    GameObject currentLaserStart, currentLaserMiddle, currentLaserEnd;
    // Define an "infinite" size, not too big but enough to go off screen
    public float maxLaserSize;
    float currentLaserSize;
    Vector2 laserDirection = Vector2.up;

    public GameObject leftRaycastPoint, rightRaycastPoint;


    
    bool endOnScreen;

    float startSpriteHeight;
    float endSpriteHeight;
    
    public override void Start(){
        base.Start();
        laserStart = bulletPrefabs[0];
        laserMiddle = bulletPrefabs[1];
        laserEnd = bulletPrefabs[2];

        startSpriteHeight = laserStart.GetComponent<SpriteRenderer>().bounds.size.y;
        endSpriteHeight = laserEnd.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    public override void UpdateShooting(){
        if (Input.GetKey(playerData.specialFireKey)){
            if(ammoCost <= currentAmmo){
                Shoot();
                return;
            }
        }
        if(currentLaserStart)
            currentLaserStart.SetActive(false);
        if(currentLaserMiddle)
            currentLaserMiddle.SetActive(false);
        if(currentLaserEnd)
            currentLaserEnd.SetActive(false);
    }

    public override void Shoot(){      
        // if(currentLaserStart)
        //     currentLaserStart.SetActive(false);
        // if(currentLaserMiddle)
        //     currentLaserMiddle.SetActive(false);
        // if(currentLaserEnd)
        //     currentLaserEnd.SetActive(false);

        


        // Raycast upwards as our sprite has been designed for that

        int layerMask = 1 << 8;
        Vector2 laserDirection = gameObject.transform.up;
        RaycastHit2D hitLeft = Physics2D.Raycast(leftRaycastPoint.transform.position, laserDirection, maxLaserSize, layerMask);
        RaycastHit2D hitCenter = Physics2D.Raycast(firingPoints[0].transform.position, laserDirection, maxLaserSize, layerMask);
        RaycastHit2D hitRight = Physics2D.Raycast(rightRaycastPoint.transform.position, laserDirection, maxLaserSize, layerMask);

        float leftLaserSize = -1;
        float centerLaserSize = -1;
        float rightLaserSize = -1;
        float currentLaserSize = maxLaserSize;     

        if(hitLeft.collider == null && hitCenter.collider == null && hitRight.collider == null){
            if(currentLaserEnd)
                currentLaserEnd.SetActive(false);
        }
        else{
            if(hitLeft.collider != null){
                leftLaserSize = Vector2.Distance(hitLeft.point, firingPoints[0].transform.position);
            }
            if(hitCenter.collider != null){
                centerLaserSize = Vector2.Distance(hitCenter.point, firingPoints[0].transform.position);
            }
            if(hitRight.collider != null){
                rightLaserSize = Vector2.Distance(hitRight.point, firingPoints[0].transform.position);
            }

            float[] laserSizes = {leftLaserSize, centerLaserSize, rightLaserSize};
            float value = 999f;
            int index = -1;
            for(int i = 0; i < laserSizes.Length; i++){
                if(laserSizes[i] < value && laserSizes[i] != -1){
                    index = i;
                    value = laserSizes[i];
                }
            }
            currentLaserSize = laserSizes[index];

        }
        // Debug.Log("size " + currentLaserSize);


        // RaycastHit2D hit = Physics2D.Raycast(firingPoints[0].transform.position, laserDirection, maxLaserSize, layerMask);

        // if (hit.collider != null){
        //     // -- Get the laser length
        //     currentLaserSize = Vector2.Distance(hit.point, firingPoints[0].transform.position);
        //     // Debug.Log("hitting " + hit.collider.gameObject.name + "    size: " + currentLaserSize + "  " + hit.point + " " + firingPoints[0].transform.position);
        // }
        // else{
        //     if(currentLaserEnd)
        //         currentLaserEnd.SetActive(false);
        // }

        Vector2 startPos = new Vector2(firingPoints[0].position.x, firingPoints[0].position.y);
        currentLaserStart  = PoolManager.Instance.ReuseObject(laserStart, startPos, firingPoints[0].rotation);
        currentLaserStart.GetComponent<BulletScript>().SetData(false, Vector2.zero);

        Vector2 midPos = new Vector2(firingPoints[0].position.x, firingPoints[0].position.y + (currentLaserSize/2f));
        currentLaserMiddle = PoolManager.Instance.ReuseObject(laserMiddle, midPos, firingPoints[0].rotation);
        currentLaserMiddle.GetComponent<BulletScript>().SetData(false, Vector2.zero);
        currentLaserMiddle.transform.localScale = new Vector3(currentLaserMiddle.transform.localScale.x, (currentLaserSize - startSpriteHeight) + .05f, currentLaserMiddle.transform.localScale.z);

        Vector2 endPos = new Vector2(firingPoints[0].position.x, firingPoints[0].position.y + currentLaserSize);
        currentLaserEnd = PoolManager.Instance.ReuseObject(laserEnd, endPos, firingPoints[0].rotation);
        currentLaserEnd.GetComponent<BulletScript>().SetData(false, Vector2.zero);
        
        base.Shoot();
    }
}
