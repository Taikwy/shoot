using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinSegment : PathSegment
{
    [Header("Segment Refs")]
    public GameObject pointObj;
    public GameObject pointHolder;
    // public Transform centerTransform;
    
    [Header("Sin Info")]
    public float totalDistance;
    public float amplitude;
    public int numHalfCycles, numPoints;
    public bool downwards = true;
    public bool rightFirst, horizontal = false;
    

    public override void PopulatePointsEditor(){
        float halfCycleDist = totalDistance/numHalfCycles;
        float frequency = Mathf.PI/halfCycleDist;

        float distBetweenPoints = totalDistance/numPoints;
        float distFromOrigin = 0;

        ResetPointsEditor();

        Vector2 holderPos = pointHolder.transform.position;
        Vector2 pointPos = Vector2.zero;
        GameObject pathPoint;
        for(int i = 0; i <= numPoints; i++){
            
            if(horizontal){
                pointPos.x = distFromOrigin + holderPos.x;
                if(rightFirst)
                    pointPos.y = amplitude * Mathf.Sin(distFromOrigin * frequency) + holderPos.y;
                else
                    pointPos.y = amplitude * Mathf.Sin((distFromOrigin + halfCycleDist) * frequency) + holderPos.y;
            }
            else{
                if(rightFirst)
                    pointPos.x = amplitude * Mathf.Sin(distFromOrigin * frequency) + holderPos.x;
                else
                    pointPos.x = amplitude * Mathf.Sin((distFromOrigin + halfCycleDist) * frequency) + holderPos.x;
                pointPos.y = distFromOrigin + holderPos.y;
            }
            
            // Debug.Log(pointPos);
            if(downwards){
                pointPos.y *= -1;
                
                // Debug.Log(pointPos.y * -1);
            }

            pathPoint = Instantiate(pointObj, pointPos, Quaternion.identity);
            pathPoint.name = "point " + (i+1);
            pathPoint.transform.parent = pointHolder.transform;
            pathPoint.transform.localScale *= 0.12f;

            pathPoints.Add(pathPoint);
            distFromOrigin += distBetweenPoints;
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
