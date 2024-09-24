using UnityEngine;
using UnityEngine.Events;

public class PlatformManager : MonoBehaviour
{
    private GameObject[] Platforms;
    private Transform platformParent;
    private int platformIndex;

    [SerializeField] MeshSlicer meshSlicer; // Zenject ile ele alýnacak
    [SerializeField] SlicerPositionSetter slicerPositionSetter;

    [HideInInspector] public UnityEvent platformChangeEvent;

    private void Start()
    {
        platformParent = GameObject.FindGameObjectWithTag("PlatformParent").transform;
        AddAllPlatformsToArray();

        // Ýlk platform açýlýr
        Platforms[0].SetActive(true);
    }

    private void AddAllPlatformsToArray()
    {
        Platforms = new GameObject[platformParent.childCount];

        for (int i = 0; i < platformParent.childCount; i++)
        {
            Platforms[i] = platformParent.GetChild(i).gameObject;
            Platforms[i].SetActive(false);
        }
    }

    public PlatformController GetCurrentPlatform()
    {
        return Platforms[platformIndex].GetComponent<PlatformController>();
    }

    private int IncreasePlatformIndex()
    {
        return ++platformIndex;
    }

    public void StartNextPlatform()
    {
        if (platformIndex == Platforms.Length - 1)
        {
            return;
        }


        Platforms[IncreasePlatformIndex()].SetActive(true);
        Platforms[platformIndex].GetComponent<MeshFilter>().mesh = slicerPositionSetter.lowerHullMesh;
        Platforms[platformIndex].GetComponent<BoxCollider>().center = slicerPositionSetter.boxColliderCenter;
        Platforms[platformIndex].GetComponent<BoxCollider>().size = slicerPositionSetter.boxColliderSize;

        platformChangeEvent.Invoke();
    }
}