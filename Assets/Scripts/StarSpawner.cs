using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private int starCount = 12;
    [SerializeField] private float screenInset = 0.6f;
    [SerializeField] private float topInset = 1.2f;

    private void Start()
    {
        if (starPrefab == null)
        {
            Debug.LogError("StarSpawner: chua gan Star Prefab.");
            return;
        }

        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("StarSpawner: khong tim thay Main Camera.");
            return;
        }

        Vector3 bottomLeft = cam.ViewportToWorldPoint(
            new Vector3(0f, 0f, 0f));
        Vector3 topRight = cam.ViewportToWorldPoint(
            new Vector3(1f, 1f, 0f));

        for (int i = 0; i < starCount; i++)
        {
            Vector2 position = new Vector2(
                Random.Range(bottomLeft.x + screenInset, topRight.x - screenInset),
                Random.Range(bottomLeft.y + screenInset, topRight.y - topInset));

            Instantiate(starPrefab, position, Quaternion.identity);
        }
    }
}
