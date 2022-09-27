using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSequence : MonoBehaviour
{
    [Header("Movement Info")]
    protected Rigidbody2D rb;
    protected Transform objectTransform;
    protected Vector2 newPosition, lastPosition, startPosition;
    protected bool endReached = false;
    protected bool sequenceComplete = false;

    //Piece Stats -------------------------------
    [HideInInspector]
    public float time, currentDist, xDist, yDist;

    //resets just the stuff for a new piece to be used
    public void SetupSequenceInfo(GameObject gameObj){
        rb = gameObj.GetComponent<Rigidbody2D>();
        objectTransform = gameObj.transform;
        lastPosition = objectTransform.position;
        startPosition = objectTransform.position;
        time = currentDist = xDist = yDist = 0;
    }

    public virtual void Setup(GameObject gameObj){}

    //Updates stats based on newposition 
    public virtual void UpdateSequence(){
        time += Time.deltaTime;
        currentDist += Vector2.Distance(newPosition, objectTransform.position);
        xDist += Mathf.Abs(newPosition.x - objectTransform.position.x);
        yDist += Mathf.Abs(newPosition.y - objectTransform.position.y);

        lastPosition = objectTransform.position;
    }

    //Updates stats based on newposition 
    public virtual void ResetSequence(){
        time = currentDist = xDist = yDist = 0;
        // lastPosition = objectTransform.position;
    }

    public virtual Vector2 MoveSequence(float speed){
        return newPosition;
    }
}
