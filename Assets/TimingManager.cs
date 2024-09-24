using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public float perfectThreshold = 0.1f; // Tolerans aralýðý

    public bool IsPerfect(float overlap)
    {
        return Mathf.Abs(overlap) < perfectThreshold;
    }
}