using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionControls : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    FullScreenMode screenMode;
    public Toggle fullScreenToggle;

    List<Resolution> resolutions = new List<Resolution>();
    public int resolutionNum = 0;
    public int myRefreshRate = 0;
    private void Start()
    {
        initUI();
    }
    void initUI()
    {
        resolutionDropdown.options.Clear();
        
        //�ػ� ��� ��������
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 165)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }

        int optionNum = 0;
        foreach (Resolution screen in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = screen.width + " x " + screen.height + " " + screen.refreshRateRatio + "hz";
            resolutionDropdown.options.Add(option);

            //�ػ� ����� �ҷ��ö� ������ �´� �ػ󵵴� ����ٿ����� ����
            if (screen.width == Screen.width && screen.height == Screen.height)
                resolutionDropdown.value = optionNum;
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();

        fullScreenToggle.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }


    
    public void OptionApply()
    {
        Screen.SetResolution(resolutions[resolutionNum].width,
            resolutions[resolutionNum].height,
            screenMode);
        Debug.Log(resolutions[resolutionNum].width + " x " + resolutions[resolutionNum].height + "�ɼ��� �����մϴ�.");
    }
}
