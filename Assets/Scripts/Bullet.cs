using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = Mathsfx.Quaternion;
using Vector3 = Mathsfx.Vector3;

public class Bullet : MonoBehaviour
{
    private Gun owner;
    
    private float speed;
    private float lifetime;

    private float time;

    public void Setup(Vector3 position, Quaternion rotation, float speed, float lifetime)
    {
        transform.position = position.ToVector3();
        transform.rotation = rotation.ToQuaternion();
        this.speed = speed;
        this.lifetime = lifetime;
    }

    private void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);

        time += Time.deltaTime;

        if (time >= lifetime)
            Destroy(gameObject);
    }
}
