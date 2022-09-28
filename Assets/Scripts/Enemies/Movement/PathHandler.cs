using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHandler : MonoBehaviour
{
    public enum TYPE{
        single,
        pingpong,
        repeat
    }
    public TYPE type;

    public float moveSpeed;

    Rigidbody2D rb;
    public PathFollower follower;
    public EnemyPath currentPath;

    Vector2 movePosition;
    bool pathReversed = false;
    public bool segmentTransitionsOn;
    bool segmentTransitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        // follower.Setup();
        SetPath();
    }

    void SetPath(){
        currentPath.Setup(pathReversed);
        follower.SetupPoints(currentPath.currentSegment, pathReversed);
        rb.position = follower.startPosition;
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
    }

    public void OnPathFinished(){
        Debug.Log("path finished");
        switch(type){
            case TYPE.single:
                break;
            case TYPE.pingpong:
                pathReversed = !pathReversed;
                break;
            case TYPE.repeat:
                break;
        }
        SetPath();
        OnMovementOverflow();
    }

    void OnMovementOverflow(){
        float remainingSpeed = moveSpeed - Vector2.Distance(rb.position, movePosition);
        movePosition = follower.FollowPath(remainingSpeed);
    }
}
