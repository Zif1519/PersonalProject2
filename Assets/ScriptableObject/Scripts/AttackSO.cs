using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackData", menuName = "TopDownController/Attacks/default", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float _Size;
    public float _Delay;
    public float _Power;
    public float _Speed;
    public LayerMask target;

    [Header("Knock Back Info")]
    public bool _IsOnKnockback;
    public float _KnockbackPower;
    public float _KnockbackTime;
}
