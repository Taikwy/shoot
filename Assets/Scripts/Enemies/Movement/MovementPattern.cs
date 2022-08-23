using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb;
    public MovementPieces movementPieces;
    public enum MovementSequence{
        // Spawn,
        Enter,
        Main,
        Exit
    }

    [HideInInspector] 
    public MovementSequence currentSequence;

    protected Vector2 movePosition;
    protected float movementSpeed, maxMovementSpeed, acceleration;

    public virtual void Setup(){
    }

    void FixedUpdate(){
        if(movementSpeed < maxMovementSpeed){
            movementSpeed += acceleration * Time.deltaTime;
            if(movementSpeed > maxMovementSpeed)
                movementSpeed = maxMovementSpeed;
        }

        switch(currentSequence){
            // case MovementSequence.Spawn:
            //     SpawnSequence();
            //     break;
            case MovementSequence.Enter:
                EnterSequence();
                break;
            case MovementSequence.Main:
                MainSequence();
                break;
            case MovementSequence.Exit:
                ExitSequence();
                break;
        }
        // Debug.Log(currentSequence);
        rb.MovePosition(movePosition);
    }

    protected virtual void SetSpeedAndAccel(float max, float spd, float accel){
        maxMovementSpeed = max;
        movementSpeed = spd;
        acceleration = accel;
    }

    protected virtual void ChangeSequence(MovementSequence newSequence){
        currentSequence = newSequence;
    }

    // //How enemy gets in position after spawning
    // protected virtual void SpawnSequence(){
    // }

    //How enemy enteres the screen
    protected virtual void EnterSequence(){
    }

    //Main movement while on screen
    protected virtual void MainSequence(){
    }

    //Movement sequence for leaving the screen
    protected virtual void ExitSequence(){
    }
}
