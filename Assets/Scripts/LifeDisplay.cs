using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    public Image[] heartImages;      // Drag all heart UI Images into this array
    public Sprite fullHeartSprite;   // Full/active heart sprite
    public Sprite emptyHeartSprite;  // Empty/inactive heart sprite

    // Call this method whenever lives change
    public void UpdateHearts(int currentLives)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].sprite = i < currentLives ? fullHeartSprite : emptyHeartSprite;
        }
    }
}
