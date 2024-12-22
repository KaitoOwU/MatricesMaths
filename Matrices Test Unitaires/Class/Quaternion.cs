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
}