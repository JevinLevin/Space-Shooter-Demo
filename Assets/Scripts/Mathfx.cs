using System;
using UnityEngine;

namespace Mathsfx
{
    public struct MathsfxConst
    {
        public const float Deg2Rad = 0.017453292f;
        public const float Rad2Deg = 57.29578f;   
    }
    
    [Serializable]
    public class Vector3
{
    public float x, y, z;

    public float Magnitude => Mathf.Sqrt(x * x + y * y + z * z);
    public Vector3 Normalized => this / Magnitude;
    public float Length => Magnitude;

    public static Vector3 Zero => new(0, 0, 0);
    public static Vector3 One => new(1, 1, 1);
    public static Vector3 Up => new(0, 1, 0);
    public static Vector3 Right => new(1, 0, 0);
    public static Vector3 Forward => new(0, 0, 1);

    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    
    public Vector3()
    {
        x = 0;
        y = 0;
        z = 0;
    }
    
    public Vector3(UnityEngine.Vector3 input)
    {
        x = input.x;
        y = input.y;
        z = input.z;
    }

    public Vector3(Vector4 input)
    {
        x = input.x;
        y = input.y;
        z = input.z;
    }

    public static Vector3 operator+ (Vector3 a, Vector3 b)
    {
        return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }
    
    public static Vector3 operator- (Vector3 a, Vector3 b)
    {
        return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }
    
    public static Vector3 operator- (Vector3 a)
    {
        return new Vector3(-a.x, -a.y, -a.z);
    }

    public static Vector3 operator* (Vector3 a, float s)
    {
        return new Vector3(a.x * s, a.y * s, a.z * s);
    }
    
    public static Vector3 operator/ (Vector3 a, float s)
    {
        return new Vector3(a.x / s, a.y / s, a.z / s);
    }

    public static float Dot(Vector3 a, Vector3 b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }

    public UnityEngine.Vector3 ToVector3()
    {
        return new UnityEngine.Vector3(x, y, z);
    }

    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }

    public override string ToString()
    {
        return "(" + x + "," + y + "," + z + ")";
    }

    public static float VecToRad(Vector2 vec)
    {
        return Mathf.Atan2(vec.y, vec.x);
    }

    public static Vector3 RadToVec(float rad)
    {
        Vector2 vec = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        return new Vector3(vec);
    }

    public static Vector3 AngToDir(Vector3 angle)
    {
        Vector3 result = new Vector3(0,0,0);

        result.x = Mathf.Cos(angle.y) * Mathf.Cos(angle.x);
        result.y = Mathf.Sin(angle.x);
        result.z = Mathf.Cos(angle.x) * Mathf.Sin(angle.y);

        return result;
    }

    public static Vector3 Cross(Vector3 a, Vector3 b)
    {
        Vector3 result = new Vector3(0,0,0);

        result.x = a.y * b.z - a.z * b.y;
        result.y = a.z * b.x - a.x * b.z;
        result.z = a.x * b.y - a.y * b.x;

        return result;
    }

