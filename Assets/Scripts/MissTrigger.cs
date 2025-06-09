using UnityEngine;

public class MissTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Deduct 1 point
            ScoreManager.instance.AddScore(-1);

            // Optionally destroy the aloe
            Destroy(other.gameObject);
        }
    }
}
