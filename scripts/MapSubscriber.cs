/*
MapSubscriber
Script to display ROS nav_msgs/OccupancyGrid map on Unity 2D plane.
*/

using System;
using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.Nav;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class MapSubscriber : UnitySubscriber<MessageTypes.Nav.OccupancyGrid>
    {
        //plane for the image to appear to
        public MeshRenderer meshRenderer;

        private Texture2D texture2D;
        private bool isMessageReceived;
        private MapMetaData info { get; set; }

        //the Occupancygrid uses signed 8bit values, this is the best data type natively available here.
        //Different mapping algorhitms from the one used during testing may return a different data type which is not compatible.
        private sbyte[] data { get; set; }
        
        private float scalex,scalez;
        protected override void Start()
        {
			base.Start();
            //texture format is R8 as it can be used with OccupancyGrid data.
            texture2D = new Texture2D(1, 1, TextureFormat.R8, false);
            texture2D.filterMode=FilterMode.Point;
            meshRenderer.material = new Material(Shader.Find("Standard"));
        }
        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Nav.OccupancyGrid occupancyGrid)
        {
            data = occupancyGrid.data;
            info = occupancyGrid.info;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            //calculate size of each pixel of image
            scalex=info.width/10*info.resolution;
            scalez=info.height/10*info.resolution;

            meshRenderer.transform.localPosition = getLocalPositionFromROS();

            if ((info.width!=0) & (info.height!=0)) {
                meshRenderer.transform.localScale=new Vector3(scalex,1,scalez);
            }

            texture2D.Resize((int)info.width, (int)info.height);//new map received may differ in size from current
            texture2D.LoadRawTextureData((byte[])(Array)data);
            texture2D.Apply();
            meshRenderer.material.SetTexture("_MainTex", texture2D);
            isMessageReceived = false;
        }
        private Vector3 getLocalPositionFromROS()
        {
            float ROSx = (float)info.width/2 * (float)info.resolution*(float)0.9;
            float ROSy = (float)info.height/2 * (float)info.resolution*(float)0.9;
            float ROSz = 0; //2d mapping, height constant 0

            //offset the map by the origin position (Unity coords are at the center of the image, ROS coords at one of the corners), the 0.9 comes from the mapping algorhitm which adds a buffer to the image sides.
            ROSx += (float)info.origin.position.x * (float)0.9;
            ROSy += (float)info.origin.position.y * (float)0.9;
            
            return new Vector3(-ROSy, ROSz, ROSx);
        }
    }

}