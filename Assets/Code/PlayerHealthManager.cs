using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private void Update()
    {
        CheckLevelFailed();
    }

    private void CheckLevelFailed()
    {
        if (transform.position.y < -2f)
        {
            GameManager._instance.LevelFailed();
        }
    }
}