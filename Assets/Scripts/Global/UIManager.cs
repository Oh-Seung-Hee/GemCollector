using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;
    private void Awake()
    {
        //todo : main아닌 다른 씬에도 필요할지?
        DontDestroyOnLoad(gameObject);

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    private List<UIPopup> popups = new List<UIPopup>();
    private bool isMenu = false;
    private bool isInventory = false;
    private bool isInformation = false;
    public UIPopup ShowPopup(string popupname)
    {
        var obj = Resources.Load("Popups/" + popupname, typeof(GameObject)) as GameObject;
        if (!obj)
        {
            //Debug.LogWarning("Failed to ShowPopup({0})".SFormat(popupname));
            return null;
        }
        return ShowPopupWithPrefab(obj, popupname);
    }

    public UIPopup ShowPopup(string popupname, Transform transform)
    {
        var obj = Resources.Load("Popups/" + popupname, typeof(GameObject)) as GameObject;
        if (!obj)
        {
            //Debug.LogWarning("Failed to ShowPopup({0})".SFormat(popupname));
            return null;
        }
        return ShowPopupWithPrefab(obj, popupname, transform);
    }

    public T ShowPopup<T>() where T : UIPopup
    {
        return ShowPopup(typeof(T).Name) as T;
    }

    public UIPopup ShowPopupWithPrefab(GameObject prefab, string popupName)
    {
        var obj = Instantiate(prefab);
        return ShowPopup(obj, popupName);
    }
    public UIPopup ShowPopupWithPrefab(GameObject prefab, string popupName, Transform transform)
    {
        var obj = Instantiate(prefab, transform);
        return ShowPopup(obj, popupName);
    }

    public UIPopup ShowPopup(GameObject obj, string popupname)
    {
        var popup = obj.GetComponent<UIPopup>();
        popups.Insert(0, popup);
        
        obj.SetActive(true);
        return popup;
    }
    public void ShowMenuPopup()
    {
        if (!isMenu)
        {
            isMenu = true;
            Debug.Log("Menu를 엽니다.");
            ShowPopup("Menu");
        }
        else
        {
            Debug.Log("이미 Menu가 열려 있습니다.");
            isMenu = false;
        }
    }

    public void ShowEventTextPopup()
    {
        ShowPopup("EventTextPopup");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.uiSelectClip);
            ShowMenuPopup();
        }
    }

    public void StartButtonOnClick()
    {
        ShowPopup("TypingText");
    }
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
