using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public int index;
    public GameManager gameManager;

    public void OnClick()
    {
        Debug.Log("Button clicked: " + index);
        gameManager.PlayerMove(index);
    }
}
