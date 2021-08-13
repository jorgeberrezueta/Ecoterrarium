using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(0.1f,2.0f)]
    public float Velocidad = 1.0f;

    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal") * Velocidad * 0.075f;
        float zAxisValue = Input.GetAxis("Vertical") * Velocidad * 0.075f;
        float yValue = Velocidad * 5 * Input.mouseScrollDelta.y * -0.075f;

        transform.position = new Vector3(transform.position.x + xAxisValue, transform.position.y + yValue, transform.position.z + zAxisValue);
    }
}
