namespace Maths_Matrices.Tests.Class;

public static class MatrixRowReductionAlgorithm
{
    public static (MatrixFloat matrixA, MatrixFloat matrixB) Apply(MatrixFloat matrixA, MatrixFloat matrixB, bool throwException = false)
    {

        MatrixFloat augmented = MatrixFloat.GenerateAugmentedMatrix(matrixA, matrixB);
        for (int i = 0, j = 0; i < matrixA.NbLines && j < matrixA.NbColumns; i++, j++)
        {
            int biggestLine = -1;
            for(int k = i; k < augmented.NbLines; k++)
            {
                if (biggestLine < 0 && augmented[k, j] != 0)
                {
                    biggestLine = k;
                } else if (biggestLine > 0 && augmented[k, j] > augmented[biggestLine, j])
                {
                    biggestLine = k;
                }
            }

            if (biggestLine == -1)
            {
                if (throwException)
                {
                    throw new MatrixInvertException();
                }
                else
                {
                    continue;
                }
            }
                

            if (biggestLine != i)
            {
                MatrixElementaryOperations.SwapLines(augmented, biggestLine, i);
            }
            MatrixElementaryOperations.MultiplyLineByFactor(augmented, i, 1f / augmented[i, j]);

            for (int r = 0; r < augmented.NbLines; r++)
            {
                if (r != i)
                {
                    MatrixElementaryOperations.AddLineToAnother(augmented, i, r, -augmented[r, j]);
                }
            }
        }

        return augmented.Split(matrixA.NbColumns - 1);
    }
}