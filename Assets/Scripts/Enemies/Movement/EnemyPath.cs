using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
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
    public List<PathSegment> pathSegments = new List<PathSegment>();
    PathSegment currentSegment;

    Vector2 movePosition;

    int currentSegmentIndex;
    int numSegments;
    bool pathComplete = false;
    bool pathReversed = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        follower.Setup();
        SetupPath();
    }

    void SetupPath(int pathIndex = 0){
        numSegments = pathSegments.Count;
        SetPath(0);
        rb.position = currentSegment.startPoint.transform.position;
    }

    void SetPath(int pathIndex = 0){
        // Debug.Log(paths.Count + " " + pathIndex);
        currentSegmentIndex = pathIndex;
        currentSegment = pathSegments[currentSegmentIndex];
        currentSegment.PopulatePoints();
        if(pathReversed)
            follower.SetupPoints(currentSegment, currentSegment.pathPoints.Count-1, pathReversed);
        else
            follower.SetupPoints(currentSegment, 0, pathReversed);
    }



    public void FixedUpdate(){

        movePosition = follower.FollowPath(moveSpeed);
        if(pathComplete){
            OnPathFinished();
        }
        if(follower.segmentFinished){
            OnSegmentFinished();
        }
        rb.MovePosition(movePosition);
    }

    public void OnSegmentFinished(){
        Debug.Log(currentSegmentIndex + " " + numSegments + " " + pathComplete + " " + pathReversed);
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
        SetPath(currentSegmentIndex);

        float remainingSpeed = moveSpeed - Vector2.Distance(rb.position, movePosition);
        movePosition = follower.FollowPath(remainingSpeed);
        
    }

    public void OnPathFinished(){
        switch(type){
            case TYPE.single:
                pathComplete = true;
                break;
            case TYPE.pingpong:
                pathComplete = false;
                pathReversed = !pathReversed;
                if(pathReversed){
                    // Debug.Log("should be reversing ");
                    SetPath(numSegments-1);
                }
                else{
                    SetPath();
                }
                    
                float remainingSpeed = moveSpeed - Vector2.Distance(rb.position, movePosition);
                movePosition = follower.FollowPath(remainingSpeed);
                break;
            case TYPE.repeat:
                pathComplete = false;
                SetPath();
                rb.position = follower.startPosition;
                    remainingSpeed = moveSpeed - Vector2.Distance(rb.position, movePosition);
                movePosition = follower.FollowPath(remainingSpeed);
                break;
        }
    }
}
