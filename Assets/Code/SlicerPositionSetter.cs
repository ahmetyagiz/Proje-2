using UnityEngine;

/// <summary>
/// Bu kod býçaklarýn konumlarýnýn sonraki platformun kenarlarýna eklenmesini saðlar.
/// </summary>
public class SlicerPositionSetter : MonoBehaviour
{
    [SerializeField] private Transform leftSlicer;
    [SerializeField] private Transform rightSlicer;

    public void SetSlicerPositions(Transform slicedObject)
    {
        // Nesnenin en sol ve sað uç noktalarýný bulmak için boyutlarýný kullanýyoruz
        Bounds bounds = slicedObject.GetComponent<Collider>().bounds;
        Vector3 leftPosition = new Vector3(bounds.min.x, slicedObject.position.y, slicedObject.position.z);
        Vector3 rightPosition = new Vector3(bounds.max.x, slicedObject.position.y, slicedObject.position.z);

        // Sol býçaðýn konumunu ayarla
        leftSlicer.parent = slicedObject;
        leftSlicer.localPosition = slicedObject.InverseTransformPoint(leftPosition);

        // Sað býçaðýn konumunu ayarla
        rightSlicer.parent = slicedObject;
        rightSlicer.localPosition = slicedObject.InverseTransformPoint(rightPosition);

        leftSlicer.parent = null;
        rightSlicer.parent = null;
    }
}