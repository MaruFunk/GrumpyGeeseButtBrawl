using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeIn : MonoBehaviour
{
    public float fadeInDuration = 0.005f;  // Short duration in seconds

    private CanvasGroup canvasGroup;

    private void Start()
    {
        // Get the CanvasGroup component
        canvasGroup = GetComponent<CanvasGroup>();

        // Set the initial alpha to 0 (completely transparent)
        canvasGroup.alpha = 0f;

        // Start the fade-in process
        StartCoroutine(FadeIn());
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            // Increase the alpha gradually
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);

            // Increment the timer
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the alpha is exactly 1 at the end
        canvasGroup.alpha = 1f;
    }
}
