using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSegment : MonoBehaviour
{
    public List<GameObject> pathPoints = new List<GameObject>();
    // public List<Transform> pathTransforms = new List<Transform>();
    public GameObject startPoint, endPoint;

    public virtual void PopulatePoints(){}
    // public virtual void OffsetPoints(Vector2 offsetAmount){}

    public virtual void OffsetPoints(Vector2 offsetAmount){
        foreach (GameObject point in pathPoints) {
            point.transform.position += (Vector3)offsetAmount;
        }
    }
}
