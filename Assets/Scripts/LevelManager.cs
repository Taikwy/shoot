using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform levelTransform;
    // public float scrollSpeed;
    // public Vector3 scrollDirection;
    public Vector2 scrollSpeed;
    public Level currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        scrollSpeed = currentLevel.scrollSpeed * currentLevel.scrollDirection;
    }

    // Update is called once per frame
    void Update()
    {
        // levelTransform.Translate(0, -scrollSpeed * Time.deltaTime, 0);
        levelTransform.Translate(scrollSpeed * Time.deltaTime);
        // Debug.Log(scrollSpeed * Time.deltaTime + " "  + scrollSpeed);
    }
}
