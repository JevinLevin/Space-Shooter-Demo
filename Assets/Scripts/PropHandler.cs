using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = Mathsfx.Vector3;
using Quaternion = Mathsfx.Quaternion;


public class PropHandler : MonoBehaviour
{
    private TransformX transformx;

    [SerializeField] private float panSpeed;
    [SerializeField] private float rotSpeed;


    private void Awake()
    {
        transformx = GetComponent<TransformX>();
    }


    private void Update()
    {
        Vector3 panInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        transformx.position += panInput * panSpeed * Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            Quaternion down = new Quaternion(-Vector3.Up);
            Quaternion right = new Quaternion(Vector3.Right);
            
            Quaternion horizontal = new Quaternion(Input.GetAxis("Mouse X"), Vector3.Up);
            Quaternion vertical = new Quaternion(Input.GetAxis("Mouse Y"), Vector3.Right);

            Quaternion result = down * horizontal * down.Inverse() * right * vertical * right.Inverse();
            
            
            Vector3 rotInput = new Vector3(result.x,result.y,result.z);
            
            print(rotInput);
            transformx.rotation += rotInput * rotSpeed * Time.deltaTime;
        }

    }
}
