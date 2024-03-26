using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mathsfx;
using Quaternion = Mathsfx.Quaternion;
using Vector3 = Mathsfx.Vector3;

public class TransformX : MonoBehaviour
{
    
    MeshFilter mesh;
    
    public Vector3 position = Vector3.Zero;
    public Vector3 rotation = Vector3.Zero;
    public Vector3 scale = Vector3.One;

    private Vector3 posBuffer;
    private Vector3 rotBuffer;
    private Vector3 scaleBuffer;

    private Vector3[] modelVertices;

    public Vector3 Radians => new Vector3(rotation.x * MathsfxConst.Deg2Rad, rotation.y * MathsfxConst.Deg2Rad, rotation.z * MathsfxConst.Deg2Rad);
    public Quaternion QuatRotation => Quaternion.FromEuler(rotation);

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>();

        modelVertices = Vector3.ToFx(mesh.mesh.vertices);
    }

    private void Update()
    {
        if(IsChanged())
            UpdateTransform();

        posBuffer = position;
        rotBuffer = rotation;
        scaleBuffer = scale;
    }

    private void UpdateTransform()
    {
        Vector3[] worldVertices = modelVertices;
        Vector3[] result = new Vector3[worldVertices.Length];

        for (int i = 0; i < worldVertices.Length; i++)
        {
            result[i] = new Vector3(Matrix4by4.TRSMatrix(scale, Radians,position) * worldVertices[i]);
        }
        
        mesh.mesh.vertices = Vector3.ToDefault(result);
        
        mesh.mesh.RecalculateNormals();
        mesh.mesh.RecalculateBounds();
        
        
    }

    private bool IsChanged()
    {
        if (posBuffer != position)
            return true;
        
        if (rotBuffer != rotation)
            return true;
        
        if (scaleBuffer != scale)
            return true;

        return false;

    }
}
