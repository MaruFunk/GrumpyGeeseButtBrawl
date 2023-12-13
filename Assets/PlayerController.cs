using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Damit wir das neue Input System von Unity benutzen können

public class PlayerController : MonoBehaviour
{

    public float speed; // Enhält die Geschwindigkeit vom Spielercharakter
    private Vector2 move; // Entählt unsere bewegungsrichtung (theoretisch auch einen Geschwindigkeits-Multiplikator bei einem Analog-Stick z.b.)

    public Sprite spriteIdle;
    public Sprite spriteMovement;
    public GameObject mySpriteComponent;


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
        move = context.ReadValue<Vector2>();
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

        if(move.x == 0 && move.y == 0 && myRigidbody.velocity == Vector3.zero)
        {
            mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteIdle;
        }
        else if (move.x > 0)
        {
            mySpriteComponent.GetComponent<SpriteRenderer>().flipX = false;
            mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteMovement;
        }
        else if (move.x < 0)
        {
            mySpriteComponent.GetComponent<SpriteRenderer>().flipX = true;
            mySpriteComponent.GetComponent<SpriteRenderer>().sprite = spriteMovement;
        }

       
        myRigidbody.AddForce(movenent * speed, ForceMode.VelocityChange); // Hier bewegen wir das Object an welches das Script angehängt ist. movement ist unser Richtungs-Faktor und speed der multiplikator für die Geschwindigkeit.
    }
}
