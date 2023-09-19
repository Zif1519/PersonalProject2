using UnityEngine;

public class Weapon_Aiming : MonoBehaviour, IMouseMoveHandler
{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;

    [SerializeField] private SpriteRenderer characterRenderer;

    private IController _controller;

    private void Awake()
    {
        _controller = GetComponent<IController>();
    }

    private void Start()
    {
        _controller.OnLookEvent += Weapon_Aim;
    }



    public void Weapon_Aim(Vector2 newAimDirection)
    {
        RotateArm(newAimDirection);
    }

    private void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        armRenderer.flipY = Mathf.Abs(rotZ) > 90f;
        characterRenderer.flipX = armRenderer.flipY;
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);

    }
}
