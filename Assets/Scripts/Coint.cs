using UnityEngine;

public class Coint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(GameSetting.aloePoint);
            Destroy(gameObject); 
        }
    }
}
