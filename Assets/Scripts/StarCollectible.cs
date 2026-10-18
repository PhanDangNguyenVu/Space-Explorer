using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    [SerializeField] private int points = 10;
    private bool collected;

    public void Collect()
    {
        if (collected)
        {
            return;
        }

        collected = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(points);
        }

        Destroy(gameObject);
    }
}
