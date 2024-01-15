using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFreund : MonoBehaviour
{

    public GameObject playerRef;
    public GameObject nordWand;
    public GameObject suedWand;
    public GameObject ostWand;
    public GameObject westWand;
    public float speed;

    private float randomDirectionTimer;
    private Vector3 direction;

    private float playerDistance;
    private float nordWandZ;
    private float suedWandZ;
    private float ostWandX;
    private float westWandX;

    public float spriteFPS;
    private float spriteFPSTimer = 0f;
    private int spriteAnimationStep = 0;
    public Sprite spriteIdle;
    public Sprite spriteMovement1;
    public Sprite spriteMovement2;
    public Sprite spriteMovement3;
    public Sprite spriteMovement4;
    public GameObject mySpriteComponent;


    Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        nordWandZ = nordWand.transform.position.z;
        suedWandZ = suedWand.transform.position.z;
        ostWandX = ostWand.transform.position.x;
        westWandX = westWand.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        updateSprite();

        playerDistance = Vector3.Distance(transform.position, playerRef.transform.position); // Die Distanz zum Spieler

        // Sprite Anpassung basierend auf Bewegungsrichtung
        if (myRigidbody.velocity == Vector3.zero)
        {
        }
        else if (direction.x > 0)
        {
            mySpriteComponent.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (direction.x < 0)
        {
            mySpriteComponent.GetComponent<SpriteRenderer>().flipX = true;
        }

        // Freund Bewegungsverhalten
        if (playerDistance <= 5) // wenn Spieler sehr Nah
        {
            direction = transform.position - playerRef.transform.position; // Die Richtung in welcher sich der Spieler befindet
            direction.Normalize();
            directAwayFromWall();
            myRigidbody.AddForce(0.75f * speed * direction, ForceMode.VelocityChange);
        }
        else if (playerDistance <= 10) // wenn Spiele Nah
        {
            direction = transform.position - playerRef.transform.position; // Die Richtung in welcher sich der Spieler befindet
            direction.Normalize();
            directAwayFromWall();
            myRigidbody.AddForce(0.5f * speed * direction, ForceMode.VelocityChange);
        }
        else // Ansosten Random Movement
        {
            setRandomDirection(); // Zufällige Richtung bestimmen
            direction.Normalize();
            directAwayFromWall();
            myRigidbody.AddForce(0.5f * speed * direction, ForceMode.VelocityChange);
        }

    }
    
    private void directAwayFromWall()
    {
        if (transform.position.z >= nordWandZ - 10)
        {
            direction.z = -1;

        }

        if (transform.position.z <= (suedWandZ + 10))
        {
            direction.z = 1;

        }

        if (transform.position.x >= (ostWandX - 10))
        {
            direction.x = -1;

        }

        if (transform.position.x <= (westWandX + 10))
        {
            direction.x = 1;

        }
    }

    private void setRandomDirection()
    {
        if (randomDirectionTimer <= 0)
        {
            randomDirectionTimer = Random.Range(1f, 5f); // Setzt den Timer für die Zufällige Richtung auf einen Wert zwischen 0.5 und 3.5 (Sekunden)
            direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
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
                    mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteMovement1;
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
