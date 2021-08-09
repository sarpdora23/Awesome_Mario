using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    private float speed = 11f;
    private Animator anim;
    private bool canMove;
    void Start()
    {
        canMove = true;
        StartCoroutine(Died(1.4f));
    }

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == MyTags.SNAIL_TAG || collision.gameObject.tag == MyTags.BEETLES_TAG || collision.gameObject.tag == MyTags.SPIDER_TAG || collision.gameObject.tag == MyTags.BOSS_TAG)
        {
            canMove = false;
            anim.Play(MyTags.BULLET_EXPLODE_ANIM);
            StartCoroutine(Died(0.23f));
        }
    }
    IEnumerator Died(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
}
