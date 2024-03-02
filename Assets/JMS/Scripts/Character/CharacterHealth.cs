using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health;
    public event Action OnDie;
    private Player Player;
    private Enemy Enemy;

    public bool IsDead => health == 0;


    private void Awake()
    {
        Player = GetComponent<Player>();
        Enemy = GetComponent<Enemy>();
    }
    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (health == 0) return;

        health = Mathf.Max(health - damage, 0);
        if (health > maxHealth)
            health = maxHealth;

        if (health == 0)
            OnDie?.Invoke();
        else
        {
            if (Player != null)
                Player.stateMachine.ChangeState(Player.stateMachine.HitState);
            else if (Enemy != null)
                Enemy.stateMachine.ChangeState(Enemy.stateMachine.HitState);
        }

        Debug.Log(health);
    }
}