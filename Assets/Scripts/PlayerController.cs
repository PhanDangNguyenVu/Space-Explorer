using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Shooting")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireInterval = 0.25f;
    [Header("Damage Settings")]
    [SerializeField] private float invincibilityDuration = 1.5f;
    [SerializeField] private float blinkInterval = 0.12f;

    private bool isInvincible = false;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private Camera mainCamera;
    private Vector2 moveInput;
    private float nextShotTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Doc phim mui ten/WASD va chuan hoa de di cheo khong nhanh hon.
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        // Giu Space de ban lien tuc theo khoang cach thoi gian.
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + fireInterval;
        }
    }

    private void FixedUpdate()
    {
        if (rb == null)
            return;

        Vector2 nextPosition = rb.position +
            moveInput * moveSpeed * Time.fixedDeltaTime;

        // Gioi han tau trong vung Camera.
        if (mainCamera != null && playerCollider != null)
        {
            Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(
                new Vector3(0f, 0f, 0f));
            Vector3 topRight = mainCamera.ViewportToWorldPoint(
                new Vector3(1f, 1f, 0f));

            float halfWidth = playerCollider.bounds.extents.x;
            float halfHeight = playerCollider.bounds.extents.y;

            nextPosition.x = Mathf.Clamp(
                nextPosition.x,
                bottomLeft.x + halfWidth,
                topRight.x - halfWidth);

            nextPosition.y = Mathf.Clamp(
                nextPosition.y,
                bottomLeft.y + halfHeight,
                topRight.y - halfHeight);
        }

        rb.MovePosition(nextPosition);
    }

    private void Shoot()
    {
        if (laserPrefab == null || firePoint == null)
        {
            Debug.LogWarning(
                "Player: chua gan Laser Prefab hoac Fire Point trong Inspector.");
            return;
        }

        Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Asteroid"))
            return;

        // Pha huy asteroid khi no va cham voi tau
        Destroy(other.gameObject);

        // Khong nhan sat thuong neu dang bat tu
        if (isInvincible)
            return;

        if (GameManager.Instance == null)
        {
            Debug.LogError("Khong tim thay GameManager!");
            return;
        }

        StartCoroutine(DamageRoutine());
    }

    private IEnumerator DamageRoutine()
    {
        isInvincible = true;

        GameManager manager = GameManager.Instance;

        if (manager == null)
        {
            isInvincible = false;
            yield break;
        }

        // Giam mot mang
        manager.LoseLife();

        // Dung xu ly neu game da ket thuc
        if (manager.IsGameEnded)
            yield break;

        // Lay tat ca SpriteRenderer cua tau va cac doi tuong con
        SpriteRenderer[] renderers =
            GetComponentsInChildren<SpriteRenderer>();

        bool[] originalStates = new bool[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalStates[i] = renderers[i] != null &&
                                renderers[i].enabled;
        }

        float interval = Mathf.Max(0.05f, blinkInterval);
        float duration = Mathf.Max(0f, invincibilityDuration);
        float elapsed = 0f;
        bool visible = false;

        // Lam tau nhap nhay trong thoi gian bat tu
        while (elapsed < duration)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i] != null)
                {
                    renderers[i].enabled =
                        originalStates[i] && visible;
                }
            }

            visible = !visible;

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        // Khoi phuc trang thai hien thi ban dau
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] != null)
            {
                renderers[i].enabled = originalStates[i];
            }
        }

        isInvincible = false;
    }
}
