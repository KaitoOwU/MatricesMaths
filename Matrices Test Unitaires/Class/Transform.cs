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
            MatrixFloat vectorToMatrix = new MatrixFloat(1, 4);
            for (int i = 0; i < 3; i++)
            {
                vectorToMatrix[0, i] = value[i];
            }
            vectorToMatrix[0, 3] = 1f;
            
            LocalTranslationMatrix = 
                MatrixFloat.GenerateAugmentedMatrix(
                    MatrixFloat.Identity(4).Split(2).matrixA,
                    vectorToMatrix
                    );
        }
    }

    public MatrixFloat LocalTranslationMatrix;

    public Transform()
    {
        LocalPosition = new();
        LocalTranslationMatrix = MatrixFloat.Identity(4);
    }
    
}