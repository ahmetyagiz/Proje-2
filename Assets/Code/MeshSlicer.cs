using UnityEngine;
using EzySlice;
using UnityEngine.Android;

public class MeshSlicer : MonoBehaviour
{
    private GameObject objectToSlice; // Kesilecek nesne
    [SerializeField] private float fallForce = 5f; // Düþme kuvveti
    [SerializeField] private Material platformMat; // Kesilen platformun materyali
    [SerializeField] SlicerPositionSetter slicerPositionSetter; // Zenject ile yapýlacak

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Týklayýnca kes
        {
            SliceObject();
        }
    }

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

    void SliceObject()
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

        BilesenEkle(kesilmisOuterHull); // Dýþta kalan, kesilen platform
        kesilmisInnerHull.AddComponent<BoxCollider>(); // Ýçeride kalan platform

        // Orijinal objeyi yok et
        Destroy(objectToSlice.gameObject);

        // Sol ve sað býçaklarý kesilen parçanýn en soluna ve en saðýna ekle
        SetSlicerPositions(kesilmisInnerHull.transform);

        // Collider ve meshi bir sonraki platforma ekle
        slicerPositionSetter.boxColliderCenter = kesilmisInnerHull.GetComponent<BoxCollider>().center;
        slicerPositionSetter.boxColliderSize = kesilmisInnerHull.GetComponent<BoxCollider>().size;
        slicerPositionSetter.lowerHullMesh = kesilmisInnerHull.GetComponent<MeshFilter>().mesh;

        // Kesilen objenin pivotu bozuldugu icin pivotunu tekrar hesaplanýr. Karakter platformun merkez pivotuna gitmelidir.
        Bounds bounds = kesilmisInnerHull.GetComponent<Collider>().bounds;
        slicerPositionSetter.innerHullCenter = bounds.center;
    }

    public SlicedHull Cut(GameObject obj, Material mat = null)
    {
        return obj.Slice(transform.position, transform.up, mat);
    }

    void BilesenEkle(GameObject obj)
    {
        obj.AddComponent<BoxCollider>();
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().AddForce(Vector3.down * fallForce, ForceMode.Impulse);
    }

    void SetSlicerPositions(Transform slicedObject)
    {
        // Nesnenin en sol ve sað uç noktalarýný bulmak için boyutlarýný kullanýyoruz
        Bounds bounds = slicedObject.GetComponent<Collider>().bounds;
        Vector3 leftPosition = new Vector3(bounds.min.x, slicedObject.position.y, slicedObject.position.z);
        Vector3 rightPosition = new Vector3(bounds.max.x, slicedObject.position.y, slicedObject.position.z);

        // Sol býçaðýn konumunu ayarla
        slicerPositionSetter.leftSlicer.parent = slicedObject.transform;
        slicerPositionSetter.leftSlicer.localPosition = slicedObject.InverseTransformPoint(leftPosition);

        // Sað býçaðýn konumunu ayarla
        slicerPositionSetter.rightSlicer.parent = slicedObject;
        slicerPositionSetter.rightSlicer.localPosition = slicedObject.InverseTransformPoint(rightPosition);
    }
}