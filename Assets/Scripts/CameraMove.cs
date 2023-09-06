using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera _Camera;
    public GameObject _player;

    private void Start()
    {
        _Camera = GetComponent<Camera>();
        _player = GameObject.FindWithTag("Player");
    }

    private void LateUpdate()
    {
        _Camera.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -10);
    }
}
