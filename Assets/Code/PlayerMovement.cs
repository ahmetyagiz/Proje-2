using DG.Tweening;
using UnityEngine;

/// <summary>
/// Bu kod karakterin hareketini saðlar.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float moveSpeed;

    #region OnEnable, OnDisable, Start, FixedUpdate
    private void OnEnable() => PlatformTransferManager._instance.platformChangeEvent.AddListener(ChangeXPosition);
    private void OnDisable() => PlatformTransferManager._instance.platformChangeEvent.RemoveListener(ChangeXPosition);
    private void Start() => rb = GetComponent<Rigidbody>();
    private void FixedUpdate() => MovePlayer();
    #endregion

    private void MovePlayer()
    {
        if (!GameManager._instance.IsLevelCompleted())
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
        }
    }

    private void ChangeXPosition() => transform.DOMoveX(PlatformTransferManager._instance.innerHullCenter.x, 0.5f);
}