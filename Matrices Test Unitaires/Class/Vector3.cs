namespace Maths_Matrices.Tests.Class;

public class Vector3
{

    public float x, y, z;

    public Vector3(float x = 0f, float y = 0f, float z = 0f)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    
    public float this[int i]
    {
        get
        {
            switch (i)
            {
                case 0:
                    return x;
                
                case 1:
                    return y;
                
                case 2:
                    return z;
                
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        
        set
        {
            switch (value)
            {
                case 0:
                    x = value;
                    return;
                
                case 1:
                    y = value;
                    return;
                
                case 2:
                    z = value;
                    return;
                
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }

}