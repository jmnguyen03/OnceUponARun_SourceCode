using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform target;       // Player to follow
    public Vector3 offset = new Vector3(0, 5, -10); // Camera offset from the player

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
