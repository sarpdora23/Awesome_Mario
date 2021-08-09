using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    [SerializeField]
    private Transform instiante_Transform;
    private Animator anim;
    [SerializeField]
    private GameObject boss_stone;
    public bool canAttack;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    private void Start()
    {
        canAttack = true;
        StartCoroutine(StartAttack());
    }
    void Attack()
    {
        GameObject stone = GameObject.Instantiate(boss_stone, instiante_Transform.position, Quaternion.identity);
        stone.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1600f,-300f),0));
    }
    void BackToIdle()
    {
        anim.Play("Boss_Idle");
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        if (canAttack)
        {
            anim.Play("Boss_Attack");
            StartCoroutine(StartAttack());
        }
    }
    void StopAttack()
    {
        StopCoroutine(StartAttack());
        StartCoroutine(Deactivate());
    }
    IEnumerator Deactivate()
    {
        yield return new WaitForSecondsRealtime(2f);
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
