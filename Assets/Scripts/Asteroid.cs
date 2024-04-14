using System;
using System.Collections;
using System.Collections.Generic;
using Mathsfx;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = Mathsfx.Vector3;

public class Asteroid : MonoBehaviour
{

    private Transform player;

    private Vector3 direction;

    [SerializeField] private float offsetMax;
    [SerializeField] private Vector2 speedRange;
    private float speed;

    private void Awake()
    {
        player = Player.Instance.transform;

        speed = Random.Range(speedRange.x, speedRange.y);

    }

    public void Spawn()
    {

        direction = new Vector3(player.position - transform.position);

        float radOffset = Random.Range(-offsetMax, offsetMax) * MathsfxConst.Deg2Rad;
        Vector3 offset = Vector3.RadToVec(radOffset);

        direction += offset;

        direction = direction.Normalized;

    }

    private void Update()
    {
        transform.position += direction.ToVector3() * (speed * Time.deltaTime);
    }
}