    public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        Vector3 c = new Vector3(0,0,0)
        {
            x = a.x * (1 - t) + b.x * t,
            y = a.y * (1 - t) + b.y * t,
            z = a.z * (1 - t) + b.z * t
        };
        return c;


    }

    public void Clamp(float max)
    {
        x = Mathf.Clamp(x, -max, max);
        y = Mathf.Clamp(y, -max, max);
        z = Mathf.Clamp(z, -max, max);
    }

    public static Vector3 AngleAxis(float radians, Vector3 axis, Vector3 vertex)
    {
        Vector3 result = (vertex * Mathf.Cos(radians)) +
                               axis * Dot(vertex, axis) * (1 - Mathf.Cos(radians)) +
                               Cross(axis, vertex) * Mathf.Sin(radians);

        return result;
    }

    public static UnityEngine.Vector3 ToDefault(Vector3 input)
    {
        return new UnityEngine.Vector3(input.x, input.y, input.z);
    }
    
    public static Vector3 ToFx(UnityEngine.Vector3 input)
    {
        return new Vector3(input.x, input.y, input.z);
    }
    
    public static UnityEngine.Vector3[] ToDefault(Vector3[] input)
    {
        UnityEngine.Vector3[] output = new UnityEngine.Vector3[input.Length];

        for(int i = 0; i < input.Length; i++)
        {
            output[i] = Vector3.ToDefault(input[i]);
        }
        
        return output;
    }
    
    public static Vector3[] ToFx(UnityEngine.Vector3[] input)
    {
        Vector3[] output = new Vector3[input.Length];

        for(int i = 0; i < input.Length; i++)
        {
            output[i] = Vector3.ToFx(input[i]);
        }
        
        return output;
    }
    
}

    public class Vector4 : Vector3
    {
        public float w;

        public Vector4() : base(0,0,0)
        {
            w = 0;
        }

        public Vector4(float x, float y, float z, float w) : base(x,y,z)
        {
            this.w = w;
        }
    }
    
    public class Matrix4by4
{
    public static Matrix4by4 Zero => new(Vector3.Zero, Vector3.Zero, Vector3.Zero, Vector3.Zero);
    public static Matrix4by4 Identity => new(new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 0));

    public Matrix4by4(Vector3 c1, Vector3 c2, Vector3 c3, Vector3 c4)
    {
        values = new float[4, 4];

        values[0, 0] = c1.x;
        values[1, 0] = c1.y;
        values[2, 0] = c1.z;
        values[3, 0] = 0;

        values[0, 1] = c2.x;
        values[1, 1] = c2.y;
        values[2, 1] = c2.z;
        values[3, 1] = 0;

        values[0, 2] = c3.x;
        values[1, 2] = c3.y;
        values[2, 2] = c3.z;
        values[3, 2] = 0;

        values[0, 3] = c4.x;
        values[1, 3] = c4.y;
        values[2, 3] = c4.z;
        values[3, 3] = 1;
    }
    
    public Matrix4by4(Vector4 c1, Vector4 c2, Vector4 c3, Vector4 c4)
    {
        values = new float[4, 4];

        values[0, 0] = c1.x;
        values[1, 0] = c1.y;
        values[2, 0] = c1.z;
        values[3, 0] = c1.w;

        values[0, 1] = c2.x;
        values[1, 1] = c2.y;
        values[2, 1] = c2.z;
        values[3, 1] = c2.w;

        values[0, 2] = c3.x;
        values[1, 2] = c3.y;
        values[2, 2] = c3.z;
        values[3, 2] = c3.w;

        values[0, 3] = c4.x;
        values[1, 3] = c4.y;
        values[2, 3] = c4.z;
        values[3, 3] = c4.w;
    }

    public float[,] values;

    public static Vector4 operator *(Matrix4by4 lhs, Vector4 rhs)
    {
        Vector4 result = new Vector4();
        result.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.y + lhs.values[0, 2] * rhs.z + lhs.values[0, 3] * rhs.w;
        result.y = lhs.values[1, 0] * rhs.x + lhs.values[1, 1] * rhs.y + lhs.values[1, 2] * rhs.z + lhs.values[1, 3] * rhs.w;
        result.z = lhs.values[2, 0] * rhs.x + lhs.values[2, 1] * rhs.y + lhs.values[2, 2] * rhs.z + lhs.values[2, 3] * rhs.w;
        result.w = lhs.values[3, 0] * rhs.x + lhs.values[3, 1] * rhs.y + lhs.values[3, 2] * rhs.z + lhs.values[3, 3] * rhs.w;

        return result;

    }

    public static Vector4 operator *(Matrix4by4 lhs, Vector3 rhs)
    {
        Vector4 result = new Vector4();
        result.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.y + lhs.values[0, 2] * rhs.z + lhs.values[0, 3] * 1;
        result.y = lhs.values[1, 0] * rhs.x + lhs.values[1, 1] * rhs.y + lhs.values[1, 2] * rhs.z + lhs.values[1, 3] * 1;
        result.z = lhs.values[2, 0] * rhs.x + lhs.values[2, 1] * rhs.y + lhs.values[2, 2] * rhs.z + lhs.values[2, 3] * 1;
        result.w = lhs.values[3, 0] * rhs.x + lhs.values[3, 1] * rhs.y + lhs.values[3, 2] * rhs.z + lhs.values[3, 3] * 1;

        return result;

    }

    public static Matrix4by4 operator *(Matrix4by4 lhs, Matrix4by4 rhs)
    {
        Matrix4by4 result = Zero;

        result.values[0, 0] = lhs.values[0, 0] * rhs.values[0, 0] + lhs.values[0, 1] * rhs.values[1, 0] + lhs.values[0, 2] * rhs.values[2, 0] + lhs.values[0, 3] * rhs.values[3, 0];
        result.values[0, 1] = lhs.values[0, 0] * rhs.values[0, 1] + lhs.values[0, 1] * rhs.values[1, 1] + lhs.values[0, 2] * rhs.values[2, 1] + lhs.values[0, 3] * rhs.values[3, 1];
        result.values[0, 2] = lhs.values[0, 0] * rhs.values[0, 2] + lhs.values[0, 1] * rhs.values[1, 2] + lhs.values[0, 2] * rhs.values[2, 2] + lhs.values[0, 3] * rhs.values[3, 2];
        result.values[0, 3] = lhs.values[0, 0] * rhs.values[0, 3] + lhs.values[0, 1] * rhs.values[1, 3] + lhs.values[0, 2] * rhs.values[2, 3] + lhs.values[0, 3] * rhs.values[3, 3];

        result.values[1, 0] = lhs.values[1, 0] * rhs.values[0, 0] + lhs.values[1, 1] * rhs.values[1, 0] + lhs.values[1, 2] * rhs.values[2, 0] + lhs.values[1, 3] * rhs.values[3, 0];
        result.values[1, 1] = lhs.values[1, 0] * rhs.values[0, 1] + lhs.values[1, 1] * rhs.values[1, 1] + lhs.values[1, 2] * rhs.values[2, 1] + lhs.values[1, 3] * rhs.values[3, 1];
        result.values[1, 2] = lhs.values[1, 0] * rhs.values[0, 2] + lhs.values[1, 1] * rhs.values[1, 2] + lhs.values[1, 2] * rhs.values[2, 2] + lhs.values[1, 3] * rhs.values[3, 2];
        result.values[1, 3] = lhs.values[1, 0] * rhs.values[0, 3] + lhs.values[1, 1] * rhs.values[1, 3] + lhs.values[1, 2] * rhs.values[2, 3] + lhs.values[1, 3] * rhs.values[3, 3];

        result.values[2, 0] = lhs.values[2, 0] * rhs.values[0, 0] + lhs.values[2, 1] * rhs.values[1, 0] + lhs.values[2, 2] * rhs.values[2, 0] + lhs.values[2, 3] * rhs.values[3, 0];
        result.values[2, 1] = lhs.values[2, 0] * rhs.values[0, 1] + lhs.values[2, 1] * rhs.values[1, 1] + lhs.values[2, 2] * rhs.values[2, 1] + lhs.values[2, 3] * rhs.values[3, 1];
        result.values[2, 2] = lhs.values[2, 0] * rhs.values[0, 2] + lhs.values[2, 1] * rhs.values[1, 2] + lhs.values[2, 2] * rhs.values[2, 2] + lhs.values[2, 3] * rhs.values[3, 2];
        result.values[2, 3] = lhs.values[2, 0] * rhs.values[0, 3] + lhs.values[2, 1] * rhs.values[1, 3] + lhs.values[2, 2] * rhs.values[2, 3] + lhs.values[2, 3] * rhs.values[3, 3];

        result.values[3, 0] = lhs.values[3, 0] * rhs.values[0, 0] + lhs.values[3, 1] * rhs.values[1, 0] + lhs.values[3, 2] * rhs.values[2, 0] + lhs.values[3, 3] * rhs.values[3, 0];
        result.values[3, 1] = lhs.values[3, 0] * rhs.values[0, 1] + lhs.values[3, 1] * rhs.values[1, 1] + lhs.values[3, 2] * rhs.values[2, 1] + lhs.values[3, 3] * rhs.values[3, 1];
        result.values[3, 2] = lhs.values[3, 0] * rhs.values[0, 2] + lhs.values[3, 1] * rhs.values[1, 2] + lhs.values[3, 2] * rhs.values[2, 2] + lhs.values[3, 3] * rhs.values[3, 2];
        result.values[3, 3] = lhs.values[3, 0] * rhs.values[0, 3] + lhs.values[3, 1] * rhs.values[1, 3] + lhs.values[3, 2] * rhs.values[2, 3] + lhs.values[3, 3] * rhs.values[3, 3];

        return result;

    }

    public static Matrix4by4 TranslateMatrix(Vector3 position)
    {
        return new Matrix4by4(
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1),
            new Vector3(position.x, position.y, position.z));
    }

    public static Matrix4by4 RollMatrix(float rotation)
    {
        return new Matrix4by4(
            new Vector3(Mathf.Cos(rotation), Mathf.Sin(rotation), 0),
            new Vector3(-Mathf.Sin(rotation), Mathf.Cos(rotation), 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, 0));
    }

    public static Matrix4by4 PitchMatrix(float rotation)
    {
        return new Matrix4by4(
            new Vector3(1, 0, 0),
            new Vector3(0, Mathf.Cos(rotation), Mathf.Sin(rotation)),
            new Vector3(0, -Mathf.Sin(rotation), Mathf.Cos(rotation)),
            new Vector3(0, 0, 0));
    }

    public static Matrix4by4 YawMatrix(float rotation)
    {
        return new Matrix4by4(
            new Vector3(Mathf.Cos(rotation), 0, -Mathf.Sin(rotation)),
            new Vector3(0, 1, 0),
            new Vector3(Mathf.Sin(rotation), 0, Mathf.Cos(rotation)),
            new Vector3(0, 0, 0));
}

    public static Matrix4by4 ScaleMatrix(Vector3 scale)
    {
        return new Matrix4by4(
            new Vector3(scale.x, 0, 0), 
            new Vector3(0, scale.y, 0), 
            new Vector3(0, 0, scale.z), 
            new Vector3(0, 0, 0));
    }

    public static Matrix4by4 RotationMatrix(Vector3 rotation)
    {
        return YawMatrix(rotation.y) * (PitchMatrix(rotation.x) * RollMatrix(rotation.z));
    }
    
    public static Matrix4by4 RotationMatrix(Quaternion rotation)
    {
        return QuaternionToMatrix(rotation);
    }
    
    
    // http://www.opengl-tutorial.org/assets/faq_quaternions/index.html#Q54
    public static Matrix4by4 QuaternionToMatrix(Quaternion q)
    {
        Matrix4by4 m = Identity;
        
        float xx = q.x * q.x;
        float xy = q.x * q.y;
        float xz = q.x * q.z;
        float xw = q.x * q.w;

        float yy = q.y * q.y;
        float yz = q.y * q.z;
        float yw = q.y * q.w;

        float zz = q.z * q.z;
        float zw = q.z * q.w;


        m.values[0,0]  = 1 - 2 * ( yy + zz );
        m.values[0,1]  =     2 * ( xy - zw );
        m.values[0,2]  =     2 * ( xz + yw );

        m.values[1,0]  =     2 * ( xy + zw );
        m.values[1,1]  = 1 - 2 * ( xx + zz );
        m.values[1,2]  =     2 * ( yz - xw );

        m.values[2,0]  =     2 * ( xz - yw );
        m.values[2,1]  =     2 * ( yz + xw );
        m.values[2,2] = 1 - 2 * ( xx + yy );

        return m;
    }

    public static Matrix4by4 TRSMatrix(Vector3 scale, Quaternion rotation, Vector3 translation)
    {
        return TranslateMatrix(translation) * (RotationMatrix(rotation) * ScaleMatrix(scale));
    }

    public Vector4 GetRow(int row)
    {
        return new Vector4(values[row, 0], values[row, 1], values[row, 2], values[row, 3]);
    }

    public Matrix4by4 InverseTranslate()
    {
        Matrix4by4 result = Identity;

        result.values[0, 3] = -values[0, 3];
        result.values[1, 3] = -values[1, 3];
        result.values[2, 3] = -values[2, 3];

        return result;
    }

    public Matrix4by4 InverseRotation()
    {
        return new Matrix4by4(GetRow(0), GetRow(1), GetRow(2), GetRow(3));
    }

    public Matrix4by4 InverseScale()
    {
        Matrix4by4 result = Identity;

        result.values[0, 0] = 1.0f / values[0, 0];
        result.values[1, 1] = 1.0f / values[1, 1];
        result.values[2, 2] = 1.0f / values[2, 2];

        return result;
    }

    public Matrix4x4 ToMatrix4x4()
    {
        Matrix4x4 result = new Matrix4x4();

        result.m00 = values[0, 0];
        result.m01 = values[0, 1];
        result.m02 = values[0, 2];
        result.m03 = values[0, 3];
        
        result.m10 = values[1, 0];
        result.m11 = values[1, 1];
        result.m12 = values[1, 2];
        result.m13 = values[1, 3];
        
        result.m20 = values[2, 0];
        result.m21 = values[2, 1];
        result.m22 = values[2, 2];
        result.m23 = values[2, 3];
        
        result.m30 = values[3, 0];
        result.m31 = values[3, 1];
        result.m32 = values[3, 2];
        result.m33 = values[3, 3];

        return result;
    }
}
    
    public class Quaternion
    {
        public float w, x, y, z;

        public static Quaternion Identity => new();
        // https://stackoverflow.com/questions/18710545/maintaining-rotation-during-quaternion-normalization
        public float Magnitude => Mathf.Sqrt(x * x + y * y + z * z + w * w);
        public Quaternion Normalized => new Quaternion(x/Magnitude, y/Magnitude, z/Magnitude, w/Magnitude);

        public Quaternion()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 1;
        }
    
        public Quaternion(float angle, Vector3 axis)
        {
            float halfAngle = angle / 2;
            w = Mathf.Cos(halfAngle);
            x = axis.x * Mathf.Sin(halfAngle);
            y = axis.y * Mathf.Sin(halfAngle);
            z = axis.z * Mathf.Sin(halfAngle);
        }

        public Quaternion(Vector3 position)
        {
            x = position.x;
            y = position.y;
            z = position.z;
        }
        
        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        
        public Quaternion(UnityEngine.Quaternion q)
        {
            x = q.x;
            y = q.y;
            z = q.z;
            w = q.w;
        }
    
        public static Quaternion operator* (Quaternion a, Quaternion b)
        {
            Quaternion result = new Quaternion();

            result.w =
                a.w * b.w -
                Vector3.Dot(new Vector3(a.x, a.y, a.z), new Vector3(b.x, b.y, b.z));

            Vector3 vec =
                new Vector3(b.x, b.y, b.z) * a.w +
                new Vector3(a.x, a.y, a.z) * b.w +
                Vector3.Cross(new Vector3(a.x, a.y, a.z), new Vector3(b.x, b.y, b.z));

            result.x = vec.x;
            result.y = vec.y;
            result.z = vec.z;
            
            return result;

        }

        public Quaternion Inverse()
        {
            Quaternion result = new Quaternion();
            result.w = w;
            result.SetAxis(-GetAxis());
            return result;
        }

        public Vector3 GetAxis()
        {
            return new Vector3(x, y, z);
        }

        public void SetAxis(Vector3 axis)
        {
            x = axis.x;
            y = axis.y;
            z = axis.z;
        }

        public Vector4 GetAxisAngle()
        {
            Vector4 result = new();

            float half = Mathf.Acos(w);
            result.w = half * 2;

            result.x = x / Mathf.Sin(half);
            result.y = y / Mathf.Sin(half);
            result.z = z / Mathf.Sin(half);

            return result;
        }

        public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
        {
            t = Mathf.Clamp(t, 0.0f, 1.0f);

            Quaternion c = b * a.Inverse();
            Vector4 axisAngle = c.GetAxisAngle();
            Quaternion result = new Quaternion(axisAngle.w * t, new Vector3(axisAngle.x, axisAngle.y, axisAngle.z));

            return result * a;
        }


        // http://www.opengl-tutorial.org/assets/faq_quaternions/index.html#Q60
        public static Quaternion FromEuler(Vector3 euler)
        {

            float xOver2 = euler.x * Mathf.Deg2Rad * 0.5f;
            float yOver2 = euler.y * Mathf.Deg2Rad * 0.5f;
            float zOver2 = euler.z * Mathf.Deg2Rad * 0.5f;

            float sinXOver2 = Mathf.Sin(xOver2);
            float cosXOver2 = Mathf.Cos(xOver2);
            float sinYOver2 = Mathf.Sin(yOver2);
            float cosYOver2 = Mathf.Cos(yOver2);
            float sinZOver2 = Mathf.Sin(zOver2);
            float cosZOver2 = Mathf.Cos(zOver2);

            Quaternion result = new Quaternion();
            result.x = cosYOver2 * sinXOver2 * cosZOver2 + sinYOver2 * cosXOver2 * sinZOver2;
            result.y = sinYOver2 * cosXOver2 * cosZOver2 - cosYOver2 * sinXOver2 * sinZOver2;
            result.z = cosYOver2 * cosXOver2 * sinZOver2 - sinYOver2 * sinXOver2 * cosZOver2;
            result.w = cosYOver2 * cosXOver2 * cosZOver2 + sinYOver2 * sinXOver2 * sinZOver2;

            return result;
        }
        
        // https://stackoverflow.com/a/70462919
        public Vector3 ToEuler()
        {
            Vector3 angles = new();

            // roll / x
            float sinr_cosp = 2 * (w * x + y * z);
            float cosr_cosp = 1 - 2 * (x * x + y * y);
            angles.x = Mathf.Atan2(sinr_cosp, cosr_cosp);

            // pitch / y
            float sinp = 2 * (w * y - z * x);
            if (Mathf.Abs(sinp) >= 1)
            {
                angles.y = Mathf.PI / 2 * Mathf.Sign(sinp);
            }
            else
            {
                angles.y = Mathf.Asin(sinp);
            }

            // yaw / z
            float siny_cosp = 2 * (w * z + x * y);
            float cosy_cosp = 1 - 2 * (y * y + z * z);
            angles.z = Mathf.Atan2(siny_cosp, cosy_cosp);

            return angles;
        }

        public UnityEngine.Quaternion ToQuaternion()
        {
            return new UnityEngine.Quaternion(x, y, z, w);
        }
        
        
        
        
    
    }
    
}
