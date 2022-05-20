using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qrPosition : MonoBehaviour
{
    private GameObject[] QR_codes;
    private GameObject QR_code;

    // Start is called before the first frame update
    void Start()
    {
        QR_codes = GameObject.FindGameObjectsWithTag("qrcode");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject QR_code in QR_codes)
        {
            QR_code.transform.position = gameObject.transform.position;
            QR_code.transform.rotation = gameObject.transform.rotation;

        }
    }
}
