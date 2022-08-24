using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPiece : MonoBehaviour
{
    [Header("Movement Info")]
    protected Rigidbody2D rb;
    protected Transform objectTransform;
    protected Vector2 newPosition, lastPosition, startPosition;

    //Piece Stats -------------------------------
    [HideInInspector]
    public float pieceTime, pieceDist, pieceDistX, pieceDistY;

    //resets just the stuff for a new piece to be used
    public void SetupPiece(){
        // Debug.Log("setting up piece");
        rb = gameObject.GetComponent<Rigidbody2D>();
        objectTransform = gameObject.transform;
        lastPosition = objectTransform.position;
        startPosition = objectTransform.position;
        pieceTime = pieceDist = pieceDistX = pieceDistY = 0;
    }

    public void UpdatePiece(){
        pieceTime += Time.deltaTime;
        pieceDist += Vector2.Distance(objectTransform.position, lastPosition);
        pieceDistX += Mathf.Abs(objectTransform.position.x - lastPosition.x);
        pieceDistY += Mathf.Abs(objectTransform.position.y - lastPosition.y);

        lastPosition = objectTransform.position;
    }
}
