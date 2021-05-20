/*
UnityMoveBaseActionClient
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [RequireComponent(typeof(RosConnector))]
    public class UnityMoveBaseActionClient : MonoBehaviour
    {
        private RosConnector rosConnector;
        public MoveBaseActionClient moveBaseActionClient;

        public string actionName;
        public Transform goalTransform;
        public Transform scaleTransform;
        public string status = "";
        public string feedback = "";
        public string result = "";
        public bool canSendGoal { get; set; }
        public bool canCancelGoal { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            rosConnector = GetComponent<RosConnector>();
            moveBaseActionClient = new MoveBaseActionClient(actionName, rosConnector.RosSocket);
            moveBaseActionClient.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            status = moveBaseActionClient.GetStatusString();
            feedback = moveBaseActionClient.GetFeedbackString();
            result = moveBaseActionClient.GetResultString();
            if (canSendGoal)
            {
                GetGeometryPoint(ScaleVector3(goalTransform.position.Unity2Ros(),scaleTransform.localScale.x), moveBaseActionClient.action.action_goal.goal.target_pose.pose.position);
                GetGeometryQuaternion(goalTransform.rotation.Unity2Ros(), moveBaseActionClient.action.action_goal.goal.target_pose.pose.orientation);
                moveBaseActionClient.action.action_goal.goal.target_pose.header.frame_id = "map";
                moveBaseActionClient.SendGoal();
                canSendGoal = false;
            }
            if (canCancelGoal)
            {
                moveBaseActionClient.CancelGoal();
                canCancelGoal = false;
            }
        }

        private static void GetGeometryPoint(Vector3 position, MessageTypes.Geometry.Point geometryPoint)
        {
            geometryPoint.x = position.x;
            geometryPoint.y = position.y;
            geometryPoint.z = position.z;
        }

        private static void GetGeometryQuaternion(Quaternion quaternion, MessageTypes.Geometry.Quaternion geometryQuaternion)
        {
            geometryQuaternion.x = quaternion.x;
            geometryQuaternion.y = quaternion.y;
            geometryQuaternion.z = quaternion.z;
            geometryQuaternion.w = quaternion.w;
        }
        public static Vector3 ScaleVector3(Vector3 inputVector3,float scale)
        {
            return new Vector3(inputVector3.x/scale, inputVector3.y/scale, inputVector3.z/scale);
        }
    }
}