using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGans: MonoBehaviour
{
    public GameObject playerRef; // Spieler Referenz um ihn anzuchargen
    public float speed; // Gans geschwindigkeit
    public float playerChaseCooldown; // Cooldown zwischen anchargen des Spielers
    private float playerChaseCooldownTimer;
    public float playerChargeThreshold = 6f;


    private float randomDirectionTimer;
    private Vector3 randomDirection;

    private float distance;

    public float spriteFPS;
    private float spriteFPSTimer = 0f;
    private int spriteAnimationStep = 0;
    public Sprite spriteIdle;
    public Sprite spriteMovement;
    public Sprite spriteMovement2;
    public Sprite spriteMovement3;
    public Sprite spriteMovement4;
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
        if (myRigidbody.velocity == Vector3.zero) // Die kondition pr?ft ob es eine Eingabe bewegung gibt; Nur wenn ja:
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

        // Wenn Spieler auf 10 Units rann kommt, dann l?uft die Gans auf den Spieler zu
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
            setCooldown(false); // Cooldown runterz?hlen oder zur?cksetzen
            setRandomDirection(); // Zuf?llige Richtung bestimmen
            myRigidbody.AddForce(0.3f * speed * randomDirection, ForceMode.VelocityChange);
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
            playerChaseCooldownTimer -= Time.deltaTime; // L?uft er weiter runter -> deltaTime: die Zeit die vergangen ist seit dem letzten Frame, also auch seit dem letzten runterz?hlen
        }
    }

    private void setRandomDirection()
    {
        if (randomDirectionTimer <= 0)
        {
            randomDirectionTimer = Random.Range(1f, 5f); // Setzt den Timer f?r die Zuf?llige Richtung auf einen Wert zwischen 0.5 und 3.5 (Sekunden)
            randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        }
        else
        {
            randomDirectionTimer -= Time.deltaTime;
        }
    }

    private void updateSprite()
    {
        spriteFPSTimer += Time.deltaTime;

        if (spriteFPSTimer >= spriteFPS)
        {
            switch (spriteAnimationStep)
            {
                case 3:
                    mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteMovement4;
                    spriteAnimationStep = 0;
                    break;
                case 2:
                    mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteMovement3;
                    spriteAnimationStep = 3;
                    break;
                case 1:
                    mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteMovement2;
                    spriteAnimationStep = 2;
                    break;
                case 0:
                    mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteMovement;
                    spriteAnimationStep = 1;
                    break;

                default:
                    mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteIdle;
                    break;
            }
            spriteFPSTimer = 0f;
        }

    }

}
