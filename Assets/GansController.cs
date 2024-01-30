using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour
{
    public GameObject poopPrefab;  // Assign the poop sprite prefab in the Unity Editor
    public Transform poopSpawnPoint; // Optional: Assign a transform for poop spawning position

    public float minTimeBetweenPoops = 2f; // Minimum time between poops in seconds
    public float maxTimeBetweenPoops = 5f; // Maximum time between poops in seconds

    private float timeSinceLastPoop;
    private float timeToNextPoop;

    void Start()
    {
        // Initialize times
        timeSinceLastPoop = 0f;
        timeToNextPoop = Random.Range(minTimeBetweenPoops, maxTimeBetweenPoops);
    }

    void Update()
    {
        // Update timeSinceLastPoop
        timeSinceLastPoop += Time.deltaTime;

        // Check if it's time to poop
        if (timeSinceLastPoop >= timeToNextPoop)
        {
            // Calculate poop position
            Vector3 poopPosition = (poopSpawnPoint != null) ? poopSpawnPoint.position : transform.position;

            // Call the Poop function with the calculated position
            Poop(poopPosition);

            // Reset timers for the next poop
            timeSinceLastPoop = 0f;
            timeToNextPoop = Random.Range(minTimeBetweenPoops, maxTimeBetweenPoops);
        }
    }

    void Poop(Vector3 poopPosition)
    {
        // Ensure poopPrefab is assigned in the Unity Editor
        if (poopPrefab != null)
        {
            // Instantiate the poop prefab
            GameObject poopInstance = Instantiate(poopPrefab, poopPosition, Quaternion.identity);

            // Set the sorting order of the poop sprite renderer to be lower than the duck's sorting order
            SpriteRenderer poopRenderer = poopInstance.GetComponent<SpriteRenderer>();
            if (poopRenderer != null)
            {
                // Get the duck's sorting order
                SpriteRenderer duckRenderer = GetComponent<SpriteRenderer>();
                int duckSortingOrder = (duckRenderer != null) ? duckRenderer.sortingOrder : 0;

                // Set the poop's sorting order
                poopRenderer.sortingOrder = duckSortingOrder - 1;
            }
            else
            {
                Debug.LogError("Poop prefab is missing a SpriteRenderer component!");
            }
        }
        else
        {
            Debug.LogError("Poop prefab is not assigned!");
        }
    }
}
