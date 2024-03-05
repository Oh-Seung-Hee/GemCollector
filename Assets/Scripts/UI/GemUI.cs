using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quantity_R;
    [SerializeField] private TextMeshProUGUI quantity_O;
    [SerializeField] private TextMeshProUGUI quantity_Y;
    [SerializeField] private TextMeshProUGUI quantity_G;
    [SerializeField] private TextMeshProUGUI quantity_B;
    
    public static GemUI instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateGemUI(GemColorType _type, int _quantity)
    {
        switch (_type)
        {
            case GemColorType.Red:
                quantity_R.text = _quantity.ToString(); break;
            case GemColorType.Orange:
                quantity_O.text = _quantity.ToString(); break;
            case GemColorType.Yellow:
                quantity_Y.text = _quantity.ToString(); break;
            case GemColorType.Green:
                quantity_G.text = _quantity.ToString(); break;
            case GemColorType.Blue:
                quantity_B.text = _quantity.ToString(); break;
        }
    }
}
