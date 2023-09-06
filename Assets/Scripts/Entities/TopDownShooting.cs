using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private IController _controller;
    [SerializeField] private Transform projectileSpawnPosition;

    public GameObject testPrefab;

    private float _timeSinceLastOperation = 0f;
    private bool _isReady = true;

    private void Awake()
    {
        _controller = GetComponent<IController>();
    }
    private void Start()
    {
        _controller.OnMouseClickHandler += OnShoot;
    }

    private void Update()
    {
        if (!_isReady)
        {
            SetReadyAfterDelay();
        }
    }

    private void OnShoot()
    {
        if (_isReady)
        {
            CreateProjectile();
            _isReady = false;
            _timeSinceLastOperation = 0f;
        }
    }
    private void CreateProjectile()
    {
        Instantiate(testPrefab, projectileSpawnPosition.position, Quaternion.identity);
    }
    private void SetReadyAfterDelay()
    {
        _timeSinceLastOperation += Time.deltaTime;
        if (_timeSinceLastOperation > 0.2f)
        {
            _isReady = true;
        }
    }
}
