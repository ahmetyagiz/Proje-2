using UnityEngine;

public class SlicerPositionSetter : MonoBehaviour
{
    // 2 farkl� b��a��n ortak bilgileri i�in bu script kullan�l�r

    [HideInInspector] public Transform leftSlicer;
    [HideInInspector] public Transform rightSlicer;
    [HideInInspector] public Mesh lowerHullMesh;
    [HideInInspector] public Vector3 boxColliderCenter;
    [HideInInspector] public Vector3 boxColliderSize;
    [HideInInspector] public Vector3 innerHullCenter;
}