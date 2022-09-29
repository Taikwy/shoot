using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSequence : MonoBehaviour
{
    [Header("Movement refs")]
    protected Rigidbody2D rb;
    protected Transform objectTransform;
    protected Vector2 newPosition;
    [HideInInspector]
    public float time, distance;

    [Header("Path Refs")]
    PathFollower follower;
    public List<EnemyPath> paths = new List<EnemyPath>();
    [HideInInspector] public EnemyPath currentPath;
    float moveSpeed;
    bool pathReversed, segmentTransitioning = false;
    [Header("Path Info")]
    public bool segmentTransitionsOn;
    public enum TYPE{
        single,
        pingpong,
        repeat
    }
    public TYPE type;
    public int maxPasses;
    protected int numPasses = 0;
    public bool relativePositioning = false;
    Vector2 relativePositioningOffset;
    [HideInInspector] public bool sequenceComplete = false;

    public virtual void Setup(GameObject gameObj, int startingPathIndex = 0){
        follower = gameObj.GetComponent<PathFollower>();
        rb = gameObj.GetComponent<Rigidbody2D>();
        objectTransform = gameObj.transform;
        time = distance = 0;

        relativePositioningOffset = rb.position;
        currentPath = paths[startingPathIndex];
        SetPath();
    }

    void SetPath(){
        currentPath.Setup(pathReversed);
        currentPath.OffsetSegments(relativePositioning, relativePositioningOffset);
        
        follower.SetupPoints(currentPath.currentSegment, pathReversed);
        rb.position = follower.startPosition;
    }

    public virtual Vector2 Move(float speed){
        moveSpeed = speed;
        if(segmentTransitioning){
            if(segmentTransitionsOn){
                newPosition = Vector2.MoveTowards(rb.position, follower.startPosition, moveSpeed * Time.deltaTime);
                if(newPosition == follower.startPosition)
                    segmentTransitioning = false;
            }
            else{
                segmentTransitioning = false;

            }
        }
        else{
            newPosition = follower.FollowPath(moveSpeed);
            if(follower.segmentFinished){
                OnSegmentFinished();
            }
            if(currentPath.pathComplete){
                OnPathFinished();
            }
        }
        UpdateSequence();
        return newPosition;
    }

    //Updates stats based on newposition 
    public virtual void UpdateSequence(){
        time += Time.deltaTime;
        distance += Vector2.Distance(newPosition, objectTransform.position);
    }


    public void OnSegmentFinished(){
        currentPath.NextSegment();
        if(currentPath.pathComplete)
            return;
        follower.SetupPoints(currentPath.currentSegment, pathReversed);
        segmentTransitioning = true;
        if(!segmentTransitionsOn){
            rb.position = follower.startPosition;
        }

        OnMovementOverflow();
    }

    public void OnPathFinished(){
        switch(type){
            case TYPE.single:
                sequenceComplete = true;
                return;
                break;
            case TYPE.pingpong:
                numPasses++;
                if(numPasses >= maxPasses){
                    sequenceComplete = true;
                    return;
                }
                pathReversed = !pathReversed;
                break;
            case TYPE.repeat:
                numPasses++;
                if(numPasses >= maxPasses){
                    sequenceComplete = true;
                    return;
                }
                break;
        }
        SetPath();
        OnMovementOverflow();
    }

    void OnMovementOverflow(){
        float remainingSpeed = moveSpeed - Vector2.Distance(rb.position, newPosition);
        newPosition = follower.FollowPath(remainingSpeed);
    }

    
    
}
