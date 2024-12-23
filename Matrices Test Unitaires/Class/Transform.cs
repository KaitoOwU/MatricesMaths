using System.Runtime;

namespace Maths_Matrices.Tests.Class;

public class Transform
{

    private Vector3 _localPosition;
    private Vector3 _localRotation;
    private Vector3 _localScale;
    private Transform _parent;
    
    public Vector3 LocalPosition
    {
        get => _localPosition;
        set
        {
            _localPosition = value;
            UpdateMatrices();
        }
    }

    public Vector3 LocalRotation
    {
        get => _localRotation;
        set
        {
            _localRotation = value;
            UpdateMatrices();
        }
    }

    public Vector3 LocalScale
    {
        get => _localScale;
        set
        {
            _localScale = value;
            UpdateMatrices();
        }
    }

    public Vector3 WorldPosition
    {
        get => new(LocalToWorldMatrix[0, 3], LocalToWorldMatrix[1, 3], LocalToWorldMatrix[2, 3]);

        set
        {
            Vector4 localPos = WorldToLocalMatrix * new Vector4(value, 1f);
            LocalPosition = new Vector3(localPos.x, localPos.y, localPos.z);
            UpdateMatrices();
        }
    }

    public Quaternion LocalRotationQuaternion
    {
        get => Quaternion.Euler(_localRotation.x, _localRotation.y, _localRotation.z);
        set
        {
            _localRotation = value.EulerAngles;
            UpdateMatrices();
        }
    }

    public MatrixFloat LocalTranslationMatrix;
    public MatrixFloat LocalRotationMatrix, LocalRotationXMatrix, LocalRotationYMatrix, LocalRotationZMatrix;
    public MatrixFloat LocalScaleMatrix;

    public MatrixFloat LocalToWorldMatrix, WorldToLocalMatrix;

    public Transform()
    {
        _localPosition = new();
        LocalTranslationMatrix = MatrixFloat.Identity(4);

        _localRotation = new();
        LocalRotationMatrix = MatrixFloat.Identity(4);
        LocalRotationXMatrix = MatrixFloat.Identity(4);
        LocalRotationYMatrix = MatrixFloat.Identity(4);
        LocalRotationZMatrix = MatrixFloat.Identity(4);

        _localScale = new(1f, 1f, 1f);
        LocalScaleMatrix = MatrixFloat.Identity(4);

        WorldToLocalMatrix = MatrixFloat.Identity(4);
        LocalToWorldMatrix = MatrixFloat.Identity(4);
    }

    public void SetParent(Transform parent)
    {
        this._parent = parent;
        
        this.LocalToWorldMatrix = parent.LocalToWorldMatrix * this.LocalToWorldMatrix;
        this.WorldToLocalMatrix = LocalToWorldMatrix.InvertByDeterminant();
    }
    
    private void UpdateMatrices()
    {
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
        
        //RotY
        LocalRotationYMatrix[0, 0] = MathF.Cos(_localRotation.y * (MathF.PI / 180f));
        LocalRotationYMatrix[0, 2] = MathF.Sin(_localRotation.y * (MathF.PI / 180f));
        LocalRotationYMatrix[2, 0] = -MathF.Sin(_localRotation.y * (MathF.PI / 180f));
        LocalRotationYMatrix[2, 2] = MathF.Cos(_localRotation.y * (MathF.PI / 180f));
                
        //RotX
        LocalRotationXMatrix[1, 1] = MathF.Cos(_localRotation.x * (MathF.PI / 180f));
        LocalRotationXMatrix[1, 2] = -MathF.Sin(_localRotation.x * (MathF.PI / 180f));
        LocalRotationXMatrix[2, 1] = MathF.Sin(_localRotation.x * (MathF.PI / 180f));
        LocalRotationXMatrix[2, 2] = MathF.Cos(_localRotation.x * (MathF.PI / 180f));
                
        //RotZ
        LocalRotationZMatrix[0, 0] = MathF.Cos(_localRotation.z * (MathF.PI / 180f));
        LocalRotationZMatrix[0, 1] = -MathF.Sin(_localRotation.z * (MathF.PI / 180f));
        LocalRotationZMatrix[1, 0] = MathF.Sin(_localRotation.z * (MathF.PI / 180f));
        LocalRotationZMatrix[1, 1] = MathF.Cos(_localRotation.z * (MathF.PI / 180f));

        //RotMultiAxis
        LocalRotationMatrix = LocalRotationYMatrix * LocalRotationXMatrix * LocalRotationZMatrix;
        
        for (int i = 0, j = 0; i < 3 && j < 3; i++, j++)
        {
            LocalScaleMatrix[j, i] = _localScale[i];
        }


        LocalToWorldMatrix = LocalTranslationMatrix * LocalRotationMatrix * LocalScaleMatrix;
        if (_parent != null)
        {
            LocalToWorldMatrix = _parent.LocalToWorldMatrix * LocalToWorldMatrix;
        }
        WorldToLocalMatrix = LocalToWorldMatrix.InvertByDeterminant();
    }
}