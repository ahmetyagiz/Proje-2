using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlatformManager platformManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Ekrana t�klama (mobilde dokunma)
        {
            platformManager.GetCurrentPlatform().StopPlatform();
            platformManager.StartNextPlatform();
            // Kesme i�lemi burada ba�lat�labilir
        }
    }
}