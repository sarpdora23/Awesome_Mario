using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private bool canDamage;
    [SerializeField]
    private float life_Counter;
    private Animator anim;
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DealDamage()
    {
        if (canDamage)
        {
            life_Counter--;
            canDamage = false;
            if (life_Counter > 0)
            {
                StartCoroutine(DamageReload());
            }
            else
            {
                anim.Play("Boss_Dead");
                gameObject.GetComponent<BossScript>().canAttack = false;
            }
        }
    }
    IEnumerator DamageReload()
    {
        yield return new WaitForSecondsRealtime(2f);
        canDamage = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == MyTags.BULLET_TAG)
        {
            DealDamage();
        }
    }
}
