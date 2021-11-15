using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosTFMessage = RosMessageTypes.Tf2.TFMessageMsg;

public class OdomCompensationSubscriber : MonoBehaviour
{

    [SerializeField]
    private GameObject _compensationFrame;
    private Vector3 UnityPosition;
    private Quaternion UnityRotation;

    // Start is called before the first frame update
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<RosTFMessage>("/tf", TFChange);
    }

    private void Update()
    {
        _compensationFrame.transform.localPosition = new Vector3(UnityPosition.x, 0.0f, UnityPosition.z);
        _compensationFrame.transform.localRotation = UnityRotation;
    }

    void TFChange(RosTFMessage TF)
    {
        int tflength = TF.transforms.Length;

        for (int i = 0; i < tflength; i++)
        {
            if (TF.transforms[i].header.frame_id == "map" && TF.transforms[i].child_frame_id == "odom")
            {
                UnityPosition = new Vector3(-(float)TF.transforms[i].transform.translation.y, (float)TF.transforms[i].transform.translation.z, (float)TF.transforms[i].transform.translation.x);
                UnityRotation = new Quaternion((float)TF.transforms[i].transform.rotation.x, (float)TF.transforms[i].transform.rotation.y, (float)TF.transforms[i].transform.rotation.z, (float)TF.transforms[i].transform.rotation.w);
                UnityRotation.eulerAngles = new Vector3(0.0f, -UnityRotation.eulerAngles.z, 0.0f);
            }
        }
    }
}
