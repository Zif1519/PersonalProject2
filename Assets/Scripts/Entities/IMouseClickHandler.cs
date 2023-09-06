using System;
using UnityEngine;

internal interface IMouseClickHandler
{
    // direction 캐릭터의 움직임 구현을 위한 벡터값으로, 방향키 입력의 normalized 된 값
    void Weapon_Shoot();
}
