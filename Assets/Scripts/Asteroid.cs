using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 60f;

    private Vector2 movementDirection;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        // Di chuyen xuong duoi va lech ngau nhien sang hai ben
        movementDirection = new Vector2(
            Random.Range(-0.6f, 0.6f),
            Random.Range(-1f, -0.35f)
        ).normalized;
    }

    private void Update()
    {
        float currentSpeed =
            moveSpeed * AsteroidSpawner.SpeedMultiplier;

        transform.position += (Vector3)(
            movementDirection * currentSpeed * Time.deltaTime
        );

        transform.Rotate(
            0f,
            0f,
            rotationSpeed * Time.deltaTime
        );

        DestroyIfOffScreen();
    }

    private void DestroyIfOffScreen()
    {
        if (mainCamera == null)
            return;

        float depth = Mathf.Abs(mainCamera.transform.position.z);

        float bottomY = mainCamera.ViewportToWorldPoint(
            new Vector3(0.5f, 0f, depth)
        ).y;

        if (transform.position.y < bottomY - 2f)
        {
            Destroy(gameObject);
        }
    }
}