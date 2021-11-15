using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosGoal = RosMessageTypes.MoveBase.MoveBaseActionGoal;
using RosMessageTypes.Geometry;
using RosMessageTypes.MoveBase;
public class GoalPublisher : MonoBehaviour
{
    ROSConnection ros;
    [SerializeField]
    public string topicName = "/move_base_simple/goal";

    [SerializeField]
    private GameObject _goalObject;

    public PoseStampedMsg _goal;

    public bool to_test = false;

    RosGoal to_send;

    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<PoseStampedMsg>(topicName);

        this._goal.header.frame_id = "map";
        this._goal.header.stamp.sec = 0;
        this._goal.header.stamp.nanosec = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this._goal.pose.position = new PointMsg((float)_goalObject.transform.localPosition.z, -(float)_goalObject.transform.localPosition.x, (float)_goalObject.transform.localPosition.y);
        this._goal.pose.orientation = new QuaternionMsg(0.0f, 0.0f, 0.0f, 1.0f);

        if (to_test)
        {
            GoalPublish();
        }
    }

    public void GoalPublish()
    {

        to_test = false;
        ros.Publish(topicName, this._goal);
    }
}
