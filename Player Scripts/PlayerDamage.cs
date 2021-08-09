using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    private bool canDamage;
    private Text life_Text;
    [SerializeField]
    private float life_Counter;
    // Start is called before the first frame update
    void Start()
    {
        canDamage = true;
        life_Text = GameObject.Find("Health_Text").GetComponent<Text>();
        life_Text.text = "x" + life_Counter;
        Time.timeScale = 1f;
    }
    public void DealDamage()
    {
        if (canDamage)
        {
            life_Counter--;
            canDamage = false;
            if (life_Counter > 0)
            {
                life_Text.text = "x" + life_Counter;
            }
            else if (life_Counter == 0)
            {
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }
            StartCoroutine(WaitCanDamage());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator WaitCanDamage()
    {
        yield return new WaitForSecondsRealtime(2f);
        canDamage = true;
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("SampleScene");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == MyTags.WATER_TAG)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
