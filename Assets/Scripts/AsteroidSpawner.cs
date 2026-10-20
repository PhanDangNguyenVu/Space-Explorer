using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid")]
    public GameObject asteroidPrefab;

    [Header("Spawn Settings")]
    public float baseSpawnInterval = 2f;
    public float minSpawnInterval = 0.5f;
    public float spawnIntervalDecreasePerMinute = 0.3f;

    [Header("Difficulty Settings")]
    public float speedIncreasePerMinute = 0.25f;
    public float maxSpeedMultiplier = 2f;

    private float elapsedGameTime = 0f;
    private Camera mainCamera;

    public static float SpeedMultiplier { get; private set; } = 1f;

    private void Start()
    {
        mainCamera = Camera.main;
        elapsedGameTime = 0f;
        SpeedMultiplier = 1f;

        if (mainCamera == null)
        {
            Debug.LogError("Khong tim thay Main Camera!");
            return;
        }

        if (asteroidPrefab == null)
        {
            Debug.LogError("Chua gan Asteroid Prefab!");
            return;
        }

        StartCoroutine(SpawnLoop());
    }

    private void Update()
    {
        elapsedGameTime += Time.deltaTime;

        float minutes = elapsedGameTime / 60f;

        SpeedMultiplier = Mathf.Min(
            maxSpeedMultiplier,
            1f + minutes * speedIncreasePerMinute
        );
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnAsteroid();

            float minutes = elapsedGameTime / 60f;

            float currentInterval = Mathf.Max(
                minSpawnInterval,
                baseSpawnInterval -
                minutes * spawnIntervalDecreasePerMinute
            );

            yield return new WaitForSeconds(currentInterval);
        }
    }

    private void SpawnAsteroid()
    {
        if (asteroidPrefab == null || mainCamera == null)
            return;

        float x = Random.Range(0.05f, 0.95f);
        float depth = Mathf.Abs(mainCamera.transform.position.z);

        Vector3 spawnPosition = mainCamera.ViewportToWorldPoint(
            new Vector3(x, 1.1f, depth)
        );

        spawnPosition.z = 0f;

        Instantiate(
            asteroidPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}