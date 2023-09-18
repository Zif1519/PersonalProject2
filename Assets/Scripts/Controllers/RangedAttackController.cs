using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackController : MonoBehaviour
{
    [SerializeField] private LayerMask _levelCollisionLayer;

    private RangedAttackData _attackData;
    private float _currentDuration;
    private Vector2 _direction;
    private bool _isReady;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trailRenderer;
    public ProjectileManager _ProjectileManager;

    public bool fxOnDestroy = true;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rb = GetComponentInChildren<Rigidbody2D>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
    {
        if ( ! _isReady )
        {
            return;
        }
        _currentDuration += Time.deltaTime;

        if (_currentDuration > _attackData._Duration )
        {
            DestroyProjectile(transform.position, false);
        }
        _rb.velocity = _direction * _attackData._Speed;
    }

    public void InitializeAttack(Vector2 direction, RangedAttackData attackData, ProjectileManager projectileManager)
    {
        _ProjectileManager = projectileManager;
        _direction = direction;
        _attackData = attackData;

        _trailRenderer.Clear();
        _currentDuration = 0;
        _spriteRenderer.color = attackData._ProjectileColor;

        transform.right = _direction;

        _isReady = true;
    }

    public void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * _attackData._Size;
    }

    private void DestroyProjectile(Vector2 position, bool createFx)
    {
        if (createFx)
        {
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_levelCollisionLayer.value == (_levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * 0.2f, fxOnDestroy);
        }
    }
}
