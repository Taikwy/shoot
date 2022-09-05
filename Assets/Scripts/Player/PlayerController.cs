using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    public PlayerScript playerScript;

    private bool m_FacingRight = true;  // For determining which way the player is currently facing.


    Vector2 dashTargetPosition;

    private void Awake()
    {
        // rb = GetComponent<Rigidbody2D>();
        // player = GetComponent<Player>();
    }

    public void Move(Vector2 moveDir)
    {
        rb.MovePosition(rb.position + moveDir * playerScript.data.movementSpeed * Time.deltaTime);
    }

    // public void Dash(Vector2 dashDir)
    // {
    //     rb.MovePosition(rb.position + dashDir * player.dashDistance * Time.fixedDeltaTime);
    //     //rb.MovePosition(Vector2.Lerp(rb.position, rb.position + dashDir * player.dashDistance * Time.fixedDeltaTime, ));
    // }

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