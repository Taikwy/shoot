using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSegment : MonoBehaviour
{
    public List<GameObject> pathPoints = new List<GameObject>();
    public List<Transform> pathTransforms = new List<Transform>();
    public GameObject startPoint, endPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void PopulatePoints(){}
}
