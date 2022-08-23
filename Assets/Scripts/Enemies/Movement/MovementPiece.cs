using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPiece : MonoBehaviour
{
    [Header("Movement Info")]
    [SerializeField]
    protected Rigidbody2D rb;
    public Transform objectTransform;
    public Vector2 newPosition;

    public Vector2 lastPosition;
    //Piece Stats -------------------------------
    public float pieceTime, pieceDist, pieceDistX, pieceDistY;

    protected Vector2 startPosition;

    //resets just the stuff for a new piece to be used
    public void ResetPiece(){
        lastPosition = objectTransform.position;
        pieceTime = pieceDist = pieceDistX = pieceDistY = 0;
        startPosition = objectTransform.position;
    }

    public void UpdatePiece(){
        pieceTime += Time.deltaTime;
        pieceDist += Vector2.Distance(objectTransform.position, lastPosition);
        pieceDistX += Mathf.Abs(objectTransform.position.x - lastPosition.x);
        pieceDistY += Mathf.Abs(objectTransform.position.y - lastPosition.y);

        lastPosition = objectTransform.position;
    }
}
