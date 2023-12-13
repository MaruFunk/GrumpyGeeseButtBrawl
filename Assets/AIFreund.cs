using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFreund : MonoBehaviour
{

    public GameObject playerRef;
    public float speed;

    private float randomDirectionTimer;
    private Vector3 randomDirection;

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
        distance = Vector3.Distance(transform.position, playerRef.transform.position); // Die Distanz zum Spieler
        Vector3 direction =  transform.position - playerRef.transform.position; // Die Richtung in welcher sich der Spieler befindet
        direction.Normalize();

        // Sprite Anpassung basierend auf Bewegungsrichtung
        if (myRigidbody.velocity == Vector3.zero)
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

        if (distance <= 5)
        {
            myRigidbody.AddForce(0.65f * speed * direction, ForceMode.VelocityChange);
        }
        else if (distance <= 10)
        {
            myRigidbody.AddForce(0.5f * speed * direction, ForceMode.VelocityChange);
        }
        else // Ansosten Random Movement
        {
            setRandomDirection(); // Zufällige Richtung bestimmen
            myRigidbody.AddForce(0.3f * speed * randomDirection, ForceMode.VelocityChange);
        }

    }

    private void setRandomDirection()
    {
        if (randomDirectionTimer <= 0)
        {
            randomDirectionTimer = Random.Range(1f, 5f); // Setzt den Timer für die Zufällige Richtung auf einen Wert zwischen 0.5 und 3.5 (Sekunden)
            randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        }
        else
        {
            randomDirectionTimer -= Time.deltaTime;
        }
    }
}
