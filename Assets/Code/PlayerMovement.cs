using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed * Time.deltaTime);
    }
}