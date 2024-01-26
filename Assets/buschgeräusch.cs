using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour
{
    public AudioClip collisionSound; // Der Sound, der abgespielt werden soll

    private void OnCollisionEnter(Collision collision)
    {
        PlayCollisionSound(); // Rufe die Funktion zum Abspielen des Sounds auf
    }

    private void PlayCollisionSound()
    {
        // Überprüfen, ob ein Audiosource-Komponente an diesem GameObject angefügt ist
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Wenn nicht, füge eine hinzu
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Setze den Sound für den Audiosource
        audioSource.clip = collisionSound;

        // Spiele den Sound ab
        audioSource.Play();
    }
}

