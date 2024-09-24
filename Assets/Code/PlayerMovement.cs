using DG.Tweening;
using UnityEngine;
//using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] PlatformManager platformManager;
    [SerializeField] SlicerPositionSetter slicerPositionSetter;

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
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, moveSpeed);
    }

    private void ChangeXPosition()
    {
        transform.DOMoveX(slicerPositionSetter.innerHullCenter.x, 0.5f);
    }
}