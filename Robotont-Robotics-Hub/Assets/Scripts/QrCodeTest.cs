using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.QR;
using System;

public class QrCodeTest : MonoBehaviour
{
    struct QRData
    {
        public Pose pose;
        public float size;
        public string text;
        
    }

    QRCodeWatcher watcher;
    DateTime watcherStart;

    // Start is called before the first frame update
    void Start()
    {
        var status = QRCodeWatcher.RequestAccessAsync().Result;
        if (status != QRCodeWatcherAccessStatus.Allowed)
            return;
        watcherStart = DateTime.Now;
        watcher = new QRCodeWatcher();
        //watcher.a


        // Update is called once per frame
        void Update()
        {

        }
    }
}
