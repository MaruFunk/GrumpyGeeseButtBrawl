using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Damit wir das neue Input System von Unity benutzen k�nnen

public class PlayerController : MonoBehaviour
{

    public float speed; // Enh�lt die Geschwindigkeit vom Spielercharakter
    private Vector2 move; // Ent�hlt unsere bewegungsrichtung (theoretisch auch einen Geschwindigkeits-Multiplikator bei einem Analog-Stick z.b.)

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
    
    /* 
     * Diese Funktion setzt den move-Wert, also die Bewegungsrichtung
     * Hierzu wird die CallbackContext Komponente des InputSystems mit �bergeben
     * Innerhalb des Funktion lautet diese context
     * 
     * In der Funktion wird einfach nur der Variablen-Wert gleich dem aktuellen Wert(Value) aus dem context gesetzt
     * Also die Eingabe-Werte die das InputSystem zu dem Zeitpunkt auslie�t
     */
    public void OnMove(InputAction.CallbackContext context) 
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnKick(InputAction.CallbackContext context)
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider target in targets)
        {
            if(target.tag == "Gans")
            {
                print("KICK!");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer(); // Hier rufen wir jeden Frame die movePlayer Funktion auf.
    }

    /*
     * die movePlayer Funktion nimmt den aktuellen wert der move Variable und f�hrt auf deren Basis die tats�chliche Bewegung durch.
     * Zuerst erstellen wir eine lokale Variable, welche die Bewegungrichtung h�lt.
     * 
     * Danach multiplizieren wir die Richtung mit der Geschwindigkeit und gewegen das Objekt entsprechend.
     * Time.deltaTime ist hierbei "Die vergangene Zeit seit dem letzten Frame"; was einfach nur bedeutet dass die bewegung gleich schnell ist unabh�ngig davon mit wieviel FPS das Programm l�uft.
     * Space.World gibt einfach nur an in relation zu was das Objekt sich bewegen soll.
     */
    public void movePlayer()
    {
        Vector3 movenent = new Vector3(move.x, 0f, move.y); // Hier erstellen und setzen eine Vector3 Variable, welche die Bewegungsrichtung im 3Dimensionalen Raum angibt.

        if (move.x == 0 && move.y == 0)
        {
            mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteIdle;
        }
        else if (move.x > 0)
        {
            updateSprite();
            mySpriteComponent.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (move.x < 0)
        {
            updateSprite();
            mySpriteComponent.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            updateSprite();
        }

       
        myRigidbody.AddForce(movenent * speed, ForceMode.VelocityChange); // Hier bewegen wir das Object an welches das Script angeh�ngt ist. movement ist unser Richtungs-Faktor und speed der multiplikator f�r die Geschwindigkeit.
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
