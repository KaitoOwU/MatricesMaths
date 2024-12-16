namespace Maths_Matrices.Tests.Class;

public class MatrixFloat
{

    private float[,] _matrix;
    public int NbLines => _matrix.GetLength(0);
    public int NbColumns => _matrix.GetLength(1);

    #region Constructors
    public MatrixFloat(int lines, int columns)
    {
        _matrix = new float[lines, columns];
    }

    public MatrixFloat(float[,] matrix)
    {
        _matrix = matrix;
    }

    public MatrixFloat(MatrixFloat matrix)
    {
        _matrix = new float[matrix.NbLines, matrix.NbColumns];
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

    public float this[int line, int column]
    {
        get => _matrix[line, column];
        set => _matrix[line, column] = value;
    }

    public static MatrixFloat operator *(MatrixFloat matrix, float value)
    {
        return MatrixFloat.Multiply(matrix, value);
    }

    public static MatrixFloat operator *(float value, MatrixFloat matrix)
    {
        return matrix * value;
    }
    
    public static MatrixFloat operator *(MatrixFloat matrixA, MatrixFloat matrixB)
    {
        MatrixFloat mReturn = matrixA.Multiply(matrixB);
        return mReturn;
    }

    public static Vector4 operator *(MatrixFloat matrix, Vector4 vector)
    {
        var v = new Vector4(
            vector[0] * matrix[0, 0] + vector[1] * matrix[0, 1] + vector[2] * matrix[0, 2] + vector[3] * matrix[0, 3],
            vector[0] * matrix[1, 0] + vector[1] * matrix[1, 1] + vector[2] * matrix[1, 2] + vector[3] * matrix[1, 3],
            vector[0] * matrix[2, 0] + vector[1] * matrix[2, 1] + vector[2] * matrix[2, 2] + vector[3] * matrix[2, 3],
            vector[0] * matrix[3, 0] + vector[1] * matrix[3, 1] + vector[2] * matrix[3, 2] + vector[3] * matrix[3, 3]
            );
        
        return v;
    }

    public static MatrixFloat operator -(MatrixFloat matrix)
    {
        MatrixFloat mReturn = new(matrix);
        for (int i = 0; i < mReturn.NbLines; i++)
        {
            for (int j = 0; j < mReturn.NbColumns; j++)
            {
                mReturn[i, j] = -mReturn[i, j];
            }
        }

        return mReturn;
    }
    
    public static MatrixFloat operator -(MatrixFloat matrixA, MatrixFloat matrixB)
    {
        MatrixFloat mReturn = new(matrixA);
        mReturn += -matrixB;
        return mReturn;
    }
    
    public static MatrixFloat operator +(MatrixFloat matrixA, MatrixFloat matrixB)
    {
        return MatrixFloat.Add(matrixA, matrixB);
    }
    
    #endregion
    
    #region Methods

    public bool IsIdentity()
    {
        if (NbLines != NbColumns)
            return false;
        
        MatrixFloat identity = MatrixFloat.Identity(NbLines);
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

    public void Multiply(float value)
    {
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbLines; j++)
            {
                this[i, j] *= value;
            }
        }
    }
    
    public void Add(MatrixFloat matrix)
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

    public MatrixFloat Multiply(MatrixFloat matrix)
    {
        if (this.NbColumns != matrix.NbLines)
            throw new MatrixMultiplyException();
        
        MatrixFloat mReturn = new MatrixFloat(this.NbLines, matrix.NbColumns);
        for (int i = 0; i < mReturn.NbLines; i++)
        {
            for (int j = 0; j < mReturn.NbColumns; j++)
            {
                float value = 0;
                for(int k = 0; k < this.NbColumns; k++)
                {
                    value += this[i, k] * matrix[k, j];
                }

                mReturn[i, j] = value;
            }
        }

        return mReturn;
    }
    
