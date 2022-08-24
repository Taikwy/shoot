using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern : MonoBehaviour
{
    protected Rigidbody2D rb;
    [HideInInspector]
    public enum MovementSequence{
        // Spawn,
        Enter,
        Main,
        Exit
    }
    public MovementSequence currentSequence;

    protected Vector2 movePosition;
    protected float movementSpeed, maxMovementSpeed, acceleration;

    public virtual void Setup(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        if(movementSpeed < maxMovementSpeed){
            movementSpeed += acceleration * Time.deltaTime;
            if(movementSpeed > maxMovementSpeed)
                movementSpeed = maxMovementSpeed;
        }

        switch(currentSequence){
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
        rb.MovePosition(movePosition);
    }

    protected virtual void SetSpeedAndAccel(float max, float spd, float accel){
        maxMovementSpeed = max;
        movementSpeed = spd;
        acceleration = accel;
    }

    public virtual void ChangeSequence(MovementSequence newSequence){
        currentSequence = newSequence;
    }

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
