using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(10f, 10f, -10f); // Isometric offset
    [SerializeField] private float smoothSpeed = 5f;

    // Start is called before the first frame update
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player == null)
            {
                Debug.LogWarning("Player not found! Please assign the player reference in the inspector.");
            }
        }
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (player == null) return;

        // Calculate the desired position with offset
        Vector3 desiredPosition = player.position + offset;
        
        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Optional: Make the camera look at the player
        transform.LookAt(player);
    }
}
