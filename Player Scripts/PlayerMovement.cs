using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Animator player_anim;
    private Rigidbody2D my_Body;
    public Transform ground_Check_Position;
    [SerializeField]
    private float jump_Force = 5f;
    private bool isGrounded;
    private bool jumped;
    public LayerMask ground_Layer;
    private void Awake()
    {
        player_anim = GetComponent<Animator>();
        my_Body = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        GroundCheck();
        PlayerJump();
    }
    private void FixedUpdate()
    {
        PlayerWalk();
    }
    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0)
        {
            my_Body.velocity = new Vector2(speed, my_Body.velocity.y);
            ChangeDirection(1);
        }
        else if (h < 0)
        {
            my_Body.velocity = new Vector2(-speed, my_Body.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            my_Body.velocity = new Vector2(0f, my_Body.velocity.y);
        }
        player_anim.SetInteger("Speed", (int)Mathf.Abs(my_Body.velocity.x));
    }
    void ChangeDirection(int direction)
    {
        Vector3 tempScale = gameObject.transform.localScale;
        tempScale.x = direction;
        gameObject.transform.localScale = tempScale;
    }
    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                player_anim.SetBool("Jump", true);
                my_Body.velocity = new Vector2(my_Body.velocity.x, jump_Force);
                jumped = true;
            }
        }
    }
    void GroundCheck()
    {
        isGrounded = Physics2D.Raycast(ground_Check_Position.position, Vector2.down, 0.1f,ground_Layer);
        if (isGrounded)
        {
            if (jumped)
            {
                player_anim.SetBool("Jump", false);
                jumped = false;
            }
        }
    }
} //class
