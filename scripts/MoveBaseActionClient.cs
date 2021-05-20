/*
MoveBaseActionClient
*/

using System;
using RosSharp.RosBridgeClient.MessageTypes.MoveBase;
using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    public class MoveBaseActionClient : ActionClient<MoveBaseAction,MoveBaseActionGoal,MoveBaseActionResult,MoveBaseActionFeedback,MoveBaseGoal,MoveBaseResult,MoveBaseFeedback>
    {
        public MoveBaseActionClient(string actionName, RosSocket rosSocket)
        {
            this.actionName = actionName;
            this.rosSocket = rosSocket;
            action = new MoveBaseAction();
            goalStatus = new MessageTypes.Actionlib.GoalStatus();
        }
        protected override MoveBaseActionGoal GetActionGoal()
        {
            return action.action_goal;
        }

        protected override void OnFeedbackReceived()
        {
            Debug.Log(this.GetFeedbackString());
            //throw new NotImplementedException();
        }

        protected override void OnResultReceived()
        {
            Debug.Log(this.GetResultString());
            //throw new NotImplementedException();
        }

        protected override void OnStatusUpdated()
        {
            Debug.Log(this.GetStatusString());
            //throw new NotImplementedException();
        }

        public string GetStatusString()
        {
            if (goalStatus != null)
            {
                return ((ActionStatus)(goalStatus.status)).ToString();
            }
            return "";
        }

        public string GetFeedbackString()
        {
            if (action != null)
                return String.Join(",", action.action_feedback.feedback.base_position.ToString());
            return "";
        }

        public string GetResultString()
        {
            if (action != null)
                return String.Join(",", action.action_result.result.ToString());
            return "";
        }
    }
}