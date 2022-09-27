using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHandler : MonoBehaviour
{

    public enum TYPE{
        single,
        pingpong,
        repeat,
        loop
    }
    public TYPE type;

    public float moveSpeed;

    Rigidbody2D rb;
    public PathFollower follower;
    public EnemyPath currentPath;
    // public List<PathSegment> segments = new List<PathSegment>();
    // PathSegment currentSegment;

    Vector2 movePosition;

    // int currentSegmentIndex;
    // int numSegments;
    // bool pathsComplete = false;
    // bool segmentComplete = false;
    // bool pathComplete = false;
    bool pathReversed = false;
    public bool segmentTransitionsOn;
    bool segmentTransitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        follower.Setup();
        SetupPath();
    }

    void SetupPath(int pathIndex = 0){
        // numSegments = segments.Count;
        SetPath();
    }

    void SetPath(){
        currentPath.Setup(pathReversed);
        // currentSegmentIndex = pathIndex;
        // currentSegment = segments[currentSegmentIndex];
        // currentSegment = path.segments[currentSegmentIndex];
        // currentSegment.PopulatePoints();
        // if(pathReversed)
            follower.SetupPoints(currentPath.currentSegment, pathReversed);
            rb.position = follower.startPosition;
        // else
        //     follower.SetupPoints(path.currentSegment, 0, pathReversed);
    }

    public void FixedUpdate(){
        if(segmentTransitioning){
            if(segmentTransitionsOn){
                movePosition = Vector2.MoveTowards(rb.position, follower.startPosition, moveSpeed * Time.deltaTime);
                if(movePosition == follower.startPosition)
                    segmentTransitioning = false;
            }
            else
                segmentTransitioning = false;
        }
        else{
            movePosition = follower.FollowPath(moveSpeed);
            if(follower.segmentFinished){
                OnSegmentFinished();
            }
            if(currentPath.pathComplete){
                OnPathFinished();
            }
        }
        
        rb.MovePosition(movePosition);
    }

    public void OnSegmentFinished(){
        Debug.Log("segment finished");
        currentPath.NextSegment();
        if(currentPath.pathComplete)
            return;
        follower.SetupPoints(currentPath.currentSegment, pathReversed);
        segmentTransitioning = true;
        if(!segmentTransitionsOn)
            rb.position = follower.startPosition;

        OnMovementOverflow();
        // float remainingSpeed = moveSpeed - Vector2.Distance(rb.position, movePosition);
        // movePosition = follower.FollowPath(remainingSpeed);
    }

    public void OnPathFinished(){
        Debug.Log("path finished");
        switch(type){
            case TYPE.single:
                // pathComplete = true;
                break;
            case TYPE.pingpong:
                // pathComplete = false;
                pathReversed = !pathReversed;
                if(pathReversed){
                    // Debug.Log("should be reversing ");
                    // SetPath();
                }
                else{
                    // SetPath();
                }
                    
                // float remainingSpeed = moveSpeed - Vector2.Distance(rb.position, movePosition);
                // movePosition = follower.FollowPath(remainingSpeed);
                break;
            case TYPE.repeat:
                // pathComplete = false;
                // SetPath();
                //     remainingSpeed = moveSpeed - Vector2.Distance(rb.position, movePosition);
                // movePosition = follower.FollowPath(remainingSpeed);
                break;
            // case TYPE.loop:
            //     pathComplete = false;
            //     SetPath();
            //         remainingSpeed = moveSpeed - Vector2.Distance(rb.position, movePosition);
            //     movePosition = follower.FollowPath(remainingSpeed);
            //     break;
        }
        SetPath();
        OnMovementOverflow();
    }

    void OnMovementOverflow(){
        float remainingSpeed = moveSpeed - Vector2.Distance(rb.position, movePosition);
        movePosition = follower.FollowPath(remainingSpeed);
    }
}
