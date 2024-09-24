using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlatformManager platformManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Ekrana týklama (mobilde dokunma)
        {
            platformManager.GetCurrentPlatform().StopPlatform();
            platformManager.StartNextPlatform();
            // Kesme iþlemi burada baþlatýlabilir
        }
    }
}