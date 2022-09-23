using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterimage : PoolObject
{
    [Header("component refs")]
    public SpriteRenderer spriteRenderer;
    
    float activeTime, timeActivated, currentAlpha, startingAlpha, alphaMultiplier;
    Color currentColor;

    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){
        
    }

    public virtual void SetData(float activeT, float startA, float alphaMult, Color c){
        activeTime = activeT;
        timeActivated = Time.time;

        startingAlpha = startA;
        currentAlpha = startingAlpha;
        alphaMultiplier = alphaMult;

        currentColor = c;
        spriteRenderer.color = currentColor;
    }

    private void Update(){
        currentAlpha*= alphaMultiplier;
        currentColor.a = currentAlpha;

        spriteRenderer.color = currentColor;

        if(Time.time >= timeActivated + activeTime || currentAlpha <= .025f){
            gameObject.SetActive(false);
        }
    }
}
