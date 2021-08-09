using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private float player_Temp_Position_x;
    private float camera_player_distance;
    [SerializeField]
    private float camera_Move_Speed;
    [SerializeField]
    private float max_speed;
    private Rigidbody2D camera_body;
    private void Awake()
    {
        camera_body = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        camera_player_distance = gameObject.transform.position.x - player.transform.position.x;
        player_Temp_Position_x = player.gameObject.transform.position.x;
    }
    void Update()
    {
        MoveCamera();
        if (camera_body.velocity.x <= 0)
        {
            ResetSpeed();
        }
    }
    private void LateUpdate()
    {
        ClampSpeed();
    }
    void MoveCamera()
    {
        if (gameObject.transform.position.x - player.transform.position.x != camera_player_distance)
        {
            if (player.transform.position.x > player_Temp_Position_x)
            {
                camera_body.velocity = Vector2.right * camera_Move_Speed;
                if (camera_Move_Speed < player.GetComponent<Rigidbody2D>().velocity.x)
                {
                    camera_Move_Speed += 0.2f;
                }
            }
        }
        player_Temp_Position_x = player.transform.position.x;
    }
    void ClampSpeed()
    {
        Vector2 temp_Speed = camera_body.velocity;
        temp_Speed.x = Mathf.Clamp(temp_Speed.x, 0f, player.GetComponent<Rigidbody2D>().velocity.x);
        camera_body.velocity = temp_Speed;
    }
    void ResetSpeed()
    {
        Vector2 temp_Speed = camera_body.velocity;
        temp_Speed.x = 0;
        camera_body.velocity = temp_Speed;
    }
}
