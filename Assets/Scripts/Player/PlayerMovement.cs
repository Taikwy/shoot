using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    public PlayerScript playerScript;
    public PlayerController controller;
    public Animator animator;

    // public float movementSpeed = 5f;
    // public float dashSpeed = 12.5f;

    Vector2 movement;
    bool isIdle;

    bool isDashing = false;

    Vector2 targetPos;
    Vector2 startPos;

    float timestarted;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if(!isDashing)
            if (Input.GetKeyDown("l")){
                isDashing = true;
                targetPos = rb.position + movement * playerScript.dashDistance; 
                startPos = rb.position;
                timestarted = Time.time;
            }

        //Currently if the player isn't moving horizontally it counts as idling
        isIdle = (movement.x == 0) && !isDashing;

        animator.SetFloat("Horizontal Movement", movement.x);
        animator.SetBool("Idle", isIdle);
    }

    private void FixedUpdate()
    {
        if(isDashing){
            Dash();
        }
        else
            controller.Move(movement);

        //rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    private void Dash()
    {
        //controller.Dash(movement);
        //isDashing = false;

        float timeSince = Time.time - timestarted;
        float percentage = timeSince * playerScript.dashSpeed * Time.deltaTime;

        rb.MovePosition(Vector2.Lerp(startPos, targetPos, percentage));
        if(rb.position == targetPos)
            isDashing = false;
    }

}
