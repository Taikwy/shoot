using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScrolling : MonoBehaviour
{

    Rigidbody2D rb; 
    
    public Sprite sprite;
    public RawImage image;
    public float xScrollRate, yScrollRate;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2 (xScrollRate, yScrollRate);
    }

    // Update is called once per frame
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(xScrollRate, yScrollRate) * Time.deltaTime, image.uvRect.size);
        
        // sprite.textureRectOffset = ;
        // sprite = new Rect(sprite.textureRect.position + new Vector2(xScrollRate, yScrollRate) * Time.deltaTime, sprite.textureRect.size);
    }

    // void Update()
    // {
    //     // If the game is over, stop scrolling.
    //     if(GameControl.instance.gameOver == true)
    //     {
    //         rb2d.velocity = Vector2.zero;
    //     }
    // }
}
