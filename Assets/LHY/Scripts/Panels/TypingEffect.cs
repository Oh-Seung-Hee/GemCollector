using DG.Tweening.Plugins;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class TypingEffect : MonoBehaviour
{
    public Text text;
    public string[] introComment;
    private string[] comment;
    public string sceneName;
    int commentNum = 0;

    bool startReady = false;

    private void Start()
    {
        StartComment(introComment);
    }

    IEnumerator Typing(string comment)
    {
        text.text = "";
        text.DOText(comment, 1f);

        yield return new WaitForSeconds(1.5f);
        
        NextTalk();
    }

    public void StartComment(string[] introComment)
    {
        comment = introComment;

        StartCoroutine(Typing(comment[commentNum]));
    }

    public void NextTalk()
    {
        commentNum++;

        //마지막 코멘트 출력시 중지
        if (commentNum == comment.Length)
        {
            EndTalk();
            return;
        }
    }
    public void EndTalk()
    {
        commentNum = 0;
        startReady = true;
    }
    public void NextButton()
    {
        if (startReady)
        {
            startReady = false;
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            StartCoroutine(Typing(comment[commentNum]));
        }
    }
}
