using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

/// <summary>
/// Bu kod input verilmesini yönetir.
/// </summary>
public class InputController : MonoBehaviour
{
    [Header("Slicers")]
    [SerializeField] private MeshSlicer leftSlicer;
    [SerializeField] private MeshSlicer rightSlicer;

    private PlatformInformationManager _platformInformationManager;
    private PlatformTransferManager _platformTransferManager;

    [Inject]
    public void Construct(PlatformTransferManager platformTransferManager, PlatformInformationManager platformInformationManager)
    {
        _platformTransferManager = platformTransferManager;
        _platformInformationManager = platformInformationManager;
    }

    void Update()
    {
        InputControl();
    }

    void InputControl()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) // Ekrana týklama
        {
            // Þu andaki platformu al ve durdur
            _platformInformationManager.GetCurrentPlatformController().StopPlatform();

            // Sol ve sað býçak ile kesim yap
            leftSlicer.SliceObject();
            rightSlicer.SliceObject();

            // Sonraki platformu baþlat
            _platformTransferManager.StartNextPlatform();
        }
    }
}