using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit an obstacle");

            // Call the GameOver function from GameManager
            FindObjectOfType<MainGameManager>().LoseLife();

            // Optional: pause the game
            //Time.timeScale = 0f;
        }
    }
}
