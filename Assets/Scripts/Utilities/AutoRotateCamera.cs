using UnityEngine;
using Cinemachine;

public class AutoRotateCamera : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float rotationSpeed = 10f;

    void Update()
    {
        if (freeLookCamera != null)
        {
            // Rotate the camera around the target
            freeLookCamera.m_XAxis.Value += rotationSpeed * Time.deltaTime;
        }
    }
}