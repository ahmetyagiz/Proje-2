using UnityEngine;

public enum PlatformStartDirection { Left, Right };

/// <summary>
/// Bu kod platformlarýn hareketini saðlar. Enum ile baþlangýçta hangi yöne gideceði belirlenir.
/// </summary>
public class PlatformController : MonoBehaviour
{
    [SerializeField] private PlatformStartDirection platformStartDirection;
    [SerializeField] private float speed = 5f; // Platformun hýzý
    [SerializeField] private float leftLimit = -4f; // Sol sýnýr
    [SerializeField] private float rightLimit = 4f; // Sað sýnýr
    private bool isMoving = true;
    private Vector3 moveDirection; // Platformun hareket yönü

    void Start()
    {
        InitializeStartDirection();
    }

    void InitializeStartDirection()
    {
        // Platform baþlangýçta rastgele saða veya sola hareket edecek
        if (platformStartDirection == PlatformStartDirection.Left)
        {
            moveDirection = Vector3.left; // Sola hareket et
        }
        else
        {
            moveDirection = Vector3.right; // Saða hareket et
        }
    }

    void Update()
    {
        PlatformMovement();
    }

    void PlatformMovement()
    {
        if (isMoving)
        {
            // Platformu hareket ettir
            transform.Translate(speed * Time.deltaTime * moveDirection);

            // Platform sýnýrlarý geçtiðinde yön deðiþtir
            if (transform.position.x >= rightLimit && moveDirection == Vector3.right)
            {
                moveDirection = Vector3.left; // Saða giderken sað sýnýra ulaþtý, sola dön
            }
            else if (transform.position.x <= leftLimit && moveDirection == Vector3.left)
            {
                moveDirection = Vector3.right; // Sola giderken sol sýnýra ulaþtý, saða dön
            }
        }
    }

    public void StopPlatform()
    {
        isMoving = false;
    }
}