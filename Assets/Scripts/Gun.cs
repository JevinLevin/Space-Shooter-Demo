using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = Mathsfx.Vector3;

public class Gun : MonoBehaviour
{
    [SerializeField] private TransformX transformx;
    [SerializeField] private GameObject bullet;

    [Header("Bullets")] 
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Bullet newBullet = Instantiate(bullet).GetComponent<Bullet>(); 
        newBullet.Setup(transformx.position,transformx.Rotation,speed,lifetime);        
        
    }

}
