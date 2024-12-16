namespace Maths_Matrices.Tests.Class;

public class Vector4
{
    public float x, y, z, w;

    public Vector4(float x = 0f, float y = 0f, float z = 0f, float w = 0f)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
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

                case 3:
                    return w;
            }
            throw new IndexOutOfRangeException();
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

                case 3:
                    w = value;
                    return;
            }
            throw new IndexOutOfRangeException();
        }
    }
}