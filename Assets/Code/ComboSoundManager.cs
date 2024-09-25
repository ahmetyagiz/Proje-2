using UnityEngine;

/// <summary>
/// Bu kod combo yapýldýðýnda ses efektlerinin oynatýlmasýný saðlar.
/// </summary>
public class ComboSoundManager : MonoBehaviour
{
    public AudioClip perfectNote;  // Nota sesi
    private AudioSource audioSource;

    public static ComboSoundManager _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Tekil örneði bu bileþen olarak ayarla
        _instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayNote()
    {
        audioSource.PlayOneShot(perfectNote);
        IncreasePitch();
    }

    void IncreasePitch()
    {
        audioSource.pitch += 0.1f;
    }

    public void SetPitchToDefault()
    {
        audioSource.pitch = 1f;
    }
}