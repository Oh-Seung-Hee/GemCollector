using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    Player player;
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text heathText;

    private void OnEnable()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        SetHealthUI();
    }

    public void SetHealthUI()
    {
        if(player != null)
        {
            healthBar.transform.localScale = new Vector3(player.CharacterHealth.health / player.CharacterHealth.maxHealth, 1, 1);
            heathText.text = $"{player.CharacterHealth.health} / {player.CharacterHealth.maxHealth}";
        }
    }
}
