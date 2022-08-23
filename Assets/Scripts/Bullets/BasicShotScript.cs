using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShotScript : PoolObject
{
    public BulletData bulletData;

    public AnimationClip idleAnimation;
    public int damage;
    public float movementSpeed;

    public Rigidbody2D rb;

    protected bool _isEnemyBullet;
    public bool isEnemyBullet{
        get{
            return _isEnemyBullet;
        }
    }
    protected Vector2 _moveDirection;
    public Vector2 moveDirection{
        get{
            return _moveDirection;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(){

    }

    //Reset stuff like animations and whatnot so it can be reused in pool
    public override void OnObjectReuse(){}

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        rb.MovePosition(rb.position + moveDirection * movementSpeed);
    }

    public virtual void Shoot(bool isEnemyBullet = false, Vector2 fireDirection = new Vector2()){
        _isEnemyBullet = isEnemyBullet;
        _moveDirection = fireDirection;

        // moveDirection = new Vector2(0,1);
    }
}
