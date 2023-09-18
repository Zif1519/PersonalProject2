using UnityEngine;

public class Weapon_Shooting : MonoBehaviour, IMouseClickHandler, IMouseMoveHandler
{
    private IController _controller;
    private CharacterStatsHandler _statsHandler ;
    private ProjectileManager _projectileManager ;
    [SerializeField] private Transform _projectileSpawnPosition;

    private float _timeSinceLastOperation = 0f;
    private bool _isReady = true;
    private Vector2 _aimDirection;

    private void Awake()
    {
        
        _statsHandler = GetComponent<CharacterStatsHandler>();
        _controller = GetComponent<IController>();
    }
    private void Start()
    {
        // 싱글턴의 경우는 스타트에서 연결시켜주어야 함
        _projectileManager = ProjectileManager.Instance;
        _controller.OnMouseClickHandler += Weapon_Shoot;
        _controller.OnMouseMoveHandler += Weapon_Aim;
    }

    private void Update()
    {
        if (!_isReady)
        {
            SetReadyAfterDelay();
        }
    }

    public void Weapon_Shoot(AttackSO attackSO)
    {
        if (!_isReady) return;
        _isReady = false;
        _timeSinceLastOperation = 0f;
        RangedAttackData rangedAttackData = attackSO as RangedAttackData;
        float projectilesAngleSpace = rangedAttackData._MultipleProjectilesAngle;
        int numberOfProjectilesPerShot = rangedAttackData._NumberOfProjectilesPerShot;

        float minAngle = -(numberOfProjectilesPerShot -1) * projectilesAngleSpace * 0.5f;
        for(int i = 0; i< numberOfProjectilesPerShot; i++) 
        {
            float angle = minAngle + i* projectilesAngleSpace;
            float rangdomSpread = Random.Range(-rangedAttackData._Speed, rangedAttackData._Speed);
            angle += rangdomSpread;
            CreateProjectile(rangedAttackData, angle);
        }
    }
    private void CreateProjectile(RangedAttackData rangedAttackData, float angle)
    {
        _projectileManager.ShootBullet(
            _projectileSpawnPosition.position,
            RotateVector2(_aimDirection,angle),
            rangedAttackData
            ) ;
    }

    private static Vector2 RotateVector2( Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
    private void SetReadyAfterDelay()
    {
        _timeSinceLastOperation += Time.deltaTime;
        if (_timeSinceLastOperation > _statsHandler._CurrentStats._AttackSO._Delay)
        {
            _isReady = true;
        }
    }

    public void Weapon_Aim(Vector2 direction)
    {
        _aimDirection = direction;
    }
}
