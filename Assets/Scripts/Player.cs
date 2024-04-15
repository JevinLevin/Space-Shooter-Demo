using System;
using System.Collections;
using System.Collections.Generic;
using Mathsfx;
using UnityEngine;
using Vector3 = Mathsfx.Vector3;
using Quaternion = Mathsfx.Quaternion;


public class Player : MonoBehaviour
{
    public static Player Instance;
    
    [SerializeField] private TransformX transformx;

    [SerializeField] private float panSpeed = 3;
    [SerializeField] private float rotSpeed = 2500;
    [SerializeField] private float scaleSpeed = 3;

    [SerializeField] private Transform cameraPivot;

    private Vector3 targetRotation;
    private Vector3 lastMousePosition = Vector3.Zero;

    private void Awake()
    {
        Instance = this;
        targetRotation = transformx.rotation;
    }


    private void Update()
    {
        Vector3 panInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        // Rotate pan input by the current rotation
        panInput = new Vector3((Matrix4by4.RotationMatrix(transformx.Radians) * panInput));
        
        transformx.position += panInput * panSpeed * Time.deltaTime;
        cameraPivot.position += (panInput * panSpeed * Time.deltaTime).ToVector3();

        // Calculate mouse delta
        Vector3 mousePosition = new Vector3(Input.mousePosition);
        Vector3 mouseDelta = mousePosition - lastMousePosition;
        lastMousePosition = mousePosition;

        if (Input.GetMouseButton(1))
        {


            Vector3 spinDirection = Vector3.Cross(Vector3.Forward, mouseDelta);

            Quaternion result = new Quaternion(mouseDelta.Magnitude, spinDirection);

            // Creates angle axis quaternion based on mouse input
            //Quaternion horizontal = new Quaternion(Input.GetAxis("Mouse X"), -Vector3.Up);
            //Quaternion vertical = new Quaternion(Input.GetAxis("Mouse Y"), Vector3.Right);

            // Create global down quaternion
            //Quaternion down = new Quaternion(-Vector3.Up);
            // Create global right quaternion
            //Quaternion right = new Quaternion(Vector3.Right);

            // Combine inputs
            //Quaternion result = down * horizontal * down.Inverse() * right * vertical * right.Inverse();
            

            // Store result as euler angle
            Vector3 rotInput = new Vector3(result.x,result.y,result.z);
            Vector3 rotValue = rotInput * rotSpeed * Time.deltaTime;
            
            targetRotation += rotValue;


            // Camera needs to follow cube rotation
            cameraPivot.eulerAngles = new UnityEngine.Vector3(targetRotation.x, targetRotation.y, targetRotation.z);
        }
        
        transformx.rotation = Vector3.Lerp(transformx.rotation, targetRotation,  easeOutCubic(0.1f));
        //transformx.rotation = Quaternion.Slerp(transformx.QuatRotation, Quaternion.FromEuler(targetRotation), 0.25f).ToEuler();

        
        // Scale object based on scroll wheel input
        float scaleInput = Input.GetAxis("Mouse ScrollWheel");
        transformx.scale += Vector3.One * scaleInput * scaleSpeed;
        

    }
    
    private float easeOutCubic(float t)
    {
        return 1 - Mathf.Pow(1 - t, 3);

    }
}
