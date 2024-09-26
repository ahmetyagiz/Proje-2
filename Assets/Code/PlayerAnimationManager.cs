using UnityEngine;
using Zenject;

/// <summary>
/// Bu kod karakterin animasyonlar�n� y�netir.
/// </summary>
public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;
    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinTrigger"))
        {
            // Seviye tamamland�
            _gameManager.SetLevelCompleted();
            PlayWinAnimation();
        }
    }

    private void PlayWinAnimation()
    {
        animator.SetTrigger("Dance");
    }
}