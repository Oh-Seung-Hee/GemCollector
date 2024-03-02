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
        statsText.text = $"���ݷ�     : {player.PlayerStats.AttackDamage}\n���ݼӵ�  : {player.PlayerStats.AttackSpeed}\n" +
            $"�̵��ӵ�  : {player.PlayerStats.MoveSpeed}\n������     : {player.PlayerStats.JumpForce}";
    }
}
