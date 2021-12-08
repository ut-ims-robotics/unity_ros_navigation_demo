# unity_ROS_navigation_demo
This repository contains the Unity project which is able to connect to Robotont (simulated and real), display the map, show the position on the map and send navigation goals.

## Prerequisites

- ROS machine with ROS Noetic or real Robotont.
- Unity side of things should be done on Windows machine
- [Unity Hub](https://unity3d.com/get-unity/download)
- [Enable Developer Mode on Hololens and Windows Machine](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/advanced-concepts/using-visual-studio?tabs=hl2)

## Installation

### Hololens side

1. Clone this repository.
2. Open Unity Hub.
    - If this is your first time using Unity Hub, license should be activated:
        1. Go to settings (gear in the corner)
        2. Go to manage license tab.
        3. Log into Unity account or create one, when asked.
        4. Press activate new license button.
        5. Choose personal and choose the answer, which suits you after that.
        6. Personal license will be acctivated.
3. Press ADD button and choose the [Robotont-Robotics-Hub](https://github.com/ut-ims-robotics/unity_ros_navigation_demo/tree/igor_devel_robotics_hub/Robotont-Robotics-Hub) folder.
4. Install the unity version, which you will be asked.
    - During the installation make sure to check Visual Studio 2019, if you don't have it installed
    - And make sure to check Universal Windows Platform (UWP) Support and Windows build support. If you miss this step, you can add them later in the installs tab if you press 3 dots on the Unity version and choose Add Modules option
5. After installation open the project. Skip the prompt about new unity version.
6. MRTK will prompt you to choose the XR plugin. Choose OpenXR and follow the setup guide provided by the MRTK window.
6. In the project panel navigate to Assets/Scenes and double click on RobotontNavigation. This will open the scene in unity.
7. In the Hierarchy click on ROSConnectionPrefab game object.
8. In Ros Connection (Script) change Ros IP Adress to the IP of your ROS machine.
9. Go to File -> Build Settings.
10. If you are going to use Robotont, it is recomended to disable the Map Subscriber script in ROSCommunication game object to remove delay it introduces. Just uncheck it.
    - This option can be left on for simulated robotont. If map size is not several dozen meters in size and wireless connection as fast, there will be no lag.
11. Make sure, that UWP is chosen (if not, choose it and press switch platform)
12. Verify that only the current scene is chosen in the Scenes in Build window.
13. Set terget device to Hololens and Architecture to ARM64.
14. Press build and choose folder where to save the built project.
15. After building go to the specified folder and open the solution file.
16. Go to Project -> Properties and to Debugging tab.
17. Fill in Machine Name with the ip address of the Hololens 2
18. Under the menu bar change debug to release, ARM to ARM64 and set to remote machine and press start debugging.
19. After deploying to Hololens, app will launch automatically, but you will be able to open it yourself from the app menu on the hololens

### ROS side

#### Real Robotont

1. Clone [this repository](https://github.com/Unity-Technologies/ROS-TCP-Endpoint.git) into robotonts catkin workspace.
2. Open ROS-TCP-Endpoint/config/params.yaml and change ROS_IP to the IP of robotont.
3. Build the package.
#### Simulated Robotont

1. Clone the following Repositories into src folder of your workspace:
    - [robotont_gazebo](https://github.com/robotont/robotont_gazebo.git)
    - [robotont_description](https://github.com/robotont/robotont_description.git)
    - [robotont_nuc_description](https://github.com/robotont/robotont_nuc_description.git)
    - [robotont_navigation](https://github.com/robotont/robotont_navigation.git)
    - [robotont_demos](https://github.com/robotont/robotont_demos.git)
2. Clone [this repository](https://github.com/Unity-Technologies/ROS-TCP-Endpoint.git) into your catkin workspace.
3. Open ROS-TCP-Endpoint/config/params.yaml and change ROS_IP to the IP of your ROS computer.
4. Install realsense2-description `sudo apt install ros-noetic-realsense2-description`
5. Install gmapping `sudo apt install ros-noetic-gmapping`
6. Build workspace

## Running the setup

### ROS side

#### Real Robotont

1. Launch 2d_slam with `roslaunch robotont_demos 2d_slam.launch`
2. Launch ros_tcp_endpoint `roslaunch ros_tcp_endpoint endpoint.launch`

#### Simulated Robotont

Minimaze is going to be used.

1. Launch simulated robotont with `roslaunch robotont_gazebo world_minimaze.launch`
2. Launch 2d_slam with `roslaunch robotont_demos 2d_slam.launch`
3. Launch ros_tcp_endpoint `roslaunch ros_tcp_endpoint endpoint.launch`

### Unity side

1. Make sure to be in the same network as ROS machine.
2. Start the Robotont-Robotics-Hub app
3. If the setup is done correctly,  Hololens will connect to the ROS machine and you will be able to set the goal for the robotont to while observing the movement. If map was left enabled, then it will also be displayed.
