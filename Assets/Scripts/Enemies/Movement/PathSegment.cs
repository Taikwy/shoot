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

    public virtual void OffsetPoints(bool offset, Vector2 offsetAmount = default(Vector2)){
        SetPositionsList();
        if(offset)
            return;
        for(int i = 0; i < pathPositions.Count; i++){
                pathPositions[i] = pathPoints[i].transform.position + (Vector3)offsetAmount;
            }
    }

    public virtual void SetPositionsList(){
        pathPositions.Clear();
        foreach (GameObject point in pathPoints) {
            pathPositions.Add(point.transform.position);
        }
    }

    
}
