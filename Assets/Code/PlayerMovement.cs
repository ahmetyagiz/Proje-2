using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    [SerializeField] private float moveSpeed;
    [SerializeField] PlatformManager platformManager;
    [SerializeField] SlicerPositionSetter slicerPositionSetter;
    private bool isLevelCompleted;

    private void OnEnable()
    {
        platformManager.platformChangeEvent.AddListener(ChangeXPosition);
    }

    private void OnDisable()
    {
        platformManager.platformChangeEvent.RemoveListener(ChangeXPosition);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (!isLevelCompleted)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
        }
    }

    private void ChangeXPosition()
    {
        transform.DOMoveX(slicerPositionSetter.innerHullCenter.x, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinTrigger"))
        {
            PlayWinAnimation();
        }
    }

    private void PlayWinAnimation()
    {
        isLevelCompleted = true;
        animator.SetTrigger("Dance");
    }
}