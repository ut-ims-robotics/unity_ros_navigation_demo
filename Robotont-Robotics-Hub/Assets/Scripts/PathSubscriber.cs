using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosPath = RosMessageTypes.Nav.PathMsg;
using RosPoseStamped = RosMessageTypes.Geometry.PoseStampedMsg;

public class PathSubscriber : MonoBehaviour
{
    [SerializeField]
    private string _topicGlobalTrajectory;
    [SerializeField]
    private string _topicLocalTrajectory;

    [SerializeField]
    private GameObject _globalTrajectoryFrame;
    [SerializeField]
    private GameObject _localTrajectoryFrame;
    [SerializeField]
    private GameObject _planPointPrefab;

    [SerializeField]
    private Material _globalPlanColor;
    [SerializeField]
    private Material _localPlanColor;

    private GameObject _globalPlanPoint;
    private GameObject _localPlanPoint;

    private RosPoseStamped[] _globalPlanPoints;
    private RosPoseStamped[] _localPlanPoints;


    private Vector3 _globalPosition;
    private Vector3 _localPosition;

    // Start is called before the first frame update
    void Start()
    {
        _globalPlanPoint = _planPointPrefab;
        _localPlanPoint = _planPointPrefab;

        //_localPlanPoint.GetComponent<MeshRenderer>().material = _localPlanColor;
        //_globalPlanPoint.GetComponent<MeshRenderer>().material = _globalPlanColor;
        

        ROSConnection.GetOrCreateInstance().Subscribe<RosPath>(_topicGlobalTrajectory, GlobalTrajectoryCb);
        ROSConnection.GetOrCreateInstance().Subscribe<RosPath>(_topicLocalTrajectory, LocalTrajectoryCb);

        //chake the frame_id of trajectories and change the parent of TrajectoriesFrame accordingly
    }


    void GlobalTrajectoryCb(RosPath msg)
    {
        _globalPlanPoints = msg.poses;
        switch (msg.header.frame_id)
        {
            case "map":
                _globalTrajectoryFrame.transform.parent = GameObject.Find("map").transform;
                _globalTrajectoryFrame.transform.localPosition = Vector3.zero;
                break;
            case "odom":
                _globalTrajectoryFrame.transform.parent = GameObject.Find("odom").transform;
                _globalTrajectoryFrame.transform.localPosition = Vector3.zero;
                break;
            default:
                _globalTrajectoryFrame.transform.parent = GameObject.Find("map").transform;
                _globalTrajectoryFrame.transform.localPosition = Vector3.zero;
                break;
        }

        foreach (Transform child in _globalTrajectoryFrame.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (RosPoseStamped GlobalPlanPose in _globalPlanPoints)
        {
            GameObject point = Instantiate(_globalPlanPoint);
            point.transform.SetParent(_globalTrajectoryFrame.transform);

            _globalPosition = new Vector3(-(float)GlobalPlanPose.pose.position.y, (float)GlobalPlanPose.pose.position.z, (float)GlobalPlanPose.pose.position.x);
            point.GetComponent<MeshRenderer>().material = _globalPlanColor;
            point.transform.localPosition = _globalPosition;
            point.transform.localRotation = Quaternion.identity;
        }
    }

    void LocalTrajectoryCb(RosPath msg)
    {
        _localPlanPoints = msg.poses;

        foreach (Transform child in _localTrajectoryFrame.transform)
        {
            Destroy(child.gameObject);
        }

        switch (msg.header.frame_id)
        {
            case "map":
                _localTrajectoryFrame.transform.parent = GameObject.Find("map").transform;
                _localTrajectoryFrame.transform.localPosition = Vector3.zero;
                break;
            case "odom":
                _localTrajectoryFrame.transform.parent = GameObject.Find("odom").transform;
                _localTrajectoryFrame.transform.localPosition = Vector3.zero;
                break;
            default:
                _localTrajectoryFrame.transform.parent = GameObject.Find("map").transform;
                _localTrajectoryFrame.transform.localPosition = Vector3.zero;
                break;
        }

        foreach (RosPoseStamped LocalPlanPose in _localPlanPoints)
        {
            GameObject point = Instantiate(_localPlanPoint);
            point.transform.SetParent(_localTrajectoryFrame.transform);

            _localPosition = new Vector3(-(float)LocalPlanPose.pose.position.y, (float)LocalPlanPose.pose.position.z, (float)LocalPlanPose.pose.position.x);
            point.GetComponent<MeshRenderer>().material = _localPlanColor;
            point.transform.localPosition = _localPosition;
            point.transform.localRotation = Quaternion.identity;
        }
    }
}
