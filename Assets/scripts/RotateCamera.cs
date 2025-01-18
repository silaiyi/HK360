using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 2.0f;
    private float xRotation = 0.0f;
    private float yRotation = -70.0f;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");//Horizontal

        // 控制相機左右旋轉，不限制範圍
        xRotation += x * rotationSpeed;

        // 控制相機上下旋轉，限制在 0到 180 度之間
        yRotation = Mathf.Clamp(yRotation + y * rotationSpeed, -90f, 90f);

        Quaternion rotation = Quaternion.Euler(-yRotation, xRotation, 0);
        transform.rotation = rotation;
    }
}