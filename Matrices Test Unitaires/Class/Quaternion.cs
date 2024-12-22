namespace Maths_Matrices.Tests.Class;

public struct Quaternion
{
    public float x, y, z, w;
    public static Quaternion Identity => new(0f, 0f, 0f, 1f);
    
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
}