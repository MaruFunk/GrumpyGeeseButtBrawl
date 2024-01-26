using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public AudioClip collisionSound; // Das Audiosample, das du abspielen möchtest
    private AudioSource audioSource;

    void Start()
    {
        // Holen Sie sich die AudioSource-Komponente, die am GameObject befestigt ist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Wenn keine AudioSource vorhanden ist, füge eine hinzu
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Weise das Audiosample der AudioSource-Komponente zu
        audioSource.clip = collisionSound;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Überprüfe, ob die Kollision mit dem Spieler stattgefunden hat
        if (collision.gameObject.CompareTag("Player"))
        {
            // Spiele den Sound ab
            audioSource.Play();
        }
    }
}
