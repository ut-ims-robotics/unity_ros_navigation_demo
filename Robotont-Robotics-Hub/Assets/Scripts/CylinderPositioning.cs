using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderPositioning : MonoBehaviour
{

    [SerializeField]
    private GameObject _cylinder;
    [SerializeField]
    private float _yCoordinate = 0.2f;

    private Quaternion _intermidiateRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _cylinder.transform.localPosition = new Vector3(_cylinder.transform.localPosition.x, _yCoordinate, _cylinder.transform.localPosition.z);
        //_cylinder.transform.localRotation = Quaternion.identity; //x and z in euler rotation should be zero

        _intermidiateRotation.eulerAngles = new Vector3(0.0f, _cylinder.transform.localRotation.eulerAngles.y, 0.0f);

        _cylinder.transform.localRotation = _intermidiateRotation;
    }
}
