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

    enum SegmentTYPE{
        single,
        pingpong,
        repeat
    }
    
    SegmentTYPE segmentType;
    
    [Header("Movement Info")]
    public Rigidbody2D rb;
    protected Vector2 newPosition;
    public Vector2 startPosition, targetPosition;

    float moveSpeed;

    int numPathPoints, currentPointIndex;

    PathSegment segment;
    bool pointReached = false;
    public bool segmentFinished = false;
    public bool segmentReversed = false;
    bool isStaying = false;
    float stayTime = 0;
     List<Vector3> segmentPositions = new List<Vector3>();

    public void ResetFollower(){
        segmentFinished = false;
        pointReached = false;
        stayTime = 0f;
        isStaying = false;
        moveSpeed = 0f;

        segment = null;
        numPathPoints = 0;
        segmentPositions.Clear();
        segmentReversed = false;
        currentPointIndex = 0;

        startPosition = targetPosition = default(Vector2);
    }

    public void SetupPoints(PathSegment s, List<Vector3> currentSegmentPositions, bool reversed){
        segment = s;
        numPathPoints = segment.pathPositions.Count;
        segmentPositions = currentSegmentPositions;
        segmentReversed = reversed;
        if(segmentReversed){
            currentPointIndex = segment.pathPositions.Count-1;
        }
        else{
            currentPointIndex = 0;
        }

        SetPoints();
    }

    public void SetPoints(int index = 0){
        segmentFinished = false;
        pointReached = false;
        stayTime = 0f;
        isStaying = numPathPoints <= 1;
        
        SetStartingPos(segmentPositions[currentPointIndex]);
        if(isStaying)
            return;

        if(segmentReversed)
            SetTargetPos(segmentPositions[currentPointIndex-1]);
        else   
            SetTargetPos(segmentPositions[currentPointIndex+1]);
    }

    public void SetStartingPos(Vector3 pos){
        startPosition = pos;
    }
    public void SetTargetPos(Vector3 pos){
        targetPosition = pos;
    }

    public Vector2 FollowPath(float newSpeed){
        if(isStaying){
            // Debug.Log("staying :     " + segment.stayTime + " " + stayTime + " || " + currentPointIndex + " " + numPathPoints);
            stayTime += Time.deltaTime;
            if(stayTime >= segment.stayTime){
                OnTargetReached();
            }
            return newPosition;
        }

        moveSpeed = newSpeed;
        Move();
        if(newPosition == targetPosition)
            OnTargetReached();
        
        return newPosition;
    }

    public Vector2 Move(){
        newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
        
        return newPosition;
    }

    //If previous point was reached without moving max distance, remaining distance is used for next point
    public Vector2 MoveOverflow(){
        // Debug.Log("point overflowed");
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
