using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public enum TYPE{
        single,
        pingpong,
        repeat
    }
    public TYPE type;
    
    [Header("Movement Info")]
    public Rigidbody2D rb;
    protected Vector2 newPosition;
    // Vector2 lastPosition;
    public Vector2 startPosition, targetPosition;

    

    float moveSpeed;

    int numPathPoints, currentPointIndex;

    PathSegment segment;
    bool pointReached = false;
    public bool segmentFinished = false;
    public bool pathComplete = false;
    public bool segmentReversed = false;


    // public void Setup(){
    //     rb = gameObject.GetComponent<Rigidbody2D>();
    // }

    public void SetupPoints(PathSegment p, bool reversed){
        segment = p;
        numPathPoints = segment.pathPoints.Count;
        segmentReversed = reversed;
        if(segmentReversed){
            currentPointIndex = segment.pathPoints.Count-1;
        }
        else{
            currentPointIndex = 0;
        }
        SetPoints();
    }

    public void SetPoints(int index = 0){
        segmentFinished = false;
        pointReached = false;
        // currentPointIndex = index;
        SetStartingPos(segment.pathPoints[currentPointIndex].transform);
        if(segmentReversed)
            SetTargetPos(segment.pathPoints[currentPointIndex-1].transform);
        else   
            SetTargetPos(segment.pathPoints[currentPointIndex+1].transform);
    }

    public void IncrementPoints(){
        SetStartingPos(segment.pathPoints[currentPointIndex].transform);
        SetTargetPos(segment.pathPoints[currentPointIndex+1].transform);
    }
    public void DecrementPoints(){
        SetStartingPos(segment.pathPoints[currentPointIndex].transform);
        SetTargetPos(segment.pathPoints[currentPointIndex-1].transform);
    }

    public void SetStartingPos(Transform transform){
        startPosition = transform.position;
    }

    public void SetTargetPos(Transform transform){
        targetPosition = transform.position;
    }

    public Vector2 FollowPath(float newSpeed){
        moveSpeed = newSpeed;
        Move();
        if(newPosition == targetPosition){
            OnTargetReached();
        }
        
        return newPosition;
    }

    public Vector2 Move(){
        newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
        
        return newPosition;
    }

    //If previous point was reached without moving max distance, remaining distance is used for next point
    public Vector2 MoveOverflow(){
        Debug.Log("point overflowed");
        float remainingDistance = moveSpeed - Vector2.Distance(rb.position, newPosition);
        newPosition = Vector2.MoveTowards(rb.position, targetPosition, remainingDistance * Time.deltaTime);
        if(newPosition == targetPosition){
            OnTargetReached();
        }
        
        return newPosition;
    }

    public void OnTargetReached(){
        if(segmentReversed){
            currentPointIndex--;
            if(currentPointIndex <= 0){
                segmentFinished = true;
                return;
            }
        }
        else{
            currentPointIndex++;
            if(currentPointIndex >= numPathPoints-1){
                segmentFinished = true;
                return;
            }
        }
        segmentFinished = false;
        SetPoints(currentPointIndex);
        MoveOverflow();
    }
}
