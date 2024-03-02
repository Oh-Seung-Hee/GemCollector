using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float MaxHealth { get => GetMaxHealth(); set => SetMaxHealth(value); }
    public float AttackDamage { get; set; }
    public float AttackSpeed { get => GetAttackSpeed(); set => SetAttackSpeed(value); }
    public float MoveSpeed { get; set; }
    public float JumpForce { get; set; }

    void SetMaxHealth(float value)
    {
        GetComponent<CharacterHealth>().maxHealth = value;
    }
    float GetMaxHealth()
    {
        return GetComponent<CharacterHealth>().maxHealth;
    }

    void SetAttackSpeed(float value)
    {
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetFloat("AttackSpeed", value);
    }
    float GetAttackSpeed()
    {
        return gameObject.transform.GetChild(0).GetComponent<Animator>().GetFloat("AttackSpeed");
    }
}
