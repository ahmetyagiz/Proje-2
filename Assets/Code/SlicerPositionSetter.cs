using UnityEngine;

/// <summary>
/// Bu kod b��aklar�n konumlar�n�n sonraki platformun kenarlar�na eklenmesini sa�lar.
/// </summary>
public class SlicerPositionSetter : MonoBehaviour
{
    [SerializeField] private Transform leftSlicer;
    [SerializeField] private Transform rightSlicer;

    public void SetSlicerPositions(Transform slicedObject)
    {
        // Nesnenin en sol ve sa� u� noktalar�n� bulmak i�in boyutlar�n� kullan�yoruz
        Bounds bounds = slicedObject.GetComponent<Collider>().bounds;
        Vector3 leftPosition = new Vector3(bounds.min.x, slicedObject.position.y, slicedObject.position.z);
        Vector3 rightPosition = new Vector3(bounds.max.x, slicedObject.position.y, slicedObject.position.z);

        // Sol b��a��n konumunu ayarla
        leftSlicer.parent = slicedObject;
        leftSlicer.localPosition = slicedObject.InverseTransformPoint(leftPosition);

        // Sa� b��a��n konumunu ayarla
        rightSlicer.parent = slicedObject;
        rightSlicer.localPosition = slicedObject.InverseTransformPoint(rightPosition);

        leftSlicer.parent = null;
        rightSlicer.parent = null;
    }
}