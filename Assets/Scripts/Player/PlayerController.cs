using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    
    [Header("player data")]
    public PlayerData data;

    
    

    private void Awake(){}

    public void UpdateSprite(){
        
        if(data.isInvincible){
            Color temp = spriteRenderer.color ;
            temp.a = 0.5f;
            spriteRenderer.color = temp;
            // Debug.Log("currentky invincibsle");
        }
        else 
        if(data.currentShield <= 0){
            spriteRenderer.color = Color.blue;
            // Debug.Log("currentky shield broken");
        }
        else if(data.isDashing){
            spriteRenderer.color = Color.magenta;
            // Debug.Log("currentky dasgubg");
        }
        else if(data.isAbsorbing){
            spriteRenderer.color = Color.green;
            // Debug.Log("currentky abosbring");
        }
        else if(data.isShooting){
            spriteRenderer.color = Color.red;
            // Debug.Log("currentky shoting");
        }
        else{
            spriteRenderer.color = Color.white;
            // Debug.Log("currentky chilling");
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}