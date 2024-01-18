using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Objekt welches die Kamera verfolgen soll
    public float smoothTime = 0.3f; // Wie schnell die Kamera zu dem Objekt aufholt
    public Vector3 offset; // Abstand zu dem Objekt
    private Vector3 velocity = Vector3.zero;

    public Transform topLimiter;
    public float topLimiterOffset;
    public Transform bottomLimiter;
    public float bottomLimiterOffset;
    public Transform rightLimiter;
    public float rightLimiterOffset;
    public Transform leftLimiter;
    public float leftLimiterOffset;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null) // Wenn die target Variable belegt ist, die Kamera also ein Objekt zum verfolgen hat:
        {
            Vector3 targetPosition = target.position + offset; // Fügt der Objekt position den gewählten Offset Hinzu.

            targetPosition.x = Mathf.Clamp(targetPosition.x, (leftLimiter.position.x + leftLimiterOffset), (rightLimiter.position.x - rightLimiterOffset));
            targetPosition.z = Mathf.Clamp(targetPosition.z, (bottomLimiter.position.z + bottomLimiterOffset), (topLimiter.position.z - topLimiterOffset));

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime); // Interpoliert von der aktuellen position zur gewünschten, innerhalt der angegebenen Zeit
        }
    }
}
