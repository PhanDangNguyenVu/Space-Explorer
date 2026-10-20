
using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    public int scoreValue = 10;

    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Star va cham voi: " + other.name);

        // Kiem tra doi tuong va cham co phai Player khong
        PlayerController player =
            other.GetComponentInParent<PlayerController>();

        if (player == null)
        {
            Debug.Log("Doi tuong va cham khong phai Player.");
            return;
        }

        if (collected)
            return;

        if (GameManager.Instance == null)
        {
            Debug.LogError("Khong tim thay GameManager.Instance!");
            return;
        }

        collected = true;

        GameManager.Instance.AddScore(scoreValue);

        Debug.Log("Da nhat sao, cong " + scoreValue + " diem.");

        Destroy(gameObject);
    }
}
