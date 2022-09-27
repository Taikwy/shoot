using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public List<PathSegment> segments = new List<PathSegment>();
    public PathSegment currentSegment;

    int currentSegmentIndex;
    // public int startingSegmentIndex;
    public int numSegments;
    public bool pathComplete = false;
    bool pathReversed = false;

    public void Setup(bool reversed){
        // Debug.Log("setting up path , is reverse: " + reversed);
        // pathComplete = false;
        numSegments = segments.Count;
        pathReversed = reversed;
        foreach(PathSegment segment in segments){
            segment.PopulatePoints();
        }
        if(pathReversed){
            currentSegmentIndex = segments.Count-1;
            // Debug.Log(currentSegmentIndex);
        }
        else{
            currentSegmentIndex = 0;
        }
        // currentSegmentIndex = startingSegmentIndex;
        SetCurrentSegment(reversed);
    }

    // public void SetupSegments(bool reversed = false){
    //     Debug.Log("setting up segments ");
    //     pathComplete = false;
    //     pathReversed = reversed;
    //     numSegments = segments.Count;
    //     foreach(PathSegment segment in segments){
    //         segment.PopulatePoints();
    //     }

    //     if(pathReversed){
    //         currentSegmentIndex = currentSegment.pathPoints.Count-1;
    //     }
    //     else{
    //         currentSegmentIndex = 0;
    //     }
    //     currentSegment = segments[currentSegmentIndex];
    // }

    public void SetCurrentSegment(bool reversed = false){
        // Debug.Log("setting up segment " + currentSegmentIndex);
        pathComplete = false;

        currentSegment = segments[currentSegmentIndex];
    }

    public void NextSegment(){
        // Debug.Log(currentSegmentIndex + " " + numSegments + " " + pathComplete + " " + pathReversed);
        if(pathReversed){
            currentSegmentIndex--;
            if(currentSegmentIndex < 0){
                pathComplete = true;
                return;
            }
        }
        else{
            currentSegmentIndex++;
            if(currentSegmentIndex > numSegments - 1){
                pathComplete = true;
                return;
            }
        }
        pathComplete = false;
        SetCurrentSegment();
    }
}
