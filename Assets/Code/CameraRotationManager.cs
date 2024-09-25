using Cinemachine;
using UnityEngine;

public class CameraRotationManager : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    CinemachineFramingTransposer framingTransposer;
    [SerializeField] private Transform player;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Update()
    {
        if (GameManager._instance.IsLevelCompleted())
        {
            // Player'� biraz d�nd�r�yorum yoksa d�nm�yor
            player.transform.Rotate(new Vector3(0, 0.01f, 0));

            // TrackedObjectOffset'e yeni bir de�er ata
            framingTransposer.m_TrackedObjectOffset = new Vector3(0, 1, 0);

            // Kameray� Y ekseninde d�nd�r
            transform.Rotate(new Vector3(0, -50, 0) * Time.deltaTime, Space.World);
        }
    }
}
