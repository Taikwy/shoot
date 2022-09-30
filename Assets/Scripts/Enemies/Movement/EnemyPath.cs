using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public List<PathSegment> segments = new List<PathSegment>();
    [HideInInspector] public PathSegment currentSegment;
    [HideInInspector] public int currentSegmentIndex, numSegments;
    [HideInInspector] public bool pathComplete = false;
    bool pathReversed = false;

    public void Setup(){
        // pathComplete = false;
        // // Debug.Log(gameObject.name);
        // numSegments = segments.Count;
        // pathReversed = reversed;
        // if(pathReversed){
        //     currentSegmentIndex = segments.Count-1;
        // }
        // else{
        //     currentSegmentIndex = 0;
        // }
        // SetCurrentSegment(reversed);
    }

    public void PopulateSegmentsEditor(){
        segments.Clear();
        foreach (Transform child in gameObject.transform)
        {
            if (child.tag == "Segment")
                segments.Add(child.gameObject.GetComponent<PathSegment>());
        }
    }

    public void MirrorXEditor(){
        PopulateSegmentsEditor();
        Vector3 tempPos = gameObject.transform.position;
        tempPos.x *= -1;
        gameObject.transform.position = tempPos;

        Vector3 tempScale = gameObject.transform.localScale;
        tempScale.x *= -1;
        gameObject.transform.localScale = tempScale;
    }

    public void MirrorYEditor(){
        PopulateSegmentsEditor();
        Vector3 tempPos = gameObject.transform.position;
        tempPos.y *= -1;
        gameObject.transform.position = tempPos;

        Vector3 tempScale = gameObject.transform.localScale;
        tempScale.y *= -1;
        gameObject.transform.localScale = tempScale;
    }

    public void MirrorSegmentsEditorX(){
        // PopulateSegmentsEditor();
        foreach(PathSegment segment in segments){
            Vector3 tempPos = segment.transform.position;
            tempPos.x *= -1;
            segment.transform.position = tempPos;

            Vector3 tempScale = segment.transform.localScale;
            tempScale.x *= -1;
            segment.gameObject.transform.localScale = tempScale;
        }
    }
    public void MirrorSegmentsEditorY(){
        PopulateSegmentsEditor();
        foreach(PathSegment segment in segments){
            // segment.MirrorPointsY();
            Vector3 tempPos = segment.transform.position;
            tempPos.y *= -1;
            segment.transform.position = tempPos;

            Vector3 tempScale = segment.transform.localScale;
            tempScale.y *= -1;
            segment.transform.localScale = tempScale;
        }
    }

    public void OffsetSegments(bool offset, Vector2 offsetAmount){
        foreach(PathSegment segment in segments){
            segment.OffsetPoints(offset, offsetAmount);
        }
    }

    public void SetCurrentSegment(bool reversed = false){
        // pathComplete = false;
        // Debug.Log(gameObject.name + " " + numSegments + " " + currentSegmentIndex);
        currentSegment = segments[currentSegmentIndex];
    }

    public bool NextSegment(){
        if(pathReversed){
            currentSegmentIndex--;
            if(currentSegmentIndex < 0){
                // pathComplete = true;
                return true;
            }
        }
        else{
            currentSegmentIndex++;
            if(currentSegmentIndex > numSegments - 1){
                Debug.Log(currentSegment.name + " " + numSegments + " " + currentSegmentIndex);
                // pathComplete = true;
                return true;
            }
        }
        // pathComplete = false;
        SetCurrentSegment();
        return false;
    }
}
