using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosPoint = RosMessageTypes.Geometry.PointMsg;
using RosQuaternion = RosMessageTypes.Geometry.QuaternionMsg;

public class RosUnityConversion
{
    
    RosUnityConversion()
    {
    }

    public Vector3 ros2unityCoord(RosPoint RosPosition)
    {
        Vector3 UnityPosition = new Vector3(-(float)RosPosition.y, (float)RosPosition.z, (float)RosPosition.x);
        return UnityPosition;
    }

    public RosPoint unity2rosCoord(Vector3 UnityPosition)
    {
        RosPoint RosPosition = new RosPoint(UnityPosition.z, -UnityPosition.x, UnityPosition.y);
        return RosPosition;
    }

    public Quaternion ros2unityOrientation(RosQuaternion RosOrientation)
    {
        Quaternion UnityOrientation = new Quaternion((float)RosOrientation.x, (float)RosOrientation.y, (float)RosOrientation.z, (float)RosOrientation.w);      //maybe minus?
        UnityOrientation.eulerAngles = new Vector3(UnityOrientation.eulerAngles.y, UnityOrientation.eulerAngles.z, UnityOrientation.eulerAngles.x);
        return UnityOrientation;
    }

    public RosQuaternion unity2rosOrientation(Quaternion UnityOrientation)
    {
        UnityOrientation.eulerAngles = new Vector3(UnityOrientation.eulerAngles.z, UnityOrientation.eulerAngles.x, UnityOrientation.eulerAngles.y);      //maybe minus?
        RosQuaternion RosOrientation = new RosQuaternion(0.0f, 0.0f, 0.0f, 1.0f);
        return RosOrientation;
    }
}
