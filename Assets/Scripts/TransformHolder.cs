using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHolder : MonoBehaviour
{
    public static TransformHolder Instance;

    [SerializeField] private Transform rightGroupStartPoint;
    [SerializeField] private Transform rightTeleportPoint;
    [SerializeField] private Transform leftGroupStartPoint;
    [SerializeField] private Transform leftTeleportPoint;
    [SerializeField] private Transform centerPoint;

    private void Awake()
    {
        Instance = this;
    }

    public Vector3 GetRightStartPos() 
    { 
        if(rightGroupStartPoint) 
            return rightGroupStartPoint.position;

        return Vector3.zero;
    }

    public Vector3 GetLeftStartPos() 
    { 
        if(leftGroupStartPoint)
            return leftGroupStartPoint.position;

        return Vector3.zero;
    }      

    public Vector3 GetRightTeleportPos() 
    { 
        if(rightTeleportPoint)
            return rightTeleportPoint.position;

        return Vector3.zero;
    }

    public Vector3 GetLeftTeleportPos() 
    { 
        if(leftTeleportPoint)
            return leftTeleportPoint.position;

        return Vector3.zero;
    }

    public Vector3 GetCenterPos()
    {
        if(centerPoint)
            return centerPoint.position;

        return Vector3.zero;
    }
}
