using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private BoxCollider2D groundCollider;
    private float groundHorizontalLength;

    private SpriteRenderer spriteRenderer;
    private float width, height;
    //Awake is called before Start.
    private void Awake ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        width = spriteRenderer.bounds.size.x;
        height = spriteRenderer.bounds.size.y;

        Debug.Log(width + " " + height);
    }

    //Update runs once per frame
    private void Update(){
        if (Mathf.Abs(transform.position.x)  > width){
            RepositionBackground(new Vector2(width, 0));
        }

        if (Mathf.Abs(transform.position.y)  > 16){
            RepositionBackground(new Vector2(0, -16));
        }
    }

    private void RepositionBackground(Vector2 offSet){

        transform.position = (Vector2) transform.position + offSet;
    }
}
