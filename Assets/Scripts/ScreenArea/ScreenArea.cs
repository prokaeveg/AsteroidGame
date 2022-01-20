using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenArea : MonoBehaviour
{
    public static Vector3 areaSize;
    public static Vector3 halfAreaSize;

    public float bufferArea;

    public void Update()
    {
        CalculateArea();
    }

    public void CalculateArea()
    {
        Camera cam = Camera.main;
        Transform cameraTransform = cam.transform;

        var distance = -cameraTransform.position.z;
        var halfAngle = cam.fieldOfView * 0.5f;
        var height = distance * Mathf.Tan(halfAngle * Mathf.Deg2Rad) * 2;
        var width = height * cam.aspect;
        areaSize = new Vector3(width + bufferArea, height + bufferArea, 20);
        halfAreaSize = areaSize * 0.5f;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, areaSize);
    }
}
