using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    float xPos;
    float zPos;
    public void CreateCharacter(GameObject go)
    {
        if (go.name != "Player")
        {
            Instantiate(go);
            xPos = Random.Range(-10f, 10f);
            zPos = Random.Range(-10f, 10f);
            go.transform.position = new Vector3(xPos, 0, zPos);
        }
        else
        {
            if (GameObject.FindWithTag("Player") == null)
                Instantiate(go);
        }
    }
}
