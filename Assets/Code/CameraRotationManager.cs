using Cinemachine;
using UnityEngine;
using Zenject;

/// <summary>
/// Bu kod seviye sonunda kameran�n d�nmesini sa�lar.
/// </summary>
public class CameraRotationManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float rotationSpeed;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineFramingTransposer framingTransposer;
    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Update()
    {
        if (_gameManager.IsLevelCompleted())
        {
            // Player'� biraz d�nd�r�yorum yoksa d�nm�yor
            player.transform.Rotate(new Vector3(0, 0.01f, 0));

            // TrackedObjectOffset'e yeni bir de�er ata
            framingTransposer.m_TrackedObjectOffset = new Vector3(0, 1, 0);

            // Kameray� Y ekseninde d�nd�r
            transform.Rotate(new Vector3(0, -rotationSpeed, 0) * Time.deltaTime, Space.World);
        }
    }
}