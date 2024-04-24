using System;
using System.Collections;
using System.Collections.Generic;
using Mathsfx;
using UnityEngine;
using Quaternion = Mathsfx.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = Mathsfx.Vector3;

public class Asteroid : MonoBehaviour
{

    private Transform player;
    MeshFilter mesh;

    private Vector3 direction;
    private float duration;

    [SerializeField] private float offsetMax;
    [SerializeField] private Vector2 speedRange;
    [SerializeField] private float spinMultiplier;
    [SerializeField] private float lifetime = 10;
    [SerializeField] private ParticleSystem destroyParticles;
    private float speed;

    private Quaternion torqueDir;
    private Vector3[] modelVertices;

    private void Awake()
    {
        player = Player.Instance.transform;

        speed = Random.Range(speedRange.x, speedRange.y);
        
        mesh = GetComponentInChildren<MeshFilter>();
        modelVertices = Vector3.ToFx(mesh.mesh.vertices);
        torqueDir = RandomQuat();

    }

    public void Spawn()
    {

        direction = new Vector3(player.position - transform.position).Normalized;

        float radOffset = Random.Range(-offsetMax, offsetMax) * MathsfxConst.Deg2Rad;

        direction = Vector3.AngleAxis(radOffset, Vector3.Up, direction);
        
    }

    private void Update()
    {
        transform.position += direction.ToVector3() * (speed * Time.deltaTime);

        duration += Time.deltaTime;
        
        if(duration >= lifetime)
            Destroy(gameObject);
        
        
        
        // Spin
        Vector3[] worldVertices = Vector3.ToFx(mesh.mesh.vertices);
        Vector3[] result = new Vector3[worldVertices.Length];
        
        for (int i = 0; i < worldVertices.Length; i++)
        {
            Quaternion target = new Quaternion(worldVertices[i]);
            result[i] = (torqueDir * target * torqueDir.Inverse()).GetAxis();
        }
        
        mesh.mesh.vertices = Vector3.ToDefault(result);
        
        mesh.mesh.RecalculateNormals();
        mesh.mesh.RecalculateBounds();
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet")) return;
        
        Score.Instance.PlayerScore++;
        destroyParticles.transform.parent = null;
        destroyParticles.Play();
        Destroy(gameObject);
    }

    private Quaternion RandomQuat()
    {
        Vector3 axis = new Vector3(Random.value, Random.value, Random.value);
        float spinSpeed = Random.value * spinMultiplier;
        return new Quaternion(spinSpeed, axis);
    }
}
