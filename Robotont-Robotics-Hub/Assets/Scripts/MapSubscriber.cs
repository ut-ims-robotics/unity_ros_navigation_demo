using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMap = RosMessageTypes.Nav.OccupancyGridMsg;
using RosMapMetaData = RosMessageTypes.Nav.MapMetaDataMsg;

public class MapSubscriber : MonoBehaviour
{
    // Start is called before the first frame update
    private Texture2D _recievedMap;
    private RosMapMetaData _mapInfo;

    private MeshRenderer meshRenderer;

    private GameObject MapMesh;
    private Mesh mesh;
    private Vector3[] meshVerticies;
    private int[] meshTriangles;
    Transform _mapOrigin;

    private sbyte[] _mapData;

    private Vector2[] uvs;

    //[SerializeField]
    //private GameObject _scaleFrom;

    private bool _isMessageRecieved = false;
    private bool _isCreated = false;

    RosUnityConversion _conversion;

    void Start()
    {
        MapMesh = new GameObject("MapMesh");
        MapMesh.transform.parent = GameObject.Find("map").transform;

        MapMesh.AddComponent<MeshFilter>();
        meshRenderer = MapMesh.AddComponent<MeshRenderer>();


        mesh = MapMesh.GetComponent<MeshFilter>().mesh;
        meshVerticies = new Vector3[4];
        //meshVerticies[0] = new Vector3(0f, 0f, 0f);
        //meshVerticies[1] = new Vector3(0f, 0f, 0f);
        //meshVerticies[2] = new Vector3(0f, 0f, 0f);
        //meshVerticies[3] = new Vector3(0f, 0f, 0f);
        meshTriangles = new int[6] { 0, 1, 2, 2, 1, 3 };//order of verticies in triangle
        mesh.vertices = meshVerticies;
        mesh.triangles = meshTriangles;


        _recievedMap = new Texture2D(1, 1, TextureFormat.R8, false);
        _recievedMap.filterMode = FilterMode.Point;

        meshRenderer.material = new Material(Shader.Find("Standard"));
        //meshRenderer.material.SetTexture("_mainTex", _recievedMap);

        _isCreated = true;

        ROSConnection.GetOrCreateInstance().Subscribe<RosMap>("/map", MapChange);
    }

    void MapChange(RosMap mapMsg)
    {
        _mapData = mapMsg.data;
        _mapInfo = mapMsg.info;

        float sizez = _mapInfo.width * _mapInfo.resolution;
        float sizex = _mapInfo.height * _mapInfo.resolution;

        Vector3 OriginUnity = new Vector3(-(float)_mapInfo.origin.position.y, (float)_mapInfo.origin.position.z, (float)_mapInfo.origin.position.x);

        //_mapOrigin.position = _conversion.ros2unityCoord(_mapInfo.origin.position);
        //_mapOrigin.rotation = _conversion.ros2unityOrientation(_mapInfo.origin.orientation);

        meshVerticies[0] = new Vector3(0f, 0f, 0f);
        meshVerticies[1] = new Vector3(-sizex, 0f, 0f);
        meshVerticies[2] = new Vector3(0f, 0f, sizez);
        meshVerticies[3] = new Vector3(-sizex, 0f, sizez);

        mesh.vertices = meshVerticies;

        _recievedMap.Resize(((int)_mapInfo.width), ((int)_mapInfo.height));
        _recievedMap.LoadRawTextureData((byte[])(System.Array)_mapData);
        _recievedMap.Apply();

        meshRenderer.material.SetTexture("_MainTex", _recievedMap);
        mesh.RecalculateBounds();

        uvs = new Vector2[4];
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(0, 1);
        uvs[2] = new Vector2(1, 0);
        uvs[3] = new Vector2(1, 1);

        mesh.uv = uvs;

        Vector3[] normals = mesh.normals;

        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = Vector3.up;
        }

        mesh.normals = normals;

        MapMesh.transform.localPosition = OriginUnity;
        MapMesh.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    //TODO
    /*
     * add functions to convert the ROScoords to Unity_coords and vice versa
     */
    //seems like not an option
}


