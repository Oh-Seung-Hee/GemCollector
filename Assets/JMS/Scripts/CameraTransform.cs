using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    private Transform Player;
    private void Update()
    {
        if (Player == null)
        {
            if (GameObject.FindWithTag("Player") != null)
                Player = GameObject.FindWithTag("Player").transform;
        }
        if(Player != null)
            transform.position = new Vector3(Player.position.x, Player.position.y + 7f, Player.position.z - 7f);
    }
}
