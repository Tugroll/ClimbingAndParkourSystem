using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Transform targetPosition;
    public float distance;

    public float rotationY;
    public float rotationX;

    public Vector3 framingOffset;
    

    public float maxVerticalVal = 45;
    public float minVerticalVal = -45;


    public bool invertX;
    public bool invertY;

    public float invertXval;
    public float invertYval;

    void Update()
    {
        invertXval = (invertX) ? -1 : 1;
        invertYval = (invertY) ? -1 : 1;

        rotationX += Input.GetAxis("Mouse Y") * invertYval;
        rotationY += Input.GetAxis("Mouse X") * invertXval;

        rotationX = Mathf.Clamp(rotationX, minVerticalVal, maxVerticalVal);

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        var focusPosition = targetPosition.position + new Vector3(framingOffset.x, framingOffset.y);

        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);

        transform.rotation = targetRotation;


    }

    public Quaternion PlanarRotation() => Quaternion.Euler(0, rotationY, 0);
}
