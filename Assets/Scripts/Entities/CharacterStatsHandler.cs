using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats _baseStats;
    public CharacterStats _CurrentStats { get; private set; }
    public List<CharacterStats> _StatsModifiers = new List<CharacterStats>();

    private void Awake()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        AttackSO attackSO = null;
        if (_baseStats._AttackSO != null)
        {
            attackSO = Instantiate(_baseStats._AttackSO);
        }
        _CurrentStats = new CharacterStats { _AttackSO = attackSO };
        _CurrentStats._StatsChangeType = _baseStats._StatsChangeType;
        _CurrentStats._Speed = _baseStats._Speed;
        _CurrentStats._MaxHealth = _baseStats._MaxHealth;
    }
}
