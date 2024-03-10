using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    public bool AwareofPlayer { get; private set; }

    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField] private float _PlayerAwarenessDistance;

    private Transform _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemytoPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemytoPlayerVector.normalized;

        if (enemytoPlayerVector.magnitude <= _PlayerAwarenessDistance)
        {
            AwareofPlayer = true;
        }
        else
        {
            AwareofPlayer = false;
        }

    }
}
