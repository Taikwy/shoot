using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public PlayerData data;
    public float pickupSpeed;
    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.CompareTag("Scrap")){
            Scrap scrap = otherCollider.gameObject.GetComponent<Scrap>();
            if(!scrap.absorbed)
                OnAbsorbScrap(scrap);
        }
    }

    void OnAbsorbScrap(Scrap scrap){
        Debug.Log("absorbed scrap");
        scrap.OnAbsorb(pickupSpeed);
    }
}
