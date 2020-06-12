using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animate;
    public Transform groundCheck;
    public float speed = 4;
    public float jumpForce = 300;
    public LayerMask whatIsGround;

    [HideInInspector]
    public bool lookingRight = true;

    private Rigidbody2D rb2d;
    public bool isGrounded = false;
    private bool jump = false;
    private float horizontalForceButton;
    private Animator anim;
    private bool isAttacking = false;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        animate.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        inputCheck();
        move();
    }


    void move()
    {
        horizontalForceButton = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(horizontalForceButton * speed, rb2d.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, whatIsGround);

            Flip();

        if (jump)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }
    }

    void Flip()
    {
        if (horizontalForceButton < 0)
         {
             lookingRight = false;
             transform.localRotation = Quaternion.Euler(0, 180, 0);
         }
         else if (horizontalForceButton > 0)
         {
             lookingRight = true;
             transform.localRotation = Quaternion.Euler(0, 0, 0);
         }
    }


    void inputCheck()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
            animate.SetBool("isJumping", true);
        }
        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Attack());
        }
    }
    private IEnumerator Attack()
    {
        if(!isAttacking)
        {
            isAttacking = true;
            animate.SetBool("Attack", true);
            yield return new WaitForSeconds(0.5F);
            isAttacking = false;
            animate.SetBool("Attack", false);
        }
    } 
}