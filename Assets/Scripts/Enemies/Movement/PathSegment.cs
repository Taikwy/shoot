using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSegment : MonoBehaviour
{
    // public GameObject pointObj;
    public List<GameObject> pathPoints = new List<GameObject>();
    public List<Vector3> pathPositions = new List<Vector3>();
    // public List<Transform> pathTransforms = new List<Transform>();
    public GameObject startPoint, endPoint;

    // public virtual void PopulatePoints(){}
    public virtual void PopulatePointsEditor(){}
    public virtual void ResetPointsEditor(){}

    public virtual void SetPositionsList(){
        // pathPositions.Clear();
        // foreach (GameObject point in pathPoints) {
        //     pathPositions.Add(point.transform.position);
        // }

        // pathTransforms.Clear();
        // foreach (GameObject point in pathPoints) {
        //     pathTransforms.Add(point.transform);
        // }
        // Debug.Log(pathTransforms.Count);


        pathPositions.Clear();
        foreach (GameObject point in pathPoints) {
            pathPositions.Add(point.transform.position);
        }
    }
    public virtual void OffsetPoints(bool offset, Vector2 offsetAmount = default(Vector2)){
        
        // Debug.Log("before pos" + pathTransforms[0].position + " path pos " + pathPoints[0].transform.position);
        // Debug.Log("before pos" + pathPositions[0] + " path pos " + pathPoints[0].transform.position);
        // Debug.Log("offsetting");
        SetPositionsList();
        // foreach (GameObject point in pathPoints) {
        //     point.transform.position += (Vector3)offsetAmount;
        // }
        // foreach (GameObject point in pathPoints) {
        //     point.transform.position += Vector3.one;
        // }


        // pathPositions.Clear();
        // foreach (GameObject point in pathPoints) {
        //     pathPositions.Add(point.transform.position);
        // }

        // for(int i = 0; i < pathPositions.Count; i++){
        //     pathPositions[i] += Vector3.one;
        // }
        // foreach (Transform t in pathTransforms) {
        //     t.position += Vector3.one;
        // }

        
        // for(int i = 0; i < pathTransforms.Count; i++){
        //     pathTransforms[i].position = pathPoints[i].transform.position + Vector3.one;
        //     // pathTransforms[i].position = pathPoints[i].transform.position + (Vector3)offsetAmount;
        // }
        // Debug.Log("after pos" + pathTransforms[0].position + " path pos " + pathPoints[0].transform.position);
        
        // Debug.Log(pathTransforms.Count);

        if(offset){
            for(int i = 0; i < pathPositions.Count; i++){
                // pathPositions[i] = pathPoints[i].transform.position + Vector3.one;
                pathPositions[i] = pathPoints[i].transform.position + (Vector3)offsetAmount;
            }
            // Debug.Log("after pos" + pathPositions[0] + " path pos " + pathPoints[0].transform.position);
        }
        
    }

    
}
