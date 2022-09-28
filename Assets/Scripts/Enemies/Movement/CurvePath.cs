using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvePath : PathSegment
{
    public float radius;
    public int numPoints = 72;

    public GameObject pointObj;
    public GameObject pointHolderObjPrefab;
    public GameObject pointHolder;

    public Transform centerTransform;
    public float degreesToTurn;
    public bool clockwise;


    public override void PopulatePoints(){
        float deltaAngle = degreesToTurn/numPoints;
        float currentAngle = 0;

        float deltaRads = Mathf.Deg2Rad*deltaAngle;
        float currentRads = 0;

        pathPoints.Clear();
        foreach (Transform child in pointHolder.transform) {
            Destroy(child.gameObject);
        }
        Vector2 pointPos;
        GameObject pathPoint;
        for(int i = 0; i < numPoints; i++){
            pointPos = new Vector2(radius * Mathf.Cos(currentRads) + centerTransform.position.x, radius * Mathf.Sin(currentRads)+centerTransform.position.y);
            pathPoint = Instantiate(pointObj, pointPos, Quaternion.identity);
            pathPoint.name = "point " + i + " : " + currentAngle + " degs";
            pathPoint.transform.parent = pointHolder.transform;
            pathPoint.transform.localScale *= 0.1f;

            pathPoints.Add(pathPoint);
            if(clockwise){
                currentAngle -= deltaAngle;
                currentRads -= deltaRads;
            }
            else{
                currentAngle += deltaAngle;
                currentRads += deltaRads;
            }
        }

        startPoint = pathPoints[0];
        endPoint = pathPoints[numPoints-1];
    }

    public override void OffsetPoints(Vector2 offsetAmount){
        Vector2 startPointPos = startPoint.transform.position;
        base.OffsetPoints(offsetAmount-startPointPos);
    }
}
