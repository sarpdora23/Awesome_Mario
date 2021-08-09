using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Rigidbody2D my_Body;
    private Animator anim;
    private bool jumpLeft = true;
    private bool animation_Started;
    private bool animation_Finished;
    private float jump_Counter = 0;
    private GameObject player;
    public LayerMask player_Layer;
    // Start is called before the first frame update
    private void Awake()
    {
        my_Body = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(FrogJump());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (animation_Started && animation_Finished)
        {
            animation_Started = false;
            gameObject.transform.parent.position = gameObject.transform.position;
            gameObject.transform.localPosition = Vector3.zero;
        }
    }
    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1, 4));
        animation_Started = true;
        animation_Finished = false;
        if (jumpLeft)
        {
            anim.Play(MyTags.FROG_JUMP_LEFT_ANIM);
        }
        else
        {
            anim.Play(MyTags.FROG_JUMP_RIGHT_ANIM);
        }
        StartCoroutine(FrogJump());
    }
    void FinishAnim()
    {
        animation_Finished = true;
        jump_Counter++;
        if (jump_Counter == 2)
        {
            jump_Counter = 0;
            jumpLeft = !jumpLeft;
        }
        if (jumpLeft)
        {
            anim.Play(MyTags.FROG_IDLE_LEFT_ANIM);
        }
        else
        {
            anim.Play(MyTags.FROG_IDLE_RIGHT_ANIM);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == MyTags.PLAYER_TAG)
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
