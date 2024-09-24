using UnityEngine;

public class SlicerPositionSetter : MonoBehaviour
{
    [HideInInspector] public Transform leftSlicer;
    [HideInInspector] public Transform rightSlicer;
    [HideInInspector] public Mesh lowerHullMesh;
    [HideInInspector] public Vector3 boxColliderCenter;
    [HideInInspector] public Vector3 boxColliderSize;
}