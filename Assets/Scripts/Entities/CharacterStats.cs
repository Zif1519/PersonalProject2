using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATSCHANGETYPE { ADD, MULTIPLE, OVERRIDE }

[Serializable]
public class CharacterStats
{
    public STATSCHANGETYPE _StatsChangeType;
    [Range(1, 100)] public int _MaxHealth;
    [Range(1f, 20f)] public float _Speed;

    // ���� ������
    public AttackSO _AttackSO;

    public int MaxHealth { get => _MaxHealth; set => _MaxHealth = value; }
}
