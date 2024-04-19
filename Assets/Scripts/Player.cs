using System;
using System.Collections;
using System.Collections.Generic;
using Mathsfx;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = Mathsfx.Vector3;
using Quaternion = Mathsfx.Quaternion;


public class Player : MonoBehaviour
{
    public static Player Instance;
    
    [SerializeField] private TransformX transformx;

    public Vector3 position => transformx.position;

    [SerializeField] private float panSpeed = 3;
    [SerializeField] private float rotSpeed = 2500;
    [SerializeField] private float scaleSpeed = 3;

    [SerializeField] private Transform cameraPivot;

    private Quaternion targetRotation;
    private Vector3 lastMousePosition = Vector3.Zero;

    private void Awake()
    {
        Instance = this;
        targetRotation = transformx.Rotation;

        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector3 panInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        // Rotate pan input by the current rotation
        panInput = new Vector3((Matrix4by4.RotationMatrix(transformx.Rotation) * panInput));
        
        transformx.position += panInput * panSpeed * Time.deltaTime;
        cameraPivot.position += (panInput * panSpeed * Time.deltaTime).ToVector3();

        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);


        if (Input.GetMouseButton(1) && mouseDelta.Magnitude > 0)
        {
            Vector3 spinDirection = Vector3.Cross(Vector3.Forward, mouseDelta);
            Quaternion quatDirection = new Quaternion( rotSpeed * Time.deltaTime, spinDirection);
            transformx.Rotation *= quatDirection;
            cameraPivot.rotation *= quatDirection.ToQuaternion();

        }
        
        //transformx.rotation = Vector3.Lerp(transformx.rotation, targetRotation,  easeOutCubic(0.1f));
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
