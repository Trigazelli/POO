using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public struct MyVector3
{
    private float _x; 
    private float _y; 
    private float _z;

    public static MyVector3 operator +(MyVector3 left, MyVector3 right)
        => new MyVector3(left._x + right._x, left._y + right._y, left._z + right._z);

    public static MyVector3 operator *(MyVector3 left, float right)
        => new MyVector3(left._x * right, left._y * right, left._z * right);

    // Constructeur

    public MyVector3(float x, float y, float z)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    // Propriétés
    public float X
    {
        get { return _x; }
        set { _x = value; }
    }

    public float Y
    {
        get { return _y; }
        set { _y = value; }
    }

    public float Z
    {
        get { return _z; }
        set { _z = value; }
    }

    public float Magnitude => Mathf.Sqrt(SqrtMagnitude);


    public float SqrtMagnitude => _x * _x + _y * _y + _z * _z;

    // Méthodes
    public void Normalize()
    {
        float magnitude = Magnitude;
        _x = _x / magnitude;
        _y = _y / magnitude;
        _z = _z / magnitude;
    }    
}
