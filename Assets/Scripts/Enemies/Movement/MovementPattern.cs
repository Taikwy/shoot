using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern : MonoBehaviour
{
    [Header("Component Refs")]
    public Rigidbody2D rb;
    public PathFollower follower;
    protected Vector2 movePosition;
    [Header("movement sequence stuff")]
    public float movementSpeed;
    public bool movementMirrored;
    public float rotationSpeed;
    public bool isRotating;
    public bool clockwise;
    [Header("movement state stuff")]
    public bool dropSpawnEnabled = false;
    
    [HideInInspector]
    public enum MovementState{
        // Spawn,
        Enter,
        Main,
        DROP,
        SPAWN,
        STAY,
        PASS,
        EXIT,
        Exit
    }
    
    public MovementState startingState;
    protected MovementState currentState;
    public MovementSequence spawnSequence, staySequence, passSequence, exitSequence;
    protected MovementSequence currentSequence;

    bool timedCurrentSequence = false;

    public virtual void Setup(bool m = false){
        // Debug.Log("setting movement pattern");
        movementMirrored = m;
        if(clockwise)
            rotationSpeed = Mathf.Abs(rotationSpeed) * -1;

        ResetMP();
        ChangeStateAndSequence(startingState);
    }

    public virtual void ResetMP(){
        // if(spawnSequence)
            spawnSequence.Reset();
        // if(staySequence)
            staySequence.Reset();
        // if(passSequence)
            passSequence.Reset();
        // if(exitSequence)
            exitSequence.Reset();
    }

    void FixedUpdate(){
        if(timedCurrentSequence){
            if(currentSequence.time >= currentSequence.maxTime)
                currentSequence.sequenceComplete = true;
        }
        if(currentSequence.sequenceComplete){
            switch(currentState){
                case MovementState.DROP:
                    ChangeStateAndSequence(MovementState.STAY);
                    break;
                case MovementState.SPAWN:
                    ChangeStateAndSequence(MovementState.STAY);
                    break;
                case MovementState.STAY:
                    ChangeStateAndSequence(MovementState.EXIT);
                    break;
                case MovementState.PASS:
                //despawn
                    break;
                case MovementState.EXIT:
                //despawn
                    break;
            }
        }
        movePosition = currentSequence.Move(movementSpeed);
        // Debug.Log(gameObject.name + " " + movePosition);
        if(isRotating){            
            rb.rotation += rotationSpeed * Time.deltaTime;
        }
        rb.MovePosition(movePosition);
    }


    public virtual void ChangeStateAndSequence(MovementState newState){
        currentState = newState;
        switch(currentState){
            case MovementState.SPAWN:
                SetSequence(spawnSequence);
                break;
            case MovementState.STAY:
                SetSequence(staySequence);
                break;
            case MovementState.PASS:
                SetSequence(passSequence);
                break;
            case MovementState.EXIT:
                SetSequence(exitSequence);
                break;
        }
        timedCurrentSequence = currentSequence.timedSequence;
    }

    public virtual void SetSequence(MovementSequence newSequence){
        currentSequence = newSequence;
        currentSequence.SetupPath(0);
    }

    public void UpdateSpeed(){
        // if(currentSpeed > maxSpeed)
        //     currentSpeed = maxSpeed;
        // else
        //     currentSpeed += acceleration * Time.deltaTime;
    }

    protected virtual void EnterSequence(){}

    protected virtual void MainSequence(){}

    protected virtual void SpawnSequence(){}

    protected virtual void StaySequence(){}

    protected virtual void PassSequence(){}
    protected virtual void ExitSequence(){}


    public virtual void SetSpeedAndAccel(float max, float spd, float acc){}
     // protected virtual void SetSpeedAndAccel(float newMax, float newSpeed, float newAccel){
    //     maxSpeed = newMax;
    //     if(newSpeed > newMax)
    //         currentSpeed = newMax;
    //     else
    //         currentSpeed = newSpeed;
    //     acceleration = newAccel;
    // }
}
