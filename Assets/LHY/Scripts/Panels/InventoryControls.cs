using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControls : MonoBehaviour
{
    private bool isActive = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isActive)
            {
                isActive = false;
                Able();
            }
            else
            {
                isActive = true;
                Disable();
            }
        }
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void Able()
    {
        gameObject.SetActive(true);
    }
}
