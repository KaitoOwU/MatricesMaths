namespace Maths_Matrices.Tests.Class;

public struct Quaternion
{
    public float x, y, z, w;
    public static Quaternion Identity => new(0f, 0f, 0f, 1f);

    public MatrixFloat Matrix
    {
        get
        {
            MatrixFloat m = new(4, 4);
            m[0, 0] = 1f - 2f * (y * y + z * z);
            m[0, 1] = 2f * (x * y - z * w);
            m[0, 2] = 2f * (x * z + y * w);
            m[0, 3] = 0f;
            m[1, 0] = 2f * (x * y + z * w);
            m[1, 1] = 1f - 2f * (x * x + z * z);
            m[1, 2] = 2f * (y * z - x * w);
            m[1, 3] = 0f;
            m[2, 0] = 2f * (x * z - y * w);
            m[2, 1] = 2f * (y * z + x * w);
            m[2, 2] = 1f - 2f * (x * x + y * y);
            m[2, 3] = 0f;
            m[3, 0] = 0f;
            m[3, 1] = 0f;
            m[3, 2] = 0f;
            m[3, 3] = 1f;

            return m;
        }
    }

    public Vector3 EulerAngles
    {
        get
        {
            var matrix = Matrix;
            float pitch = MathF.Asin(-matrix[1, 2]);
            
            float heading;
            float banking;
            if (MathF.Abs(MathF.Cos(pitch)) > 0.001d) 
            {
                heading = MathF.Atan2(matrix[0, 2], matrix[2, 2]);
                banking = MathF.Atan2(matrix[1, 0], matrix[1, 1]);
            }
            else
            {
                heading = MathF.Atan2(-matrix[2, 0], matrix[0, 0]);
                banking = 0f;
            }

            return new Vector3(pitch, heading, banking) * (180 / MathF.PI);
        }
    }
    
    public Quaternion(float x = 0f, float y = 0f, float z = 0f, float w = 1f)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    
    public static Quaternion operator *(Quaternion a, Quaternion b)
    {
        Quaternion q = new();
        q.x = a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y;
        q.y = a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x;
        q.z = a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w;
        q.w = a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z;

        return q;
    }
    
    public static Vector3 operator *(Quaternion q, Vector3 v)
    {
        Quaternion p = new(v.x, v.y, v.z, 0f);
        Quaternion qInv = new(-q.x, -q.y, -q.z, q.w);
        Quaternion result = q * p * qInv;
        
        return new(result.x, result.y, result.z);
    }

    public static Quaternion AngleAxis(float angle, Vector3 axis)
    {
        Quaternion q = new();
        axis = axis.Normalize();
        q.x = axis.x * MathF.Sin(angle * MathF.PI/180f / 2f);
        q.y = axis.y * MathF.Sin(angle * MathF.PI/180f / 2f);
        q.z = axis.z * MathF.Sin(angle * MathF.PI/180f / 2f);
        q.w = MathF.Cos(angle * MathF.PI/180f / 2f);

        return q;
    }
    
    public static Quaternion Euler(float x, float y, float z)
    {
        Quaternion q = new();
        Quaternion qRY = AngleAxis(y, new Vector3(0f, 1f, 0f));
        Quaternion qRX = AngleAxis(x, new Vector3(1f, 0f, 0f));
        Quaternion qRZ = AngleAxis(z, new Vector3(0f, 0f, 1f));
        q = qRY * qRX * qRZ;

        return q;
    }
}