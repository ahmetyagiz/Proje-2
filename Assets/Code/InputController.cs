using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    [SerializeField] private MeshSlicer leftSlicer;
    [SerializeField] private MeshSlicer rightSlicer;

    void Update()
    {
        InputControl();
    }

    void InputControl()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) // Ekrana týklama
        {
            // Þu andaki platformu al ve durdur
            PlatformInformationManager._instance.GetCurrentPlatformController().StopPlatform();

            // Sol ve sað býçak ile kesim yap
            leftSlicer.SliceObject();
            rightSlicer.SliceObject();

            // Sonraki platformu baþlat
            PlatformTransferManager._instance.StartNextPlatform();
        }
    }
}