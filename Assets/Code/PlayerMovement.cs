using DG.Tweening;
using UnityEngine;
using Zenject;

/// <summary>
/// Bu kod karakterin hareketini saðlar.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float moveSpeed;

    private PlatformTransferManager _platformTransferManager;
    private GameManager _gameManager;

    [Inject]
    public void Construct(PlatformTransferManager platformTransferManager, GameManager gameManager)
    {
        _platformTransferManager = platformTransferManager;
        _gameManager = gameManager;
    }

    private void OnEnable()
    {
        _platformTransferManager.platformChangeEvent.AddListener(ChangeXPosition);
    }

    public void OnDisable()
    {
        _platformTransferManager.platformChangeEvent.RemoveListener(ChangeXPosition);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!_gameManager.IsLevelCompleted())
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
        }
    }

    private void ChangeXPosition()
    {
        transform.DOMoveX(_platformTransferManager.innerHullCenter.x, 0.5f);
    }
}