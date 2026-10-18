using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private float destroyMargin = 1.5f;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector2 moveDirection;
    private bool directionAssigned;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.001f)
        {
            direction = Vector2.down;
        }

        moveDirection = direction.normalized;
        directionAssigned = true;
    }

    private void Start()
    {
        if (!directionAssigned)
        {
            moveDirection = Random.insideUnitCircle.normalized;
            if (moveDirection.sqrMagnitude < 0.001f)
            {
                moveDirection = Vector2.down;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(
            rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        transform.Rotate(0f, 0f, rotationSpeed * Time.fixedDeltaTime);

        // Xoa thien thach sau khi no roi xa vung Camera.
        if (mainCamera != null)
        {
            Vector3 min = mainCamera.ViewportToWorldPoint(
                new Vector3(0f, 0f, 0f));
            Vector3 max = mainCamera.ViewportToWorldPoint(
                new Vector3(1f, 1f, 0f));

            if (rb.position.x < min.x - destroyMargin ||
                rb.position.x > max.x + destroyMargin ||
                rb.position.y < min.y - destroyMargin ||
                rb.position.y > max.y + destroyMargin)
            {
                Destroy(gameObject);
            }
        }
    }
}
