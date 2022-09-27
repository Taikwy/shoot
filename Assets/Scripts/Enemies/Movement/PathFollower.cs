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
    protected Rigidbody2D rb;
    protected Vector2 newPosition;
    // Vector2 lastPosition;
    public Vector2 startPosition, targetPosition;

    

    float moveSpeed;

    int numPathPoints, currentPointIndex;

    PathSegment path;
    bool pointReached = false;
    public bool segmentFinished = false;
    public bool pathComplete = false;
    public bool segmentReversed = false;


    public void Setup(){
        rb = gameObject.GetComponent<Rigidbody2D>();
        // lastPosition = rb.position;
    }

    public void SetupPoints(LinePath p){
        // endReached = false;
        // pathComplete = false;
        // pointReached = false;
        path = p;
        numPathPoints = path.pathPoints.Count;
        SetPoints();
        Debug.Log("setup point");
    }

    public void SetupPoints(PathSegment p, int startIndex, bool reversed){
        path = p;
        numPathPoints = path.pathPoints.Count;
        segmentReversed = reversed;
        currentPointIndex = startIndex;
        // Debug.Log("setup point " + currentPointIndex + " " + reversed);
        SetPoints(currentPointIndex);
    }

    public void SetPoints(int index = 0){
        segmentFinished = false;
        pointReached = false;
        currentPointIndex = index;
        SetStartingPos(path.pathPoints[currentPointIndex].transform);
        if(segmentReversed)
            SetTargetPos(path.pathPoints[currentPointIndex-1].transform);
        else   
            SetTargetPos(path.pathPoints[currentPointIndex+1].transform);
    }

    public void IncrementPoints(){
        SetStartingPos(path.pathPoints[currentPointIndex].transform);
        SetTargetPos(path.pathPoints[currentPointIndex+1].transform);
    }
    public void DecrementPoints(){
        SetStartingPos(path.pathPoints[currentPointIndex].transform);
        SetTargetPos(path.pathPoints[currentPointIndex-1].transform);
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
