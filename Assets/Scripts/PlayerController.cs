/*
Camera:
Description: In Unity, the Camera is a component that represents the virtual camera through which the game is viewed.
Purpose: Cameras in Unity are used to render scenes from a specific perspective. They can be moved, rotated, and configured to control how the game world is displayed to the player.

CharacterController:
Description: CharacterController is a Unity component that provides a simple way to handle character movement and collisions without the need for rigidbody physics.
Purpose: Used for controlling characters in the game, CharacterController allows for precise movement and handling of character interactions with the environment.

Animator:
Description: Animator is a Unity component used for creating and controlling animations.
Purpose: Animators are essential for bringing game characters and objects to life by defining and managing animation states, transitions, and parameters. They are often used in conjunction with character models to control their movements and appearances.

Vector2:
Description: Vector2 is a structure in Unity representing a point or direction in 2D space.
Purpose: Used for 2D positional and directional calculations, Vector2 is commonly used to represent coordinates, velocities, and other 2D vectors in Unity.

Mathf:
Description: Mathf is a Unity class that provides a collection of static methods for common mathematical operations.
Purpose: Used for performing common mathematical calculations in Unity, Mathf includes methods for functions like trigonometry, logarithms, interpolation, and clamping. It's particularly useful for game development tasks involving physics, animations, and general math operations.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Singleton instance of the PlayerController
    public static PlayerController instance;

    // Variables for movement and control
    public float boinceForce = 8;
    public float moveSpeed;
    public float jumpForce;
    public float garvityScale = 5f;
    private Vector3 moveDirection;

    // Reference to the CharacterController component
    public CharacterController charController;

    // Reference to the main camera
    private Camera theCam;

    // References for player model and animation
    public GameObject playerModel;
    public float rotateSpeed;
    public Animator Anim;

    // Variables for knockback effect
    public bool isKnocking;
    public float knockBackLength = .5f;
    private float knockBackCounter;
    public Vector2 knockbackPower;

    // Array of player pieces and flag to stop movement
    public GameObject[] PlayerPieces;
    public bool stopMove;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the main camera
        theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is not knocking and movement is not stopped
        if (!isKnocking && !stopMove)
        {
            // Store the y-component of the moveDirection
            float yStore = moveDirection.y;

            // Calculate the moveDirection based on player input
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = yStore;

            // Check if the player is grounded
            if (charController.isGrounded)
            {
                moveDirection.y = 0f;

                // Check for jump input
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }

            // Apply gravity to the moveDirection
            moveDirection.y += Physics.gravity.y * Time.deltaTime * garvityScale;
            charController.Move(moveDirection * Time.deltaTime);

            // Rotate the player based on input and camera orientation
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }
        }

        // Check if the player is knocking
        if (isKnocking)
        {
            // Decrease the knockBackCounter
            knockBackCounter -= Time.deltaTime;

            // Store the y-component of the moveDirection
            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockbackPower.x;
            moveDirection.y = yStore;

            // Check if the player is grounded
            if (charController.isGrounded)
            {
                moveDirection.y = 0f;

                // Check for jump input
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = 0f;
                }
            }

            // Apply gravity to the moveDirection
            moveDirection.y += Physics.gravity.y * Time.deltaTime * garvityScale;

            // Move the character controller
            charController.Move(moveDirection * Time.deltaTime);

            // Check if the knockBackCounter has reached zero
            if (knockBackCounter <= 0)
            {
                isKnocking = false;
            }
        }

        // Check if movement is stopped
        if (stopMove)
        {
            moveDirection = Vector3.zero;
            moveDirection.y += Physics.gravity.y * Time.deltaTime * garvityScale;
            charController.Move(moveDirection);
        }

        // Update animator parameters based on movement
        Anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        Anim.SetBool("Grounded", charController.isGrounded);
    }

    // Method to initiate knockback
    public void KnockBack()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        Debug.Log("Player knocked back");
        moveDirection.y = knockbackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }

    // Method to make the player bounce
    public void Bounce()
    {
        moveDirection.y = boinceForce;
        Debug.Log("Player Bounced");
        charController.Move(moveDirection * Time.deltaTime);
    }
}
