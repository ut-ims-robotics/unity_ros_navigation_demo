using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosOdom = RosMessageTypes.Nav.OdometryMsg;

public class OdomSubscriber : MonoBehaviour
{
    [SerializeField]
    private GameObject _robotont;

    private RosUnityConversion _conversion;
    // Start is called before the first frame update
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<RosOdom>("/odom", OdomChange);
    }

    void OdomChange(RosOdom msg)
    {
        Debug.Log(msg.pose.pose);
        Debug.Log("called");


        Quaternion ROSRotation = new Quaternion((float)msg.pose.pose.orientation.x, (float)msg.pose.pose.orientation.y, (float)msg.pose.pose.orientation.z, (float)msg.pose.pose.orientation.w);
        Quaternion UnityRotation = Quaternion.identity;
        UnityRotation.eulerAngles = new Vector3(0.0f, -ROSRotation.eulerAngles.z, 0.0f);

        //_robotont.transform.localPosition = _conversion.ros2unityCoord(msg.pose.pose.position);
        //_robotont.transform.localRotation = _conversion.ros2unityOrientation(msg.pose.pose.orientation);

        _robotont.transform.localPosition = new Vector3(-(float)msg.pose.pose.position.y, (float)msg.pose.pose.position.z, (float)msg.pose.pose.position.x);
        _robotont.transform.localRotation = UnityRotation;
    }
}
