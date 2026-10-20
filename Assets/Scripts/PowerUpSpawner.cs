using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;

    public float firstSpawnDelay = 8f;
    public float spawnInterval = 15f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        if (powerUpPrefabs == null || powerUpPrefabs.Length == 0)
        {
            Debug.LogWarning("Chua gan Power-up Prefabs!");
            return;
        }

        if (mainCamera == null)
        {
            Debug.LogError("Khong tim thay Main Camera!");
            return;
        }

        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(firstSpawnDelay);

        while (true)
        {
            SpawnPowerUp();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnPowerUp()
    {
        GameObject prefab = powerUpPrefabs[
            Random.Range(0, powerUpPrefabs.Length)
        ];

        float x = Random.Range(0.1f, 0.9f);
        float y = Random.Range(0.15f, 0.85f);
        float depth = Mathf.Abs(mainCamera.transform.position.z);

        Vector3 position = mainCamera.ViewportToWorldPoint(
            new Vector3(x, y, depth)
        );

        position.z = 0f;

        Instantiate(prefab, position, Quaternion.identity);
    }
}