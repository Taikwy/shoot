using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern : MonoBehaviour
{
    protected Rigidbody2D rb;
    
    protected Vector2 movePosition;
    protected float currentSpeed, maxSpeed, acceleration;
    
    [HideInInspector]
    public enum MovementSequence{
        // Spawn,
        Enter,
        Main,
        Exit
    }
    public MovementSequence currentSequence;

    public virtual void Setup(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
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

        if(currentSpeed > maxSpeed)
            currentSpeed = maxSpeed;
        else
            currentSpeed += acceleration * Time.deltaTime;
    }

    protected virtual void SetSpeedAndAccel(float newMax, float newSpeed, float newAccel){
        maxSpeed = newMax;
        if(newSpeed > newMax)
            currentSpeed = newMax;
        else
            currentSpeed = newSpeed;
        acceleration = newAccel;
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
