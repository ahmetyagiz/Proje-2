using UnityEngine;

public class PlatformCutter : MonoBehaviour
{
    public void CutPlatform(GameObject currentPlatform, GameObject previousPlatform)
    {
        float overlap = currentPlatform.transform.position.x - previousPlatform.transform.position.x;
        float excess = Mathf.Abs(overlap);

        if (excess > 0)
        {
            // Fazlalýk kýsmý kes
            Vector3 newScale = currentPlatform.transform.localScale;
            newScale.x -= excess;
            currentPlatform.transform.localScale = newScale;

            // Fazlalýðý düþür
            GameObject fallingPart = Instantiate(currentPlatform);
            fallingPart.transform.position = currentPlatform.transform.position + new Vector3(excess / 2, 0, 0);
            Destroy(fallingPart, 2f); // 2 saniye sonra yok et
        }
    }
}