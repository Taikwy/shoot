using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSequence : MonoBehaviour
{
    [Header("Component refs")]
    public Transform objectTransform;
    public Rigidbody2D rb;
    public PathFollower follower;

    [Header("Path Stuff")]
    public List<EnemyPath> paths = new List<EnemyPath>();
    [HideInInspector] public EnemyPath currentPath;
    Vector2 relativePositioningOffset;
    [HideInInspector] public bool sequenceComplete, pathComplete = false;

    [Header("Path Stats")]
    protected int numPasses = 0;
    bool pathReversed, segmentTransitioning = false;
    int currentSegmentIndex = 0;
    PathSegment currentSegment;
    List<Vector3> currentSegmentPositions = new List<Vector3>();
    int numSegments;

    [Header("Path Variables")]
    public bool segmentTransitionsOn;
    public enum TYPE{
        single,
        pingpong,
        repeat
    }
    public TYPE type;
    public int maxPasses;
    public float maxTime;
    public bool timedSequence = false;
    public bool relativePositioning = false;


    protected Vector2 newPosition;
    [HideInInspector]
    public float time, distance;
    float moveSpeed;

    public virtual void SetComponentsEditor(){
        objectTransform = gameObject.transform.parent.parent;
        follower = objectTransform.GetComponent<PathFollower>();
        rb = objectTransform.GetComponent<Rigidbody2D>();
        // Debug.Log(rb + " rb set");
    }

    public virtual void Reset(){
        SetComponentsEditor();

        pathReversed = false;
        sequenceComplete = false;
        pathComplete = false;
        currentSegmentIndex = 0;
        numPasses = 0;
        time = distance = 0;

    }

    public virtual void SetupPath(int startingPathIndex = 0){
        relativePositioningOffset = rb.position;
        currentPath = paths[startingPathIndex];
        SetPath();
    }

    public virtual void SetPath(){
        if(pathReversed){
            currentSegmentIndex = currentPath.segments.Count-1;
        }
        else{
            currentSegmentIndex = 0;
        }
        
        Debug.Log("rbpos " + rb.position + " at ");
        
        SetCurrentSegment();
        rb.position = follower.startPosition;
    }

    public void SetCurrentSegment(bool reversed = false){
        pathComplete = false;
        currentSegment = currentPath.segments[currentSegmentIndex];
        currentSegmentPositions = currentSegment.OffsetPointsList(relativePositioning, relativePositioningOffset);
        follower.SetupPoints(currentSegment, currentSegmentPositions, pathReversed);
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
            if(currentSegmentIndex > currentPath.segments.Count - 1){
                pathComplete = true;
                return;
            }
        }
        pathComplete = false;
        SetCurrentSegment();
    }

    public virtual Vector2 Move(float speed){
        if(sequenceComplete)
            return newPosition;
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
            if(pathComplete){
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
        NextSegment();
        if(pathComplete)
            return;
        
        segmentTransitioning = true;
        if(!segmentTransitionsOn){
            rb.position = follower.startPosition;
        }

        OnMovementOverflow();
    }

    public void OnPathFinished(){
        // Debug.Log(gameObject.transform.parent.transform.parent.gameObject.name + " finished " + currentPath.gameObject.name );
        switch(type){
            case TYPE.single:
                sequenceComplete = true;
                return;
                break;
            case TYPE.pingpong:
                pathReversed = !pathReversed;
                break;
            case TYPE.repeat:
                break;
        }
        numPasses++;
        if(numPasses >= maxPasses){
            sequenceComplete = true;
            return;
        }
        SetPath();
        OnMovementOverflow();
    }

    void OnMovementOverflow(){
        float remainingSpeed = moveSpeed - Vector2.Distance(rb.position, newPosition);
        newPosition = follower.FollowPath(remainingSpeed);
    }
}
