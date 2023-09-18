using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackData", menuName = "TopDownController/Attacks/Ranged", order =1)]
public class RangedAttackData : AttackSO
{
    [Header("Ranged Attack Data")]
    public string _BulletNameTag;
    public float _Duration;
    public float _Spread;
    public int _NumberOfProjectilesPerShot;
    public float _MultipleProjectilesAngle;
    public Color _ProjectileColor;
}
