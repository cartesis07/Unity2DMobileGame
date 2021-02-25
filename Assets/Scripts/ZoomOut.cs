using UnityEngine;
using System.Collections;

public class ZoomOut : MonoBehaviour
{
    public Camera cam;
    public GameObject player;

    Vector3 lastPosition = Vector3.zero;

    public float player_position_y;


    public void Update()
    {
        if (player.transform.position.y >= -3f && player.transform.position.y <= 40f)
        {
            player_position_y = (player.transform.position.y - lastPosition.y);
            lastPosition = player.transform.position;

            cam.orthographicSize += player_position_y / 3.0f;
        }
    }
}