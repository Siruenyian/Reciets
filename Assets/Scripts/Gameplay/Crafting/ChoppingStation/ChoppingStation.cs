using System.Threading;
using Cinemachine;
using reciets;
using UnityEngine;

public class ChoppingStation : MonoBehaviour, Iinteractable
{
    private CharacterInteraction characterI = null;
    [SerializeField] private Canvas canvas;
    [SerializeField] private CinemachineVirtualCamera transitionCam = null;
    [SerializeField] private Skinner skinner;
    [SerializeField] private Cutter cutter;

    private LayerMask layersToDisable;
    private void Start()
    {

        layersToDisable = 1 << LayerMask.NameToLayer("Player");
        canvas.gameObject.SetActive(false);
        transitionCam.gameObject.SetActive(false);
    }
    public void Interact(CharacterInteraction characterInteraction)
    {
        OpenOverlay();
    }
    public void OpenOverlay()
    {
        canvas.gameObject.SetActive(true);
        characterI.FreezeMovement();
        Camera.main.cullingMask &= ~layersToDisable;
        transitionCam.gameObject.SetActive(true);
    }

    public void CloseOverlay()
    {
        canvas.gameObject.SetActive(false);
        characterI.UnfreezeMovement();
        Camera.main.cullingMask |= layersToDisable;
        transitionCam.gameObject.SetActive(false);
    }


    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out CharacterInteraction characterInteraction))
        {
            characterInteraction.Interactable = this;
            characterI = characterInteraction;
            skinner.CharacterI = characterI;
            cutter.CharacterI = characterI;

        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out CharacterInteraction characterInteraction))
        {
            characterInteraction.Interactable = null;
            characterI = null;
            skinner.CharacterI = characterI;
            cutter.CharacterI = characterI;
        }
    }
}