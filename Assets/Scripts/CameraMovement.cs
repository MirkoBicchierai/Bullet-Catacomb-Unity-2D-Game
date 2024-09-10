using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;        
    public float smoothSpeed = 0.125f;
    public Vector3 offset;  
    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
