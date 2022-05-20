using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosTFMessage = RosMessageTypes.Tf2.TFMessageMsg;

public class qr_code_tf_subscriber : MonoBehaviour
{
    [SerializeField]
    private GameObject _mapFrame;

    private Vector3 UnityPosition;
    private Quaternion UnityRotation;

    public string TF_frame = "anchor";

    private GameObject[] QR_texts;
    private GameObject QR_text;


    // Start is called before the first frame update
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<RosTFMessage>("/tf", TFChange);
        UnityPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _mapFrame.transform.localPosition = new Vector3(-UnityPosition.x, -UnityPosition.y, -UnityPosition.z);
        _mapFrame.transform.localRotation = UnityRotation;
        QR_texts = GameObject.FindGameObjectsWithTag("qrtext");
        foreach (GameObject QR_text in QR_texts)
        {
            TF_frame = QR_text.gameObject.GetComponent<TextMesh>().text;
        }
    }

    void TFChange(RosTFMessage TF)
    {
        int tflength = TF.transforms.Length;

        for (int i = 0; i < tflength; i++)
        {
            if (TF.transforms[i].header.frame_id == "map" && TF.transforms[i].child_frame_id == TF_frame)
            {
                UnityPosition = new Vector3(-(float)TF.transforms[i].transform.translation.y, (float)TF.transforms[i].transform.translation.z, (float)TF.transforms[i].transform.translation.x);
                UnityRotation = new Quaternion((float)TF.transforms[i].transform.rotation.x, (float)TF.transforms[i].transform.rotation.y, (float)TF.transforms[i].transform.rotation.z, (float)TF.transforms[i].transform.rotation.w);
                UnityRotation.eulerAngles = new Vector3(0.0f, -UnityRotation.eulerAngles.z, 0.0f);
                UnityRotation.eulerAngles = new Vector3(-UnityRotation.eulerAngles.x, -UnityRotation.eulerAngles.y, -UnityRotation.eulerAngles.z);
            }
        }
    }
}
