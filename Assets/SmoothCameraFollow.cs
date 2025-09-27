using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public string playerTag = "Player"; // Tag used to identify player objects
    public Vector3 offset = new Vector3(0, 0, -10); // Default camera offset
    public float smoothTime = 0.3f; // Smooth follow duration

    private Transform target; // Reference to player transform
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // Find the player GameObject by tag dynamically on start
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player object with tag '" + playerTag + "' not found. Please add the tag or player to the scene.");
        }
    }

    void LateUpdate()
    {
        // If the player is found, follow smoothly
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
