using UnityEngine;
using UnityEngine.Events;

public class PlatformTransferManager : MonoBehaviour
{
    [Header("Current Mesh Transfer Informations")]
    [SerializeField] private Mesh innerHullMesh;

    private Vector3 boxColliderSize;
    private Vector3 boxColliderCenter;

    public Vector3 innerHullCenter;
    private Vector3 previousInnerHullCenter;

    [HideInInspector] public UnityEvent platformChangeEvent;

    public static PlatformTransferManager _instance;

    #region Awake, Start
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Tekil örneði bu bileþen olarak ayarla
        _instance = this;
    }

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

        if (!PlatformInformationManager._instance.IsArrayCompleted())
        {
            // platformIndex'i 1 arttýr
            PlatformInformationManager._instance.IncreasePlatformIndex();
            PlatformInformationManager._instance.GetCurrentPlatform().SetActive(true);
            previousInnerHullCenter = innerHullCenter;
            SetMeshAndColliders();
            platformChangeEvent.Invoke();
        }
    }

    void SetMeshAndColliders()
    {
        PlatformInformationManager._instance.GetCurrentPlatform().GetComponent<MeshFilter>().mesh = innerHullMesh;
        PlatformInformationManager._instance.GetCurrentPlatform().GetComponent<BoxCollider>().center = boxColliderCenter;
        PlatformInformationManager._instance.GetCurrentPlatform().GetComponent<BoxCollider>().size = boxColliderSize;
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
            ComboSoundManager._instance.PlayNote();
        }
        else
        {
            ComboSoundManager._instance.SetPitchToDefault();
        }
    }
}