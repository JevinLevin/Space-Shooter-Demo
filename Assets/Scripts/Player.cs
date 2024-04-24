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
    [SerializeField] private Vector2 scaleBounds = new Vector2 (0.1f, 10f);
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float startingScale = 0.5f;
    private float scaleValue;


    private bool flipping;
    [SerializeField] private float flipLength;
    private float flipTime;
    private Quaternion flipStart;
    private Quaternion flipEnd; 


    [SerializeField] private Transform cameraPivot;

    private Quaternion targetRotation;
    private Vector3 lastMousePosition = Vector3.Zero;

    private void Awake()
    {
        Instance = this;
        targetRotation = transformx.Rotation;

        scaleValue = startingScale;
        transformx.scale = Vector3.One * Mathf.Lerp(scaleBounds.x, scaleBounds.y, scaleCurve.Evaluate(scaleValue));


        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        
        #region Movement
        Vector3 panInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        // Rotate pan input by the current rotation
        panInput = new Vector3((Matrix4by4.RotationMatrix(transformx.Rotation) * panInput));
        
        transformx.position += panInput * panSpeed * Time.deltaTime;
        cameraPivot.position += (panInput * panSpeed * Time.deltaTime).ToVector3();
        #endregion
        
        #region Rotation
        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        Vector3 spinDirection = Vector3.Cross(Vector3.Forward, mouseDelta);
        Quaternion quatDirection = new Quaternion(rotSpeed * Time.deltaTime, spinDirection);

        if (mouseDelta.Magnitude > 0 && !flipping)
        {
            transformx.Rotation *= quatDirection;
        }
        #endregion

        #region  Scale
        // Scale object based on scroll wheel input
        float scaleInput = Input.GetAxis("Mouse ScrollWheel");
        if(scaleInput != 0) 
        {
            scaleValue = Mathf.Clamp(scaleValue + scaleInput, 0, 1);
            transformx.scale = Vector3.One * Mathf.Lerp(scaleBounds.x, scaleBounds.y, scaleCurve.Evaluate(scaleValue));
        }
        #endregion

        // Flip gun
        if(Input.GetKeyDown(KeyCode.R) && !flipping)
        { 
            flipping = true;
            flipTime = 0.0f;

            flipStart = transformx.Rotation;
            flipEnd = flipStart * new Quaternion(180 * MathsfxConst.Deg2Rad, Vector3.Right);

        }

        if(flipping)
        {

            flipTime += Time.deltaTime;

            transformx.Rotation = Quaternion.Slerp(flipStart, flipEnd, easeOutCubic(flipTime / flipLength));

            if (flipTime >= flipLength-(flipLength/5))
                flipping = false;
        }

        cameraPivot.rotation = transformx.Rotation.ToQuaternion();


    }

    private float easeOutCubic(float t)
    {
        return 1 - Mathf.Pow(1 - t, 3);

    }
    
    private float easeOutQuad(float t) {
        return 1 - (1 - t) * (1 - t);

    }
}
