using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsInfo : MonoBehaviour
{
    Player player;
    [SerializeField] private TMP_Text statsText;
    private void OnEnable()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        SetStatsInfoUI();
    }

    void SetStatsInfoUI()
    {
        statsText.text = $"공격력     : {player.PlayerStats.AttackDamage}\n공격속도  : {player.PlayerStats.AttackSpeed}\n" +
            $"이동속도  : {player.PlayerStats.MoveSpeed}\n점프력     : {player.PlayerStats.JumpForce}";
    }
}
