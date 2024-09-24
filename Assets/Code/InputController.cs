using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlatformManager platformManager; // Bu k�s�m zenject ile yap�lacak

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