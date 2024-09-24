using UnityEngine;

public class SlicerPositionSetter : MonoBehaviour
{
    // 2 farklý býçaðýn ortak bilgileri için bu script kullanýlýr

    [HideInInspector] public Transform leftSlicer;
    [HideInInspector] public Transform rightSlicer;
    [HideInInspector] public Mesh lowerHullMesh;
    [HideInInspector] public Vector3 boxColliderCenter;
    [HideInInspector] public Vector3 boxColliderSize;
    [HideInInspector] public Vector3 innerHullCenter;
}