using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChicken : MonoBehaviour
{
    public GameObject playerRef;
    public float speed;

    private float distance;

    public Sprite spriteIdle;
    public Sprite spriteMovement;
    public GameObject mySpriteComponent;

    Rigidbody myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, playerRef.transform.position); // Die Distanz zum Spieler
        Vector3 direction = playerRef.transform.position - transform.position; // Die Richtung in welcher sich der Spieler befindet

        //transform.position = Vector3.MoveTowards(this.transform.position, playerRef.transform.position, speed * Time.deltaTime);

        if (myRigidbody.velocity == Vector3.zero) // Die kondition prüft ob es eine Eingabe bewegung gibt; Nur wenn ja:
        {
            mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteIdle;

        }
        else if (direction.x > 0)
        {
            mySpriteComponent.GetComponent<SpriteRenderer>().flipX = false;
            mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteMovement;
        }
        else if (direction.x < 0)
        {
            mySpriteComponent.GetComponent<SpriteRenderer>().flipX = true;
            mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteMovement;
        }

        myRigidbody.AddForce(direction * speed, ForceMode.Acceleration);
    }
}
