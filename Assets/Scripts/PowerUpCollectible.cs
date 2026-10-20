using UnityEngine;

public enum PowerUpType
{
    ExtraLife,
    RapidFire,
    Shield
}

public class PowerUpCollectible : MonoBehaviour
{
    [Header("Power-up Settings")]
    public PowerUpType powerUpType;
    public float effectDuration = 5f;
    public float lifetime = 10f;

    private bool collected = false;

    private void Start()
    {
        if (lifetime > 0f)
        {
            Destroy(gameObject, lifetime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player =
            other.GetComponentInParent<PlayerController>();

        if (player == null || collected)
            return;

        if (GameManager.Instance == null)
        {
            Debug.LogError("Khong tim thay GameManager!");
            return;
        }

        collected = true;

        switch (powerUpType)
        {
            case PowerUpType.ExtraLife:
                GameManager.Instance.AddLife(1);
                Debug.Log("Da nhat Extra Life!");
                break;

            case PowerUpType.RapidFire:
                player.ActivateRapidFire(effectDuration);
                Debug.Log("Da kich hoat Rapid Fire!");
                break;

            case PowerUpType.Shield:
                player.ActivateShield(effectDuration);
                Debug.Log("Da kich hoat Shield!");
                break;
        }

        Destroy(gameObject);
    }
}