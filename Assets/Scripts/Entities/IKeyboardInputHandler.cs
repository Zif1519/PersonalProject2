using System;
using UnityEngine;

internal interface IKeyboardInputHandler
{
    // direction 캐릭터의 움직임 구현을 위한 벡터값으로, 방향키 입력의 normalized 된 값
    void Character_Move(Vector2 direction);
}
