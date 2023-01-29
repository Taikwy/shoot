using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb; 
    [SerializeField] SpriteRenderer spriteRenderer;
    float width, height;
    float xScrollRate, yScrollRate;

    public void Setup(float x, float y){
        width = spriteRenderer.bounds.size.x;
        height = spriteRenderer.bounds.size.y;

        xScrollRate = x;
        yScrollRate = y;
        rb.velocity = new Vector2 (xScrollRate, yScrollRate);
    }

    private void Update(){
        if (Mathf.Abs(transform.position.x)  > width){
            // RepositionBackground(new Vector2(width, 0));
        }

        if (Mathf.Abs(transform.position.y)  > height/4){
            RepositionBackground(new Vector2(0, height/4));
        }
    }

    private void RepositionBackground(Vector2 offSet){

        transform.position = (Vector2) transform.position + offSet;
    }
}
