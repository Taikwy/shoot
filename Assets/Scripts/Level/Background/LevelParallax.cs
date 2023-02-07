using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelParallax : MonoBehaviour
{
    [HideInInspector] public enum ScrollDirection{
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    public ScrollDirection scrollDirection;
    public List<ScrollingObject> scrollingObjects = new List<ScrollingObject>();
    public List<float> scrollRates = new List<float>();
    // public List<float> xScrollRates = new List<float>();
    // public List<float> yScrollRates = new List<float>();

    private void Awake ()
    {
    }

    void Start(){
        switch(scrollDirection){
            case ScrollDirection.UP:
                for(int i = 0; i < scrollingObjects.Count; i++){
                    scrollingObjects[i].Setup(0, scrollRates[i]);
                }
                break;
            case ScrollDirection.DOWN:
                for(int i = 0; i < scrollingObjects.Count; i++){
                    scrollingObjects[i].Setup(0, -1*scrollRates[i]);
                }
                break;
        }
    }

    void Update(){
        // newPosition = rb.position + moveDirection * moveSpeed * Time.deltaTime;
        // newPosition += levelScrollSpeed * Time.deltaTime;
    }
}
