using UnityEngine;

/// <summary>
/// Bu kod combo yapýldýðýnda ses efektlerinin oynatýlmasýný saðlar.
/// </summary>
public class ComboSoundManager : MonoBehaviour
{
    public AudioClip perfectNote;  // Nota sesi
    private AudioSource audioSource;

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