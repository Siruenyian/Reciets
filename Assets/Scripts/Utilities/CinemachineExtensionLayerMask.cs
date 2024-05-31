using Cinemachine;
using UnityEngine;

public class CinemachineExtensionLayerMask : CinemachineExtension
{
    [SerializeField]
    private LayerMask _layers;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        // bitwise black magic
        Camera.main.cullingMask &= ~_layers;
    }
}
