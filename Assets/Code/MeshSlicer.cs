using UnityEngine;
using EzySlice;

/// <summary>
/// Bu kod býçaðýn meshi kesmesini yönetir
/// </summary>
public class MeshSlicer : MonoBehaviour
{
    private GameObject objectToSlice; // Kesilecek nesne
    [SerializeField] private float fallForce = 5f; // Düþme kuvveti
    [SerializeField] private Material platformMat; // Kesilen platformun materyali
    [SerializeField] private SlicerPositionSetter slicerPositionSetter;

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
        
        // Nesneyi kesmeden önce ana parent'tan ayýr
        objectToSlice.transform.parent = null;

        // Kesim iþlemi
        SlicedHull cuttingObject = Cut(objectToSlice.GetComponent<Collider>().gameObject, platformMat);
        GameObject kesilmisOuterHull = cuttingObject.CreateOuterHull(objectToSlice, platformMat);
        GameObject kesilmisInnerHull = cuttingObject.CreateInnerHull(objectToSlice, platformMat);

        AddComponents(kesilmisOuterHull); // Dýþta kalan, kesilen, aþaðýya düþen platform
        kesilmisInnerHull.AddComponent<BoxCollider>(); // Ýçeride kalan, sabitlenen platform

        // Orijinal objeyi yok et
        Destroy(objectToSlice.gameObject);

        // Sol ve sað býçaklarý kesilen parçanýn en soluna ve en saðýna ekle
        slicerPositionSetter.SetSlicerPositions(kesilmisInnerHull.transform);

        // Bir sonraki platforma eklenmesi için transfer bilgilerini kaydet
        SaveTransferInformations(kesilmisInnerHull);
    }

    public void SaveTransferInformations(GameObject kesilmisInnerHull)
    {
        // Kesilen objenin pivotu bozuldugu icin pivotu tekrar hesaplanýr. Karakter platformun merkez pivotuna gitmelidir.
        Bounds bounds = kesilmisInnerHull.GetComponent<Collider>().bounds;
        PlatformTransferManager._instance.innerHullCenter = bounds.center;

        // Collider ve meshi bir sonraki platforma eklemek icin kaydet
        PlatformTransferManager._instance.SetMesh(kesilmisInnerHull.GetComponent<MeshFilter>().mesh);
        PlatformTransferManager._instance.SetBoxColliderSize(kesilmisInnerHull.GetComponent<BoxCollider>().size);
        PlatformTransferManager._instance.SetBoxColliderCenter(kesilmisInnerHull.GetComponent<BoxCollider>().center);
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