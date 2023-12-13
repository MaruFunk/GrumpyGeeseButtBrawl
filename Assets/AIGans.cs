using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChicken : MonoBehaviour
{
    public GameObject playerRef; // Spieler Referenz um ihn anzuchargen
    public float speed; // Gans geschwindigkeit
    public float playerChaseCooldown; // Cooldown zwischen anchargen des Spielers
    private float playerChaseCooldownTimer;
    public float playerChargeThreshold = 6f;


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
        Vector3 direction = playerRef.transform.position - transform.position; // Die Richtung in welcher sich der Spieler befindet
        direction.Normalize();

        // Sprite Anpassung basierend auf Bewegungsrichtung
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

        // Wenn Spieler auf 10 Units rann kommt, dann läuft die Gans auf den Spieler zu
        if ( distance <= playerChargeThreshold && playerChaseCooldownTimer <= 0)
        {
            myRigidbody.AddForce(direction * speed, ForceMode.VelocityChange);
            if (distance <= 2)
            {
                setCooldown(true);
            }
        }
        else // Ansosten Random Movement bis Timer auf 0
        {
            setCooldown(false); // Cooldown runterzählen oder zurücksetzen
            setRandomDirection(); // Zufällige Richtung bestimmen
            myRigidbody.AddForce(0.2f * speed * randomDirection, ForceMode.VelocityChange);
        }
    }

    private void setCooldown(bool reset)
    {
        if (playerChaseCooldownTimer <= 0 && reset) // Wenn der Cooldown abgelaufen ist:
        {
            playerChaseCooldownTimer = playerChaseCooldown; // Wird er wieder auf den Startwert gesetzt
        }
        else // Ansosonsten:
        {
            playerChaseCooldownTimer -= Time.deltaTime; // Läuft er weiter runter -> deltaTime: die Zeit die vergangen ist seit dem letzten Frame, also auch seit dem letzten runterzählen
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
