/*
ApplyScaledTransform
Script to apply scaling but not modify existing coordinates.
*/

using UnityEngine;

public class applyScaledTransform : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform applyScalingTo;
    public Transform getScalingFrom;
    private Vector3 setPosition;
    void Start()
    {
        setPosition = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {

        setPosition.x = this.transform.position.x * getScalingFrom.localScale.x;
        setPosition.y = this.transform.position.y * getScalingFrom.localScale.y;
        setPosition.z = this.transform.position.z * getScalingFrom.localScale.z;

        applyScalingTo.position = setPosition;
        applyScalingTo.rotation = this.transform.rotation;
    }
}
