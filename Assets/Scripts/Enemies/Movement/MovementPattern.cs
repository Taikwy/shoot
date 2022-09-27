using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern : MonoBehaviour
{
    protected Rigidbody2D rb;
    
    protected Vector2 movePosition;
    protected float currentSpeed, maxSpeed, acceleration;
    
    [HideInInspector]
    public enum MovementSeq{
        // Spawn,
        Enter,
        Main,
        Exit
    }
    public MovementSeq currentSeq;

    public List<MovementSequence> sequences = new List<MovementSequence>();
    public MovementSequence currentSequence;
    public int currentSequenceIndex = 0;
    bool sequenceLooping = false;



    bool movementMirrored;

    public float movementSpeed;


    public virtual void Setup(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        // switch(currentSequence){
        //     case MovementSequence.Enter:
        //         EnterSequence();
        //         break;
        //     case MovementSequence.Main:
        //         MainSequence();
        //         break;
        //     case MovementSequence.Exit:
        //         ExitSequence();
        //         break;
        // }

        movePosition = currentSequence.MoveSequence(movementSpeed);
        rb.MovePosition(movePosition);


        // if(currentSpeed > maxSpeed)
        //     currentSpeed = maxSpeed;
        // else
        //     currentSpeed += acceleration * Time.deltaTime;
    }

    public virtual void ChangeSequence(MovementSequence newSequence){
        currentSequence = newSequence;
        // newSequence.SetupSequence();
    }

    public virtual void ChangeSequence(MovementSeq newSequence){
    }

    protected virtual void SetSpeedAndAccel(float newMax, float newSpeed, float newAccel){
        maxSpeed = newMax;
        if(newSpeed > newMax)
            currentSpeed = newMax;
        else
            currentSpeed = newSpeed;
        acceleration = newAccel;
    }

    protected virtual void EnterSequence(){
    }

    //sin downwards
    protected virtual void MainSequence(){
    }

    //curves and goes away
    protected virtual void ExitSequence(){ 
    }
}
