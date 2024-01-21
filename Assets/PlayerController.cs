using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Damit wir das neue Input System von Unity benutzen können

public class PlayerController : MonoBehaviour
{

    public float speed; // Enhält die Geschwindigkeit vom Spielercharakter
    private Vector2 move; // Entählt unsere bewegungsrichtung (theoretisch auch einen Geschwindigkeits-Multiplikator bei einem Analog-Stick z.b.)

    public float kickForce = 20f;

    public int kicksToWin = 3;
    private int friendKickCounter = 0;

    public float spriteFPS;
    private float spriteFPSTimer = 0f;
    private int spriteAnimationStep = 0;
    public Sprite spriteIdle;
    public Sprite spriteMovement1;
    public Sprite spriteMovement2;
    public Sprite spriteMovement3;
    public Sprite spriteMovement4;
    public Sprite spriteKick;
    public GameObject mySpriteComponent;

    public IntroGameSequence introGameSequence;
    public bool gameIntro = true;

    public GameObject endgameScreenRef;


    Rigidbody myRigidbody;
    
    /* 
     * Diese Funktion setzt den move-Wert, also die Bewegungsrichtung
     * Hierzu wird die CallbackContext Komponente des InputSystems mit übergeben
     * Innerhalb des Funktion lautet diese context
     * 
     * In der Funktion wird einfach nur der Variablen-Wert gleich dem aktuellen Wert(Value) aus dem context gesetzt
     * Also die Eingabe-Werte die das InputSystem zu dem Zeitpunkt ausließt
     */
    public void OnMove(InputAction.CallbackContext context) 
    {
        if (!gameIntro)
            move = context.ReadValue<Vector2>();
        else if (context.performed)
            gameIntro = introGameSequence.nextFrame();
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer(); // Hier rufen wir jeden Frame die movePlayer Funktion auf.
    }

    /*
     * die movePlayer Funktion nimmt den aktuellen wert der move Variable und führt auf deren Basis die tatsächliche Bewegung durch.
     * Zuerst erstellen wir eine lokale Variable, welche die Bewegungrichtung hält.
     * 
     * Danach multiplizieren wir die Richtung mit der Geschwindigkeit und gewegen das Objekt entsprechend.
     * Time.deltaTime ist hierbei "Die vergangene Zeit seit dem letzten Frame"; was einfach nur bedeutet dass die bewegung gleich schnell ist unabhängig davon mit wieviel FPS das Programm läuft.
     * Space.World gibt einfach nur an in relation zu was das Objekt sich bewegen soll.
     */
    public void movePlayer()
    {
        Vector3 movenent = new Vector3(move.x, 0f, move.y); // Hier erstellen und setzen eine Vector3 Variable, welche die Bewegungsrichtung im 3Dimensionalen Raum angibt.

        if (move.x == 0 && move.y == 0)
        {
            updateSprite(0);
            // mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteIdle;
        }
        else if (move.x > 0)
        {
            updateSprite(1);
            mySpriteComponent.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (move.x < 0)
        {
            updateSprite(1);
            mySpriteComponent.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            updateSprite(1);
        }

       
        myRigidbody.AddForce(movenent * speed, ForceMode.VelocityChange); // Hier bewegen wir das Object an welches das Script angehängt ist. movement ist unser Richtungs-Faktor und speed der multiplikator für die Geschwindigkeit.
    }

    private void updateSprite(int animationType)
    {
        spriteFPSTimer += Time.deltaTime;

        if (animationType == 0)
        {
            mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteIdle;
            spriteAnimationStep = 0;
            spriteFPSTimer = 0f;
        }

        if (animationType == 2)
        {
            mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteKick;
            spriteAnimationStep = 0;
            spriteFPSTimer = 0f;
        }

        if (spriteFPSTimer >= spriteFPS)
        {

            if (animationType == 1)
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

                }
            }
            spriteFPSTimer = 0f;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Gans")
        {
            updateSprite(2);
            print("Gans KICK!");

            Vector3 kickDirection = (collision.gameObject.transform.position - transform.position);

            collision.gameObject.GetComponent<Rigidbody>().AddForce(kickForce * kickDirection, ForceMode.VelocityChange);
        }

        if (collision.gameObject.tag == "Freund")
        {
            updateSprite(2);
            print("You kicked your friend, you win!");


            kickFriend(collision.gameObject);
        }

        
    }

    private void kickFriend(GameObject friend)
    {
        Vector3 kickDirection = (friend.transform.position - transform.position);

        friend.GetComponent<Rigidbody>().AddForce(kickForce * kickDirection, ForceMode.Impulse);

        friend.GetComponent<AIFreund>().getKicked();

        friendKickCounter++;

        if(friendKickCounter >= kicksToWin)
        {
            Time.timeScale = 0.05f;

            endgameScreenRef.SetActive(true);
        }

    }
}
