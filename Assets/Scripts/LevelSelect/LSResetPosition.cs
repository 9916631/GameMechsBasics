using UnityEngine;

public class LSResetPosition : MonoBehaviour
{
    // Create a static instance variable to ensure only one instance of this class exists.
    public static LSResetPosition instance;

    // Public variable for the respawn position.
    public Vector3 respawnPosition;

    // Awake method is called before the Start method.
    private void Awake()
    {
        // Set the static instance variable to this instance of the LSResetPosition class.
        instance = this;
    }

    // Called when another collider enters the trigger collider.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has the "Player" tag.
        if (other.tag == "Player")
        {
            // Deactivate the player's GameObject to prevent unexpected behavior during the reset.
            PlayerController.instance.gameObject.SetActive(false);

            // Set the player's position to the respawnPosition.
            PlayerController.instance.transform.position = respawnPosition;

            // Reactivate the player's GameObject.
            PlayerController.instance.gameObject.SetActive(true);

            // Log a message indicating that the respawn has occurred.
            Debug.Log("Respawned");
        }
    }
}
