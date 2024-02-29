using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    float xPos;
    float zPos;
    public void CreateGameObject(GameObject go)
    {
        Instantiate(go);
        xPos = Random.Range(-10f, 10f);
        zPos = Random.Range(-10f, 10f);
        go.transform.position = new Vector3(xPos, 0, zPos);
    }
}
