using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{
    private Animator anim;
    private bool canAnimate = true;
    private bool startAnim = false;
    private Vector3 origin_Position;
    private Vector3 anim_Position;
    public Transform bottom_Collision;
    public LayerMask player_Layermask;
    private Vector3 move_Direction = Vector3.up;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void Start()
    {
        origin_Position = gameObject.transform.position;
        anim_Position = gameObject.transform.position;
        anim_Position.y += 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForCollision();
        AnimateUpDown();
    }
    void CheckForCollision()
    {
        if (canAnimate)
        {
            RaycastHit2D hit = Physics2D.Raycast(bottom_Collision.position, Vector2.down, 0.1f, player_Layermask);
            if (hit)
            {
                Debug.Log("test");
                if (hit.transform.tag == MyTags.PLAYER_TAG)
                {
                    anim.Play("Idle");
                    canAnimate = false;
                    startAnim = true;
                }
            }
        }
    }
    void AnimateUpDown()
    {
        if (startAnim)
        {
            transform.Translate(move_Direction * Time.smoothDeltaTime);
            if (transform.position.y >= anim_Position.y)
            {
                move_Direction = Vector3.down;
            }
            else if (transform.position.y <= anim_Position.y)
            {
                startAnim = false;
            }
        }
    }
}
