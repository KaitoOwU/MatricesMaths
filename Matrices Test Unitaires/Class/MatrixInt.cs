namespace Maths_Matrices.Tests.Class;

public class MatrixInt
{

    private int[,] _matrix;
    public int NbLines => _matrix.GetLength(0);
    public int NbColumns => _matrix.GetLength(1);

    #region Constructors
    public MatrixInt(int lines, int columns)
    {
        _matrix = new int[lines, columns];
    }

    public MatrixInt(int[,] matrix)
    {
        _matrix = matrix;
    }

    public MatrixInt(MatrixInt matrix)
    {
        _matrix = new int[matrix.NbLines, matrix.NbColumns];
        for (int i = 0; i < matrix.NbLines; i++)
        {
            for (int j = 0; j < matrix.NbColumns; j++)
            {
                _matrix[i, j] = matrix[i, j];
            }
        }
    }
    #endregion
    
    #region Operators

    public int this[int line, int column]
    {
        get => _matrix[line, column];
        set => _matrix[line, column] = value;
    }

    public static MatrixInt operator *(MatrixInt matrix, int value)
    {
        return MatrixInt.Multiply(matrix, value);
    }

    public static MatrixInt operator *(int value, MatrixInt matrix)
    {
        return matrix * value;
    }
    
    public static MatrixInt operator *(MatrixInt matrixA, MatrixInt matrixB)
    {
        MatrixInt mReturn = matrixA.Multiply(matrixB);
        return mReturn;
    }

    public static MatrixInt operator -(MatrixInt matrix)
    {
        MatrixInt mReturn = new(matrix);
        for (int i = 0; i < mReturn.NbLines; i++)
        {
            for (int j = 0; j < mReturn.NbColumns; j++)
            {
                mReturn[i, j] = -mReturn[i, j];
            }
        }

        return mReturn;
    }
    
    public static MatrixInt operator -(MatrixInt matrixA, MatrixInt matrixB)
    {
        MatrixInt mReturn = new(matrixA);
        mReturn += -matrixB;
        return mReturn;
    }
    
    public static MatrixInt operator +(MatrixInt matrixA, MatrixInt matrixB)
    {
        return MatrixInt.Add(matrixA, matrixB);
    }
    
    #endregion
    
    #region Methods

    public bool IsIdentity()
    {
        if (NbLines != NbColumns)
            return false;
        
        MatrixInt identity = MatrixInt.Identity(NbLines);
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbLines; j++)
            {
                if (identity[i, j] != this[i, j])
                    return false;
            }
        }

        return true;
    }

    public void Multiply(int value)
    {
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbLines; j++)
            {
                this[i, j] *= value;
            }
        }
    }
    
    public void Add(MatrixInt matrix)
    {
        if (NbLines != matrix.NbLines || NbColumns != matrix.NbColumns)
        {
            throw new MatrixSumException();
        }

        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                this[i, j] += matrix[i, j];
            }
        }
    }

    public MatrixInt Multiply(MatrixInt matrix)
    {
        if (this.NbColumns != matrix.NbLines)
            throw new MatrixMultiplyException();
        
        MatrixInt mReturn = new MatrixInt(this.NbLines, matrix.NbColumns);
        for (int i = 0; i < mReturn.NbLines; i++)
        {
            for (int j = 0; j < mReturn.NbColumns; j++)
            {
                int value = 0;
                for(int k = 0; k < this.NbColumns; k++)
                {
                    value += this[i, k] * matrix[k, j];
                }

                mReturn[i, j] = value;
            }
        }

        return mReturn;
    }
    
    public MatrixInt Transpose()
    {
        MatrixInt mReturn = new MatrixInt(NbColumns, NbLines);
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                mReturn[j, i] = this[i, j];
            }
        }

        return mReturn;
    }
    
    public (MatrixInt matrixA, MatrixInt matrixB) Split(int column)
    {
        column++;
        MatrixInt matrixA = new MatrixInt(NbLines, column);
        MatrixInt matrixB = new MatrixInt(NbLines, NbColumns - column);
        
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                if(j <  column)
                    matrixA[i, j] = this[i, j];
                else
                    matrixB[i, j - column] = this[i, j];
            }
        }

        return (matrixA, matrixB);
    }
    #endregion
    
    #region Static Methods

    public static MatrixInt Identity(int size)
    {
        MatrixInt mReturn = new MatrixInt(size, size);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if(i == j)
                    mReturn[i, j] = 1;
                else
                    mReturn[i, j] = 0;
            }
        }

        return mReturn;
    }

    public static MatrixInt Multiply(MatrixInt m, int value)
    {
        MatrixInt mReturn = new(m);
        mReturn.Multiply(value);
        return mReturn;
    }
    
    public static MatrixInt Add(MatrixInt matrixA, MatrixInt matrixB)
    {
        MatrixInt mReturn = new(matrixA);
        if (mReturn.NbLines != matrixB.NbLines || mReturn.NbColumns != matrixB.NbColumns)
        {
            throw new MatrixSumException();
        }

        for (int i = 0; i < mReturn.NbLines; i++)
        {
            for (int j = 0; j < mReturn.NbColumns; j++)
            {
                mReturn[i, j] += matrixB[i, j];
            }
        }

        return mReturn;
    }

    public static MatrixInt Multiply(MatrixInt matrixA, MatrixInt matrixB)
    {
        MatrixInt mReturn = matrixA.Multiply(matrixB);
        return mReturn;
    }

    public static MatrixInt Transpose(MatrixInt matrix)
    {
        MatrixInt mReturn = matrix;
        return mReturn.Transpose();
    }
    
    public static MatrixInt GenerateAugmentedMatrix(MatrixInt matrixA, MatrixInt matrixB)
    {
        MatrixInt mReturn = new MatrixInt(matrixA.NbLines, matrixA.NbColumns + matrixB.NbColumns);
        for (int i = 0; i < matrixA.NbLines; i++)
        {
            for (int j = 0; j < matrixA.NbColumns; j++)
            {
                mReturn[i, j] = matrixA[i, j];
            }
        }

        for (int i = 0; i < matrixB.NbLines; i++)
        {
            for (int j = 0; j < matrixB.NbColumns; j++)
            {
                mReturn[i, matrixA.NbColumns + j] = matrixB[i, j];
            }
        }

        return mReturn;
    }
    #endregion

    public int[,] ToArray2D()
    {
        return _matrix;
    }
}

#region Exceptions
public class MatrixSumException : Exception{}
public class MatrixMultiplyException : Exception{}
public class MatrixScalarZeroException : Exception{}
public class MatrixInvertException : Exception{}
#endregion