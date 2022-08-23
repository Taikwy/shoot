using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    float movementSpeed;

    public float direction = 1;
    private void FixedUpdate()
    {
        Move(new Vector2(0,direction));
    }

    public void Move(Vector2 moveDir)
    {
        rb.MovePosition(rb.position + moveDir * movementSpeed);
    }
}
