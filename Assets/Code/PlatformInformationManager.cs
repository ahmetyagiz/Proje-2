using UnityEngine;

/// <summary>
/// Bu kod platformlar�n bilgilerini tutar. Platform dizisi, platform indexi gibi.
/// </summary>
public class PlatformInformationManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Platforms;
    [SerializeField] private int platformIndex;

    public static PlatformInformationManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Tekil �rne�i bu bile�en olarak ayarla
        _instance = this;
    }

    private void Start()
    {
        DisableAllPlatforms();

        // �lk platform a��l�r
        Platforms[0].SetActive(true);
    }

    private void DisableAllPlatforms()
    {
        foreach (GameObject platform in Platforms)
        {
            platform.SetActive(false);
        }
    }

    public GameObject GetCurrentPlatform()
    {
        return Platforms[platformIndex];
    }

    public PlatformController GetCurrentPlatformController()
    {
        return Platforms[platformIndex].GetComponent<PlatformController>();
    }

    public void IncreasePlatformIndex()
    {
        platformIndex++;
    }

    public bool IsArrayCompleted()
    {
        if (platformIndex == Platforms.Length - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}