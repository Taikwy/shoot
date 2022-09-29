using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveSegment : PathSegment
{
    [Header("Segment Refs")]
    public GameObject pointObj;
    public GameObject pointHolderObjPrefab;
    public GameObject pivotCircle;
    public GameObject pointHolder;
    // public Transform centerTransform;
    
    [Header("Curve Info")]
    public float radius;
    public float degreesToTurn;
    public int numPoints = 72;
    public bool clockwise;

    public override void PopulatePointsEditor(){
        float deltaAngle = degreesToTurn/numPoints;
        float currentAngle = 0;

        float deltaRads = Mathf.Deg2Rad*deltaAngle;
        float currentRads = 0;

        ResetPointsEditor();

        pivotCircle.transform.localScale = (radius * 2) * Vector3.one;
        Vector2 holderPos = pointHolder.transform.position;
        Vector2 pointPos;
        GameObject pathPoint;
        for(int i = 0; i <= numPoints; i++){
            pointPos = new Vector2(radius * Mathf.Cos(currentRads) + holderPos.x, radius * Mathf.Sin(currentRads) + holderPos.y);
            pathPoint = Instantiate(pointObj, pointPos, Quaternion.identity);
            pathPoint.name = "point " + (i+1) + " : " + currentAngle + " degs";
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
        endPoint = pathPoints[pathPoints.Count-1];

        SetPositionsList();
    }

    public override void ResetPointsEditor(){
        pathPoints.Clear();
        while(pointHolder.transform.childCount > 0){
            DestroyImmediate(pointHolder.transform.GetChild(0).gameObject);
        }
        pointHolder.transform.rotation = Quaternion.identity;
    }
}
