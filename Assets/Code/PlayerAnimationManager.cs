using UnityEngine;

/// <summary>
/// Bu kod karakterin animasyonlarýný yönetir.
/// </summary>
public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinTrigger"))
        {
            // Seviye tamamlandý
            GameManager._instance.SetLevelCompleted();
            PlayWinAnimation();
        }
    }

    private void PlayWinAnimation()
    {
        animator.SetTrigger("Dance");
    }
}