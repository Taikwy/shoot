using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScrolling : MonoBehaviour
{
    
    public Sprite sprite;
    public RawImage image;
    public float xScrollRate, yScrollRate;

    // Update is called once per frame
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(xScrollRate, yScrollRate) * Time.deltaTime, image.uvRect.size);
        
        // sprite.textureRectOffset = ;
        // sprite = new Rect(sprite.textureRect.position + new Vector2(xScrollRate, yScrollRate) * Time.deltaTime, sprite.textureRect.size);
    }
}
