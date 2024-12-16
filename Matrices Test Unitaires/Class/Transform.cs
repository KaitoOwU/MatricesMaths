namespace Maths_Matrices.Tests.Class;

public class Transform
{

    private Vector3 _localPosition;
    public Vector3 LocalPosition
    {
        get => _localPosition;
        set
        {
            _localPosition = value;

            LocalTranslationMatrix = new(4, 4);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == j)
                    {
                        LocalTranslationMatrix[j, i] = 1f;
                    } else if (i == 3 && j < 3)
                    {
                        LocalTranslationMatrix[j, i] = _localPosition[j];
                    }
                    else
                    {
                        LocalTranslationMatrix[j, i] = 0f;
                    }
                }
            }
        }
    }

    public MatrixFloat LocalTranslationMatrix;

    public Transform()
    {
        LocalPosition = new();
        LocalTranslationMatrix = MatrixFloat.Identity(4);
    }
    
}