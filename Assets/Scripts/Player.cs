using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject BulletPrefab;

    private Rigidbody2D _Rigid;
    private Animator _Animator;
    private SpriteRenderer _Sr;

    public float speed = 3;

    private bool isOnGround = false;
    private int jumpCount = 1;
    private bool isJumping = false;
    private bool isRight = true;
    

    void Start()
    {
        _Rigid = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        _Sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Move(x);
        Flip(x);
        Jump();
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject bulletObj = Instantiate(BulletPrefab);
            bulletObj.transform.position = transform.position;
            
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if(isRight)
                bullet.SetDirection(Vector2.right);
            else bullet.SetDirection(Vector2.left);
        }
    }

    void Move(float x)
    {
        _Animator.SetFloat("move", x);
        _Animator.SetBool("onGround", isOnGround);
        _Animator.SetFloat("verticalSpeed", _Rigid.velocity.y);
        _Rigid.velocity = new Vector2(x * speed, _Rigid.velocity.y);
    }

    void Flip(float x)
    {
        if (x < 0)
        {
            _Sr.flipX = true;
            isRight = false;
        }

        if (x > 0)
        {
            _Sr.flipX = false;
            isRight = true;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            _Rigid.AddForce(new Vector2(0, 300));
            jumpCount--;
            isJumping = true;
            _Animator.SetTrigger("jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.tag=="Ground")
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            if (!isJumping)
                jumpCount = 2;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}