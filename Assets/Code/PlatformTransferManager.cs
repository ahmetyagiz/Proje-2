using UnityEngine;
using UnityEngine.Events;
using Zenject;

/// <summary>
/// Bu kod platformlar arasý verilerin transferini saðlar
/// </summary>
public class PlatformTransferManager : MonoBehaviour
{
    [Header("Current Mesh Transfer Informations")]
    [SerializeField] private Mesh innerHullMesh;

    private Vector3 boxColliderSize;
    private Vector3 boxColliderCenter;

    public Vector3 innerHullCenter;
    private Vector3 previousInnerHullCenter;

    [HideInInspector] public UnityEvent platformChangeEvent;

    private GameManager _gameManager;
    private ComboSoundManager _comboSoundManager;
    private PlatformInformationManager _platformInformationManager;

    [Inject]
    public void Construct(GameManager gameManager, ComboSoundManager comboSoundManager, PlatformInformationManager platformInformationManager)
    {
        _gameManager = gameManager;
        _comboSoundManager = comboSoundManager;
        _platformInformationManager = platformInformationManager;
    }

    #region Start

    private void Start()
    {
        InitializeTransferValues();
    }

    void InitializeTransferValues()
    {
        innerHullCenter = Vector3.zero;
        previousInnerHullCenter = Vector3.zero;
        boxColliderCenter = Vector3.zero;
        boxColliderSize = Vector3.one;
    }

    #endregion

    public void SetMesh(Mesh newMesh)
    {
        innerHullMesh = newMesh;
    }

    public void SetBoxColliderSize(Vector3 newBoxColliderSize)
    {
        boxColliderSize = newBoxColliderSize;
    }

    public void SetBoxColliderCenter(Vector3 newBoxColliderCenter)
    {
        boxColliderCenter = newBoxColliderCenter;
    }

    public void StartNextPlatform()
    {
        ThresholdHandler();

        if (!_platformInformationManager.IsArrayCompleted())
        {
            // platformIndex'i 1 arttýr
            _platformInformationManager.IncreasePlatformIndex();
            _platformInformationManager.GetCurrentPlatform().SetActive(true);
            previousInnerHullCenter = innerHullCenter;
            SetMeshAndColliders();
            platformChangeEvent.Invoke();
        }
    }

    void SetMeshAndColliders()
    {
        _platformInformationManager.GetCurrentPlatform().GetComponent<MeshFilter>().mesh = innerHullMesh;
        _platformInformationManager.GetCurrentPlatform().GetComponent<BoxCollider>().center = boxColliderCenter;
        _platformInformationManager.GetCurrentPlatform().GetComponent<BoxCollider>().size = boxColliderSize;
    }

    bool IsPlatformFit()
    {
        // Debug.Log(previousInnerHullCenter.x + " " + innerHullCenter.x);
        return (Mathf.Abs(previousInnerHullCenter.x - innerHullCenter.x) < 0.45f);
    }

    public void ThresholdHandler()
    {
        if (IsPlatformFit())
        {
            _comboSoundManager.PlayNote();
        }
        else
        {
            _comboSoundManager.SetPitchToDefault();
        }
    }
}