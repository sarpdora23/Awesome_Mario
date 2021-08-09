using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D my_Body;
    private Vector3 origin_Position;
    private Vector3 move_Position;
    private Vector3 move_Direction;
    [SerializeField]
    private float speed = 2.45f;
    private bool canMove;
    public GameObject egg;
    public LayerMask player_Layer;
    private bool attacked = false;
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        my_Body = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        move_Direction = Vector3.left;
        origin_Position = transform.position;
        origin_Position.x += 6f;
        move_Position = transform.position;
        move_Position.x -= 6f;
        canMove = true;
    }

    
    void Update()
    {
        MoveBird();
        DropEgg();
    }
    void MoveBird()
    {
        if (canMove)
        {
            transform.Translate(move_Direction * speed * Time.smoothDeltaTime);
            if (transform.position.x <= move_Position.x)
            {
                move_Direction = Vector3.right;
                ChangeDirection(-0.6f);
            }
            else if (transform.position.x >= move_Direction.x)
            {
                move_Direction = Vector3.left;
                ChangeDirection(0.6f);
            }
        }
    }
    void ChangeDirection(float direction)
    {
        Vector3 temp = transform.localScale;
        temp.x = direction;
        transform.localScale = temp;
    }
    void DropEgg()
    {
        if (!attacked)
        {
            if (Physics2D.Raycast(transform.position,Vector2.down,Mathf.Infinity,player_Layer))
            {
                Instantiate(egg, new Vector3(transform.position.x,transform.position.y -1,transform.position.z),Quaternion.identity);
                attacked = true;
                anim.Play(MyTags.BIRD_FLY_ANIM);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == MyTags.BULLET_TAG)
        {
            canMove = false;
            my_Body.bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            anim.Play(MyTags.BIRD_DEAD_ANIM);
            StartCoroutine(BirdDead());
        }
    }
    IEnumerator BirdDead()
    {
        yield return new WaitForSeconds(2.4f);
        gameObject.SetActive(false);
    }
}
