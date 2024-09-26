using UnityEngine;
using Zenject;

/// <summary>
/// Bu kod karakterin belirli mesafe aþaðýya düþtüðünde seviyeyi kaybetmesini saðlar.
/// </summary>
public class PlayerHealthManager : MonoBehaviour
{
    private GameManager _gameManager;
    private bool isPlayerDead;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void Update()
    {
        CheckLevelFailed();
    }

    private void CheckLevelFailed()
    {
        if (transform.position.y < -2f && !isPlayerDead)
        {
            _gameManager.LevelFailed();
            isPlayerDead = true;
        }
    }
}