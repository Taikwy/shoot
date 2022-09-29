using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveSegment : PathSegment
{
    [Header("Segment Refs")]
    public GameObject pointObj;
    public GameObject pointHolderObjPrefab;
    public GameObject pivot;
    public GameObject pointHolder;
    // public GameObject pointsHolder;
    public Transform centerTransform;
    
    [Header("Curve Info")]
    public float radius;
    public int numPoints = 72;
    public float degreesToTurn;
    public bool clockwise;

    // public override void PopulatePoints(){
    //     float deltaAngle = degreesToTurn/numPoints;
    //     float currentAngle = 0;

    //     float deltaRads = Mathf.Deg2Rad*deltaAngle;
    //     float currentRads = 0;

    //     // pathPoints.Clear();
    //     // foreach (Transform child in pointsHolder.transform) {
    //     //     Destroy(child.gameObject);
    //     // }
    //     // Destroy(pointsHolder);
    //     // pointsHolder = Instantiate(new GameObject(), gameObject.transform.position, Quaternion.identity);
    //     CleanPointsHolder();

    //     Vector2 pointPos;
    //     GameObject pathPoint;
    //     for(int i = 0; i < numPoints; i++){
    //         pointPos = new Vector2(radius * Mathf.Cos(currentRads) + centerTransform.position.x, radius * Mathf.Sin(currentRads)+centerTransform.position.y);
    //         pathPoint = Instantiate(pointObj, pointPos, Quaternion.identity);
    //         pathPoint.name = "point " + i + " : " + currentAngle + " degs";
    //         pathPoint.transform.parent = pointsHolder.transform;
    //         // pathPoint.transform.parent = gameObject.transform.GetChild(0).transform;
    //         pathPoint.transform.localScale *= 0.1f;

    //         pathPoints.Add(pathPoint);
    //         if(clockwise){
    //             currentAngle -= deltaAngle;
    //             currentRads -= deltaRads;
    //         }
    //         else{
    //             currentAngle += deltaAngle;
    //             currentRads += deltaRads;
    //         }
    //     }

    //     startPoint = pathPoints[0];
    //     endPoint = pathPoints[numPoints-1];
    // }

    // public void CleanPointsHolder(){
    //     pathPoints.Clear();
    //     if(pointsHolder){
    //         foreach (Transform child in pointsHolder.transform) {
    //             Destroy(child.gameObject);
    //         }
    //     }
    //     Destroy(pointsHolder);
    //     pointsHolder = Instantiate(new GameObject(), gameObject.transform.position, Quaternion.identity);
    // }

    // public override void OffsetPoints(Vector2 offsetAmount){
    //     // Vector2 startPointPos = startPoint.transform.position;
    //     // base.OffsetPoints(offsetAmount-startPointPos);
    //     base.OffsetPoints(offsetAmount);
    // }

    public override void PopulatePointsEditor(){
        float deltaAngle = degreesToTurn/numPoints;
        float currentAngle = 0;

        float deltaRads = Mathf.Deg2Rad*deltaAngle;
        float currentRads = 0;

        ResetPointsEditor();

        pivot.transform.localScale = (radius * 2) * Vector3.one;
        // pivot.transform.localScale = (radius * 2) * Vector3.one;

        Vector2 pointPos;
        GameObject pathPoint;
        for(int i = 0; i <= numPoints; i++){
            pointPos = new Vector2(radius * Mathf.Cos(currentRads) + centerTransform.position.x, radius * Mathf.Sin(currentRads)+centerTransform.position.y);
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
