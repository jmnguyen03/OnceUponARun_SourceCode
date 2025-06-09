using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -7);

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.z = target.position.z + offset.z;
            transform.position = newPosition;
        }
    }
}
