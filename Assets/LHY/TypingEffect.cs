using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    public string comment;
    public Transform cameraPosition;
    private float delay = 0.375f;
    
    private void Start()
    {
        GetComponent<TextMeshPro>();
        StartCoroutine(textPrint(delay, comment));
    }
    IEnumerator textPrint(float delay, string text)
    {
        int count = 0;
        while (count != text.Length)
        {
            if (count < text.Length)
            {
                gameObject.GetComponent<TextMeshPro>().text += text[count].ToString();
                //targetPrefeb.text += text[count].ToString();
                count++;
            }

            yield return new WaitForSeconds(delay);
        }
    }
}