    public MatrixFloat Transpose()
    {
        MatrixFloat mReturn = new MatrixFloat(NbColumns, NbLines);
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                mReturn[j, i] = this[i, j];
            }
        }

        return mReturn;
    }
    
    public (MatrixFloat matrixA, MatrixFloat matrixB) Split(int column)
    {
        column++;
        MatrixFloat matrixA = new MatrixFloat(NbLines, column);
        MatrixFloat matrixB = new MatrixFloat(NbLines, NbColumns - column);
        
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
    
    public MatrixFloat InvertByRowReduction()
    {
        var invert = MatrixRowReductionAlgorithm.Apply(this, Identity(NbLines), true);
        return invert.matrixB;
    }
    
    public MatrixFloat SubMatrix(int line, int column)
    {
        MatrixFloat mReturn = new MatrixFloat(NbLines - 1, NbColumns - 1);
        for(int i = 0; i < NbLines; i++)
        {
            if(i == line)
                continue;
            
            for(int j = 0; j < NbColumns; j++)
            {
                if(j == column)
                    continue;
                
                int iReturn = i < line ? i : i - 1;
                int jReturn = j < column ? j : j - 1;
                mReturn[iReturn, jReturn] = this[i, j];
            }
        }

        return mReturn;
    }
    
    public MatrixFloat Adjugate()
    {
        MatrixFloat mReturn = new MatrixFloat(NbLines, NbColumns);
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                float sign = (i + j) % 2 == 0 ? 1 : -1;
                mReturn[j, i] = sign * Determinant(SubMatrix(i, j));
            }
        }

        return mReturn;
    }
    
    public MatrixFloat InvertByDeterminant()
    {
        float determinant = Determinant(this);
        if (determinant == 0)
            throw new MatrixInvertException();
        
        return Adjugate() * (1 / determinant);
    }
    #endregion
    
    #region Static Methods

    public static MatrixFloat Identity(int size)
    {
        MatrixFloat mReturn = new MatrixFloat(size, size);
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

    public static MatrixFloat Multiply(MatrixFloat m, float value)
    {
        MatrixFloat mReturn = new(m);
        mReturn.Multiply(value);
        return mReturn;
    }
    
    public static MatrixFloat Add(MatrixFloat matrixA, MatrixFloat matrixB)
    {
        MatrixFloat mReturn = new(matrixA);
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

    public static MatrixFloat Multiply(MatrixFloat matrixA, MatrixFloat matrixB)
    {
        MatrixFloat mReturn = matrixA.Multiply(matrixB);
        return mReturn;
    }

    public static MatrixFloat Transpose(MatrixFloat matrix)
    {
        MatrixFloat mReturn = matrix;
        return mReturn.Transpose();
    }
    
    public static MatrixFloat GenerateAugmentedMatrix(MatrixFloat matrixA, MatrixFloat matrixB)
    {
        MatrixFloat mReturn = new MatrixFloat(matrixA.NbLines, matrixA.NbColumns + matrixB.NbColumns);
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
    
    public static MatrixFloat InvertByRowReduction(MatrixFloat matrix)
    {
        return matrix.InvertByRowReduction();
    }
    
    public static MatrixFloat SubMatrix(MatrixFloat matrix, int line, int column)
    {
        return matrix.SubMatrix(line, column);
    }
    
    public static float Determinant(MatrixFloat matrix)
    {
        if (matrix.NbLines > 2 && matrix.NbColumns > 2)
        {
            float determinant = 0;
            for(int i = 0; i < matrix.NbColumns; i++)
            {
                float sign = i % 2 == 0 ? 1 : -1;
                determinant += sign * matrix[0, i] * Determinant(matrix.SubMatrix(0, i));
            }

            return determinant;
        }
        else if(matrix.NbLines > 1 && matrix.NbColumns > 1)
        {
            return (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);
        } else
        {
            return matrix[0, 0];
        }
    }

    public static MatrixFloat Adjugate(MatrixFloat matrix)
    {
        return matrix.Adjugate();
    }
    
    public static MatrixFloat InvertByDeterminant(MatrixFloat matrix)
    {
        return matrix.InvertByDeterminant();
    }
    #endregion

    public float[,] ToArray2D()
    {
        return _matrix;
    }
}