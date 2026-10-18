using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private int initialAsteroidCount = 8;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnMargin = 0.6f;
    [SerializeField] private float targetInset = 0.5f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        if (asteroidPrefab == null)
        {
            Debug.LogError("AsteroidSpawner: chua gan Asteroid Prefab.");
            return;
        }

        for (int i = 0; i < initialAsteroidCount; i++)
        {
            SpawnAsteroid();
        }

        InvokeRepeating(nameof(SpawnAsteroid), 1f, spawnInterval);
    }

    private void SpawnAsteroid()
    {
        if (asteroidPrefab == null || mainCamera == null)
        {
            return;
        }

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(
            new Vector3(0f, 0f, 0f));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(
            new Vector3(1f, 1f, 0f));

        Vector2 spawnPosition;
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0: // Ben trai
                spawnPosition = new Vector2(
                    bottomLeft.x - spawnMargin,
                    Random.Range(bottomLeft.y, topRight.y));
                break;

            case 1: // Ben phai
                spawnPosition = new Vector2(
                    topRight.x + spawnMargin,
                    Random.Range(bottomLeft.y, topRight.y));
                break;

            case 2: // Phia tren
                spawnPosition = new Vector2(
                    Random.Range(bottomLeft.x, topRight.x),
                    topRight.y + spawnMargin);
                break;

            default: // Phia duoi
                spawnPosition = new Vector2(
                    Random.Range(bottomLeft.x, topRight.x),
                    bottomLeft.y - spawnMargin);
                break;
        }

        Vector2 target = new Vector2(
            Random.Range(bottomLeft.x + targetInset, topRight.x - targetInset),
            Random.Range(bottomLeft.y + targetInset, topRight.y - targetInset));

        GameObject asteroidObject = Instantiate(
            asteroidPrefab, spawnPosition, Quaternion.identity);

        Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
        if (asteroid != null)
        {
            asteroid.SetDirection(target - spawnPosition);
        }
    }
}
