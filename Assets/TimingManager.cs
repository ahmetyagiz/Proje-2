using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public float perfectThreshold = 0.1f; // Tolerans aral���

    public bool IsPerfect(float overlap)
    {
        return Mathf.Abs(overlap) < perfectThreshold;
    }
}