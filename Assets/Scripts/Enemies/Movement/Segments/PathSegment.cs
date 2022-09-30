using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSegment : MonoBehaviour
{
    // public GameObject pointObj;
    public List<GameObject> pathPoints = new List<GameObject>();
    [HideInInspector] public List<Vector3> pathPositions = new List<Vector3>();
    public GameObject startPoint, endPoint;

    [Header("Stay Info")]
    public float stayTime;
    
    public virtual void PopulatePointsEditor(){}
    public virtual void ResetPointsEditor(){}

    public virtual void MirrorPointsX(){
        foreach (GameObject point in pathPoints) {
            Vector3 tempPos = point.transform.position;
            tempPos.x *= -1;
            point.transform.position = tempPos;
        }
        SetPositionsList();
    }

    public virtual void MirrorPointsY(){
        foreach (GameObject point in pathPoints) {
            Vector3 tempPos = point.transform.position;
            tempPos.y *= -1;
            point.transform.position = tempPos;
        }
        SetPositionsList();
    }

    public virtual void OffsetPoints(bool offset, Vector2 offsetAmount = default(Vector2)){
        SetPositionsList();
        if(!offset)
            return;
        for(int i = 0; i < pathPositions.Count; i++){
            pathPositions[i] = pathPoints[i].transform.position + (Vector3)offsetAmount;
        }
    }

    public virtual List<Vector3> OffsetPointsList(bool offset, Vector2 offsetAmount = default(Vector2)){
        SetPositionsList();
        if(!offset)
            return pathPositions;
        List<Vector3> offsetPoints = new List<Vector3>();
        for(int i = 0; i < pathPositions.Count; i++){
            offsetPoints.Add(pathPoints[i].transform.position + (Vector3)offsetAmount);
        }
        return offsetPoints;
    }

    public virtual void SetPositionsList(){
        pathPositions.Clear();
        foreach (GameObject point in pathPoints) {
            pathPositions.Add(point.transform.position);
        }
    }    
}
