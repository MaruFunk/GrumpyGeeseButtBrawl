using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityController : MonoBehaviour
{
    private bool isImmune = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isImmune)
        {
            // Handle collision logic here

            // Make the GameObject immune
            StartImmunityTimer();
        }
    }

    private void StartImmunityTimer()
    {
        isImmune = true;
        StartCoroutine(EndImmunityAfterDelay(3f));
    }

    private System.Collections.IEnumerator EndImmunityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Make the GameObject vulnerable again
        isImmune = false;
    }
}
