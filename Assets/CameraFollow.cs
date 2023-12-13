using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Objekt welches die Kamera verfolgen soll
    public float smoothTime = 0.3f; // Wie schnell die Kamera zu dem Objekt aufholt
    public Vector3 offset; // Abstand zu dem Objekt
    private Vector3 velocity = Vector3.zero;

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

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime); // Interpoliert von der aktuellen position zur gewünschten, innerhalt der angegebenen Zeit
        }
    }
}
