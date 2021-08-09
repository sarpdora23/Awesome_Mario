using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float move_Speed = 1f;
    public Transform snail_Bottom_position;
    public Transform snail_Top_Collision, snail_Right_Collision, snail_Left_Collision;
    [SerializeField]
    private Vector3 snail_Left_Position, snail_Right_Position;
    private bool canMove;
    private bool isStunned;
    private Rigidbody2D my_Body;
    private Animator anim;
    private bool isMoveLeft;
    public LayerMask player_Layer;

    private void Awake()
    {
        my_Body = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        snail_Left_Position = snail_Left_Collision.localPosition;
        snail_Right_Position = snail_Right_Collision.localPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        isMoveLeft = true;
        canMove = true;
        isStunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        SnailMove();
        GroundCheck();
        CheckCollision();
    }
    void SnailMove()
    {
        if (canMove)
        {
            if (isMoveLeft)
            {
                my_Body.velocity = new Vector2(-move_Speed, my_Body.velocity.y);
            }
            else
            {
                my_Body.velocity = new Vector2(move_Speed, my_Body.velocity.y);
            }
        }
    }
    void GroundCheck()
    {
        if (!Physics2D.Raycast(snail_Bottom_position.position,Vector2.down,0.1f))
        {
            ChangeDirection();
        }
    }
    void ChangeDirection()
    {
        isMoveLeft = !isMoveLeft;
        Vector3 tempScale = gameObject.transform.localScale;
        if (isMoveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            snail_Left_Collision.localPosition = snail_Left_Position;
            snail_Right_Collision.localPosition = snail_Right_Position;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
            snail_Right_Collision.localPosition = snail_Left_Position;
            snail_Left_Collision.localPosition = snail_Right_Position;
        }
        gameObject.transform.localScale = tempScale;
    }
    void CheckCollision()
    {
        RaycastHit2D left_Hit = Physics2D.Raycast(snail_Left_Collision.position,Vector2.left,0.1f,player_Layer);
        RaycastHit2D right_Hit = Physics2D.Raycast(snail_Right_Collision.position, Vector2.right, 0.1f,player_Layer);
        Collider2D top_Hit = Physics2D.OverlapCircle(snail_Top_Collision.position, 0.2f,player_Layer);
        if (top_Hit != null)
        {
            if (top_Hit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                canMove = false;
                my_Body.velocity = Vector2.zero;
                anim.Play(MyTags.ANIM_STUNNED);
                isStunned = true;
                if (tag != MyTags.BEETLES_TAG)
                {
                    top_Hit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(top_Hit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 8);
                    StartCoroutine(Died(3.5f));
                }
                else
                {
                    top_Hit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(top_Hit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 3);
                    StartCoroutine(Died(0.5f));
                }
            }
        }
        if (right_Hit)
        {
            if (right_Hit.transform.tag == MyTags.PLAYER_TAG)
            {
                if (tag != MyTags.BEETLES_TAG)
                {
                    if (isStunned)
                    {
                        my_Body.velocity = new Vector2(-15f, my_Body.velocity.y);
                    }
                    else
                    {
                        right_Hit.transform.gameObject.GetComponent<PlayerDamage>().DealDamage();
                        Debug.Log("Damage!");
                    }
                }
                else
                {
                    right_Hit.transform.gameObject.GetComponent<PlayerDamage>().DealDamage();
                    Debug.Log("Damage!");
                }
                
            }
        }
        if (left_Hit)
        {
            Debug.Log("Teess");
            if (left_Hit.transform.tag == MyTags.PLAYER_TAG)
            {
                if (tag != MyTags.BEETLES_TAG)
                {
                    if (isStunned)
                    {
                        my_Body.velocity = new Vector2(15f, my_Body.velocity.y);
                    }
                    else
                    {
                        left_Hit.transform.gameObject.GetComponent<PlayerDamage>().DealDamage();
                        Debug.Log("Damage");
                    }
                }
                else
                {
                    left_Hit.transform.gameObject.GetComponent<PlayerDamage>().DealDamage();
                    Debug.Log("Damage");
                }
            }
        }
    }
    IEnumerator Died(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == MyTags.BULLET_TAG)
        {
            if (gameObject.transform.tag == MyTags.BEETLES_TAG)
            {
                canMove = false;
                my_Body.velocity = Vector2.zero;
                isStunned = true;
                anim.Play(MyTags.ANIM_STUNNED);
                StartCoroutine(Died(0.5f));
            }
            else if (gameObject.transform.tag == MyTags.SNAIL_TAG)
            {
                if (!isStunned)
                {
                    isStunned = true;
                    canMove = false;
                    my_Body.velocity = Vector2.zero;
                    anim.Play(MyTags.ANIM_STUNNED);
                    StartCoroutine(Died(3.5f));
                }
                else
                {
                    StartCoroutine(Died(0.1f));
                }
            }
        }
    }
}
