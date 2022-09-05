using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    // public GameObject player;
    public PlayerScript playerScript;
    public PlayerShooting playerShooting;

    public static SingletonManager Instance { get; private set; } // static singleton

    void Awake() {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
        // Cache references to all desired variables
        playerScript = FindObjectOfType<PlayerScript>();
        playerShooting = FindObjectOfType<PlayerShooting>();
        // player = GameObject.FindWithTag("Player");
     }
}
