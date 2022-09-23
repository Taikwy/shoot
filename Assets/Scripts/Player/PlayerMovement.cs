using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("component refs")]
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Animator animator;

    [Header("player data")]
    public PlayerData data;

    Vector2 moveDirection, dashDirection, dashStartPos;
    float dashTimeStarted, dashSpeed;
    bool isIdle;

    [Header("afterimage info")]
    Vector2 lastAfterimagePos;
    float lastAfterimageTime;

    public void Setup(){
        PoolManager.Instance.CreatePool(data.afterImagePrefab, 20);
    }
    public void UpdateMovement()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        moveDirection.Normalize();

        if (Input.GetKeyDown("l") && !data.isDashing){
            if(moveDirection.x != 0 || moveDirection.y != 0){
                StartDashing();
            }
        }

        //Currently if the player isn't moving horizontally it counts as idling
        // isIdle = (movement.x == 0) && !isDashing;

        animator.SetFloat("Horizontal Movement", moveDirection.x);
        animator.SetBool("Idle", isIdle);
    }

    private void FixedUpdate()
    {
        if(data.isDashing){
            if(data.dashLag){
                if(Time.time - dashTimeStarted >= data.dashTime + data.dashEndlagAmount){
                    data.isDashing = false;
                    data.dashLag = false;
                }
            }
            else{
                Dash();
            }
        }
        else{
            if(data.isAbsorbing){
                rb.MovePosition(rb.position + moveDirection * data.absorbMovementSpeed * Time.deltaTime);

            }
            else
                rb.MovePosition(rb.position + moveDirection * data.movementSpeed * Time.deltaTime);
        }
    }

    private void StartDashing(){
        data.isDashing = true;
        data.isInvincible = true;
        data.isAbsorbing = false;

        dashDirection = moveDirection;
        dashStartPos = rb.position;
        dashTimeStarted = Time.time;
        data.dashSpeed = data.dashDistance / data.dashTime;

        Debug.Log("starting dash");
        lastAfterimagePos = transform.position;
        // CreateAfterImage();
    }
    private void Dash()
    {
        if(Time.time - dashTimeStarted >= data.dashTime){
            data.isInvincible = false;
            data.dashLag = true;
        }
        else{
            if(Mathf.Abs(Time.time - lastAfterimageTime) >= data.timeBetweenAfterimages){
                CreateAfterImage();
            }
            
            rb.MovePosition(rb.position + dashDirection * data.dashSpeed * Time.deltaTime);
        }
    }

    private GameObject CreateAfterImage(){
        Debug.Log("creating afterimage");
        GameObject afterimage = PoolManager.Instance.ReuseObject(data.afterImagePrefab, transform.position, Quaternion.identity);
        PlayerAfterimage afterimageScript = afterimage.gameObject.GetComponent<PlayerAfterimage>();
        afterimageScript.SetData(data.activeTime, data.startingAlpha, data.alphaMultiplier, data.afterimageColor);
        
        afterimageScript.spriteRenderer.sprite = spriteRenderer.sprite;

        lastAfterimagePos = transform.position;
        lastAfterimageTime = Time.time;
        return afterimage;
    }

}
