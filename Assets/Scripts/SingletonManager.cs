using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    public PlayerScript player;

    public static SingletonManager Instance { get; private set; } // static singleton

    void Awake() {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
        // Cache references to all desired variables
        player = FindObjectOfType<PlayerScript>();
     }
}
