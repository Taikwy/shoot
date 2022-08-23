using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovementPattern : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    enum MovementSequence{
        Enter,
        Main,
        Exit
    }

    MovementSequence currentSequence;

    public TestMovementPiece testMovementPiece;

    public LineStop lineStop;
    public CurveLine uTurn;

    [SerializeField]    
    SinData sinData;

    public Vector2 movePosition;
    public float movementSpeed;
    public float maxMovementSpeed;
    public float acceleration;

    // Start is called before the first frame update
    void Start()
    {
        maxMovementSpeed = 5f;
        movementSpeed = maxMovementSpeed;
        acceleration = 5f;

        ChangeSequence(MovementSequence.Enter);
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
        // Debug.Log("asda" + movePosition);
        rb.MovePosition(movePosition);
    }

    void SetSpeedAndAccel(float max, float spd, float accel){
        maxMovementSpeed = max;
        movementSpeed = spd;
        acceleration = accel;
    }

    void ChangeSequence(MovementSequence newSequence){
        Debug.Log("changing sequence " + movePosition);
        switch(newSequence){
            case MovementSequence.Enter:
                lineStop.Setup();
                break;
            case MovementSequence.Main:
                testMovementPiece.SetupSin(sinData);
                break;
            case MovementSequence.Exit:
                SetSpeedAndAccel(50f, 0f, 5f);
                uTurn.Setup();
                break;
        }
        
        currentSequence = newSequence;
    }

    //line and stay
    void EnterSequence(){
        if(lineStop.numSwaps >= 1){
            ChangeSequence(MovementSequence.Main);
            MainSequence();
            return;
        }
        // movePosition = lineStop.Movement();
    }

    //sin downwards
    void MainSequence(){
        if(testMovementPiece.numHalfCycles >= 2){
            movePosition = testMovementPiece.ClampSinX();
            // Debug.Log(movePosition + "clamped");
            ChangeSequence(MovementSequence.Exit);
            return;
        }
        movePosition = testMovementPiece.SinXMovement();
    }

    //u turn away
    void ExitSequence(){
        // movePosition = uTurn.Movement();
    }
}
