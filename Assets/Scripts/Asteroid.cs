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
    private Rigidbody rb;

    private Vector3 direction;
    private float duration;

    [SerializeField] private float offsetMax;
    [SerializeField] private Vector2 speedRange;
    [SerializeField] private float lifetime = 10;
    [SerializeField] private ParticleSystem destroyParticles;
    private float speed;

    private void Awake()
    {
        player = Player.Instance.transform;

        speed = Random.Range(speedRange.x, speedRange.y);

        rb = GetComponent<Rigidbody>();

    }

    public void Spawn()
    {

        direction = new Vector3(player.position - transform.position).Normalized;

        float radOffset = Random.Range(-offsetMax, offsetMax) * MathsfxConst.Deg2Rad;

        direction = Vector3.AngleAxis(radOffset, Vector3.Up, direction);
        
        rb.AddTorque(Random.value, Random.value, Random.value, ForceMode.Impulse);

    }

    private void Update()
    {
        transform.position += direction.ToVector3() * (speed * Time.deltaTime);

        duration += Time.deltaTime;
        
        if(duration >= lifetime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet")) return;
        
        Score.Instance.PlayerScore++;
        destroyParticles.transform.parent = null;
        destroyParticles.Play();
        Destroy(gameObject);
    }
}
