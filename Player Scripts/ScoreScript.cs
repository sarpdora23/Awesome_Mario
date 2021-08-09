using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private float coin_counter;
    private Text coin_Text;
    private AudioSource au_Manager;
    private void Awake()
    {
        au_Manager = gameObject.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        coin_Text = GameObject.Find("Coin_Text").GetComponent<Text>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.COIN_TAG)
        {
            collision.gameObject.SetActive(false);
            coin_counter++;
            coin_Text.text = "x" + coin_counter;
            au_Manager.Play();
        }
    }
}
