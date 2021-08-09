using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    private Rigidbody2D my_Body;
    private Animator anim;
    private Vector3 move_Direction;
    private bool canMove;
    private void Awake()
    {
        my_Body = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        move_Direction = Vector3.down;
        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (canMove)
        {
            transform.Translate(move_Direction * Time.smoothDeltaTime);
        }
    }
    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(Random.Range(2, 5));
        if (move_Direction == Vector3.down)
        {
            move_Direction = Vector3.up;
        }
        else
        {
            move_Direction = Vector3.down;
        }
        StartCoroutine(ChangeDirection());
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(1.83f);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == MyTags.BULLET_TAG)
        {
            StopCoroutine(ChangeDirection());
            anim.Play(MyTags.SPIDER_DEAD_ANIM);
            canMove = false;
            my_Body.bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            StartCoroutine(Die());
        }
        if (collision.gameObject.tag == MyTags.PLAYER_TAG)
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
