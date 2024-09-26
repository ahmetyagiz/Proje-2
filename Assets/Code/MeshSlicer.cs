using UnityEngine;
using EzySlice;
using Zenject;

/// <summary>
/// Bu kod b��a��n meshi kesmesini y�netir
/// </summary>
public class MeshSlicer : MonoBehaviour
{
    private GameObject objectToSlice; // Kesilecek nesne
    [SerializeField] private float fallForce = 5f; // D��me kuvveti
    [SerializeField] private Material platformMat; // Kesilen platformun materyali
    [SerializeField] private SlicerPositionSetter slicerPositionSetter;

    private PlatformTransferManager _platformTransferManager;

    [Inject]
    public void Construct(PlatformTransferManager platformTransferManager)
    {
        _platformTransferManager = platformTransferManager;
    }

    #region OnTriggerEnter, OnTriggerExit
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            objectToSlice = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            objectToSlice = null;
        }
    }
    #endregion

    public void SliceObject()
    {
        if (objectToSlice == null)
        {
            return;
        }
        
        // Nesneyi kesmeden �nce ana parent'tan ay�r
        objectToSlice.transform.parent = null;

        // Kesim i�lemi
        SlicedHull cuttingObject = Cut(objectToSlice.GetComponent<Collider>().gameObject, platformMat);
        GameObject kesilmisOuterHull = cuttingObject.CreateOuterHull(objectToSlice, platformMat);
        GameObject kesilmisInnerHull = cuttingObject.CreateInnerHull(objectToSlice, platformMat);

        AddComponents(kesilmisOuterHull); // D��ta kalan, kesilen, a�a��ya d��en platform
        kesilmisInnerHull.AddComponent<BoxCollider>(); // ��eride kalan, sabitlenen platform

        // Orijinal objeyi yok et
        Destroy(objectToSlice.gameObject);

        // Sol ve sa� b��aklar� kesilen par�an�n en soluna ve en sa��na ekle
        slicerPositionSetter.SetSlicerPositions(kesilmisInnerHull.transform);

        // Bir sonraki platforma eklenmesi i�in transfer bilgilerini kaydet
        SaveTransferInformations(kesilmisInnerHull);
    }

    public void SaveTransferInformations(GameObject kesilmisInnerHull)
    {
        // Kesilen objenin pivotu bozuldugu icin pivotu tekrar hesaplan�r. Karakter platformun merkez pivotuna gitmelidir.
        Bounds bounds = kesilmisInnerHull.GetComponent<Collider>().bounds;
        _platformTransferManager.innerHullCenter = bounds.center;

        // Collider ve meshi bir sonraki platforma eklemek icin kaydet
        _platformTransferManager.SetMesh(kesilmisInnerHull.GetComponent<MeshFilter>().mesh);
        _platformTransferManager.SetBoxColliderSize(kesilmisInnerHull.GetComponent<BoxCollider>().size);
        _platformTransferManager.SetBoxColliderCenter(kesilmisInnerHull.GetComponent<BoxCollider>().center);
    }

    public SlicedHull Cut(GameObject obj, Material mat = null)
    {
        return obj.Slice(transform.position, transform.up, mat);
    }

    void AddComponents(GameObject obj)
    {
        obj.AddComponent<BoxCollider>();
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().AddForce(Vector3.down * fallForce, ForceMode.Impulse);
    }
}