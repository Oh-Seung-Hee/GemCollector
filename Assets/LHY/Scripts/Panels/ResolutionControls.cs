using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionControls : MonoBehaviour
{
    public Dropdown resolutionDropdown;

    List<Resolution> resolutions = new List<Resolution>();

    private void Start()
    {
        initUI();
    }
    void initUI()
    {
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        foreach (Resolution screen in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = screen.width + " x " + screen.height + " " + screen.refreshRateRatio + "hz";
            resolutionDropdown.options.Add(option);

        }
        resolutionDropdown.RefreshShownValue();
    }
}
