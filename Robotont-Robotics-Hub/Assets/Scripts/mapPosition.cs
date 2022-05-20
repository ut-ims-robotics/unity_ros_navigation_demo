using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosTFMessage = RosMessageTypes.Tf2.TFMessageMsg;

public class mapPosition : MonoBehaviour
{

    [SerializeField]
    private GameObject _mapFrame;
    private Vector3 UnityPosition;
    private Quaternion UnityRotation;

    public Transform map_tf;
    // Start is called before the first frame update
    void Start()
    {
        //ROSConnection.GetOrCreateInstance().Subscribe<RosTFMessage>("/tf", TFChange);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = map_tf.position;

    }
}
