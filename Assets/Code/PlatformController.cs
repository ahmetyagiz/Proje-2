using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float speed = 5f; // Platformun hýzý
    public float leftLimit = -4f; // Sol sýnýr
    public float rightLimit = 4f; // Sað sýnýr
    private bool isMoving = true;
    private Vector3 moveDirection; // Platformun hareket yönü

    void Start()
    {
        // Platform baþlangýçta rastgele saða veya sola hareket edecek
        if (Random.Range(0, 2) == 0)
        {
            moveDirection = Vector3.right; // Saða hareket et
        }
        else
        {
            moveDirection = Vector3.left; // Sola hareket et
        }
    }

    void Update()
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