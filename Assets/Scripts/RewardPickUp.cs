using UnityEngine;

public class RewardPickUp : MonoBehaviour
{
    [SerializeField] int pointsForPickUp = 1;
    bool wasCollected;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindFirstObjectByType<GameSession>().AddToMoney(pointsForPickUp);
            Destroy(gameObject);
        }
    
    }
}
