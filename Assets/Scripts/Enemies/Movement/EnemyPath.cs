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

    public void Setup(bool reversed, bool relativePositioning = false){
        // Debug.Log("set up path");
        numSegments = segments.Count;
        pathReversed = reversed;
        foreach(PathSegment segment in segments){
            segment.PopulatePoints();
        }
        if(pathReversed){
            currentSegmentIndex = segments.Count-1;
        }
        else{
            currentSegmentIndex = 0;
        }
        SetCurrentSegment(reversed);
    }

    public void AdjustPositioning(Vector2 offsetAmount){
        foreach(PathSegment segment in segments){
            segment.OffsetPoints(offsetAmount);
        }
    }

    public void SetCurrentSegment(bool reversed = false){
        pathComplete = false;

        currentSegment = segments[currentSegmentIndex];
    }

    public void NextSegment(){
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
