using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBackground : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(1, 1, 1);
        float width = sr.bounds.size.x;
        float height = sr.bounds.size.y;
        float worldHeight = Camera.main.orthographicSize * 2;
        float worldWidth = worldHeight / Screen.height * Screen.width;
        Vector3 tempScale = gameObject.transform.localScale;
        tempScale.y = worldHeight / height + 0.1f;
        tempScale.x = worldWidth / width + 0.1f;
        transform.localScale = tempScale;
    }
}
