using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform _Player {  get; private set; }
    [SerializeField] private string playerTag = "Player";

    private void Awake()
    {
        Instance = this;
        _Player = GameObject.FindGameObjectWithTag(playerTag).transform;
    }
}
