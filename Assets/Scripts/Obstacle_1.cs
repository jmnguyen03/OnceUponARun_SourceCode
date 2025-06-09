using UnityEngine;

public class Obstacle_1 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit an obstacle — Game Over!");
            Time.timeScale = 0f; // ⏸ pause the game
            GameObject.Find("GameOverPanel").SetActive(true);

        }
    }
}
