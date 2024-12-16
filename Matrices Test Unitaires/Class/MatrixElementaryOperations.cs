namespace Maths_Matrices.Tests.Class;

public static class MatrixElementaryOperations
{
    public static void SwapLines(MatrixInt matrix, int line1, int line2)
    {
        for (int i = 0; i < matrix.NbColumns; i++)
        {
            int temp = matrix[line1, i];
            matrix[line1, i] = matrix[line2, i];
            matrix[line2, i] = temp;
        }
    }

    public static void SwapColumns(MatrixInt matrix, int column1, int column2)
    {
        for (int i = 0; i < matrix.NbLines; i++)
        {
            int temp = matrix[i, column1];
            matrix[i, column1] = matrix[i, column2];
            matrix[i, column2] = temp;
        }
    }

    public static void MultiplyLine(MatrixInt matrix, int line1, int line2)
    {
        if (line1 == line2)
            throw new MatrixScalarZeroException();
        
        for (int i = 0; i < matrix.NbColumns; i++)
        {
            matrix[line1, i] *= line2;
        }
    }

    public static void MultiplyColumn(MatrixInt matrix, int column1, int column2)
    {
        if (column1 == column2)
            throw new MatrixScalarZeroException();
        
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, column1] *= column2;
        }
    }

    public static void AddLineToAnother(MatrixInt matrix, int line1, int line2, int factor = 1)
    {
        for (int i = 0; i < matrix.NbColumns; i++)
        {
            matrix[line2, i] += matrix[line1, i] * factor;
        }
    }

    public static void AddColumnToAnother(MatrixInt matrix, int column1, int column2, int factor = 1)
    {
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, column2] += matrix[i, column1] * factor;
        }
    }
    
    //FLOATS
    public static void SwapLines(MatrixFloat matrix, int line1, int line2)
    {
        for (int i = 0; i < matrix.NbColumns; i++)
        {
            float temp = matrix[line1, i];
            matrix[line1, i] = matrix[line2, i];
            matrix[line2, i] = temp;
        }
    }

    public static void SwapColumns(MatrixFloat matrix, int column1, int column2)
    {
        for (int i = 0; i < matrix.NbLines; i++)
        {
            float temp = matrix[i, column1];
            matrix[i, column1] = matrix[i, column2];
            matrix[i, column2] = temp;
        }
    }

    public static void MultiplyLine(MatrixFloat matrix, int line1, int line2)
    {
        if (line1 == line2)
            throw new MatrixScalarZeroException();
        
        for (int i = 0; i < matrix.NbColumns; i++)
        {
            matrix[line1, i] *= line2;
        }
    }

    public static void MultiplyColumn(MatrixFloat matrix, int column1, int column2)
    {
        if (column1 == column2)
            throw new MatrixScalarZeroException();
        
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, column1] *= column2;
        }
    }

    public static void AddLineToAnother(MatrixFloat matrix, int line1, int line2, float factor = 1f)
    {
        for (int i = 0; i < matrix.NbColumns; i++)
        {
            matrix[line2, i] += matrix[line1, i] * factor;
        }
    }

    public static void AddColumnToAnother(MatrixFloat matrix, int column1, int column2, float factor = 1f)
    {
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, column2] += matrix[i, column1] * factor;
        }
    }

    public static void MultiplyLineByFactor(MatrixFloat matrix, int line, float factor = 1f)
    {
        for (int i = 0; i < matrix.NbColumns; i++)
        {
            matrix[line, i] *= factor;
        }
    }
}