# unity_ROS_navigation_demo
This repository is a collection of Unity scripts that were created to set the navigation goal for the move_base action using the Microsoft Hololens 2. 

## Before you begin
Unity-ROS communication is based on [ROS#](https://github.com/siemens/ros-sharp). The created solution for Hololens requires UWP supported [ROS#](https://github.com/EricVoll/ros-sharp). Please read their Wiki and follow the tutorials for them to set up the environment.

## Script functionality
- ApplyScaledTransform is used to scale an object without modifying the coordinate values, this is not necessary for all solutions.
- MapSubscriber is used to subscribe to a topic to get and display a map. 
- MoveBaseActionClient is the basis of using the move_base action in Unity.
- UnityMoveBaseActionClient uses the MoveBaseActionClient and alongside it is the basis of using the move_base action in Unity.

## Message types
Aside from those already included in ROS#, messages from [move_base](http://wiki.ros.org/move_base) need to be generated.
