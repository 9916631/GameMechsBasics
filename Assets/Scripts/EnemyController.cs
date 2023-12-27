/*
NavMeshAgent:
Description: NavMeshAgent is a component in Unity that enables GameObjects to navigate through a scene using Unity's built-in navigation system (NavMesh).
Purpose: It calculates a path based on the scene's NavMesh and moves the GameObject along that path, making it suitable for characters or objects that need to navigate intelligently in the game world.

Transform:
Description: Transform is a component in Unity that represents the position, rotation, and scale of a GameObject.
Purpose: It allows manipulation of the GameObject's position, rotation, and scale. It's fundamental for controlling the spatial aspects of GameObjects in the 3D space.

enum (Enumeration):
Description: An enum is a data type in programming languages that represents a set of named values (constants).
Purpose: Enums are useful for creating a set of distinct states or options. In the provided script, the AIState enum is used to define different states for the enemy's behavior (Idle, Patrolling, Chasing, Attacking).

switch case:
Description: switch is a control flow statement in programming that selects one of many code blocks to be executed.
Purpose: It's used to control the flow of the program based on the value of an expression. In the provided script, 
switch is used to handle different cases for the enemy's behavior based on its current state (Idle, Patrolling, Chasing,
Attacking). Each case represents a different behavior, and the code inside the corresponding case block is executed based on the current state. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;  // Array of patrol points for the enemy
    public int currentPatrolPoint;    // Index of the current patrol point

    public NavMeshAgent agent;        // Reference to the NavMeshAgent component
    public Animator anim;             // Reference to the Animator component
    
    public AIState currentState;       // Current state of the AI
    
    // Enum to represent different states of the AI
    public enum AIState
    {
        isIdle, isPatrolling, isChasing, isAttacking
    };    

    // Variables for idle and attack behavior
    public float waitAtPoint = 2f;    // Time to wait at a patrol point
    private float waitCounter;         // Counter for waiting at a patrol point
    public float chaseRange;           // Distance to start chasing the player
    public float attackRange = 1f;     // Distance to attack the player
    public float timeBetweenattacks = 2f; // Time between attacks
    private float attackCounter;       // Counter for time between attacks
    private bool isonfloor;            // Flag to check if the enemy is on the floor

    void Start()
    {
        waitCounter = waitAtPoint;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        // State machine for the AI behavior
        switch (currentState)
        {
            case AIState.isIdle:
                anim.SetBool("IsMoving", false);

                // If waiting time is not over, continue waiting
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    // If waiting time is over, start patrolling
                    currentState = AIState.isPatrolling;
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                }

                // If the player is within chase range, start chasing
                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.isChasing;
                    anim.SetBool("IsMoving", true);
                }
                break;

            case AIState.isPatrolling:
                // If the enemy reaches the current patrol point, switch to idle state
                if (agent.remainingDistance <= 2f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;
                }

                // If the player is within chase range, switch to chasing state
                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.isChasing;
                }
                anim.SetBool("IsMoving", true);
                break;

            case AIState.isChasing:
                // Set destination to the player's position
                agent.SetDestination(PlayerController.instance.transform.position);

                // If the player is within attack range, switch to attacking state
                if (distanceToPlayer <= attackRange)
                {
                    currentState = AIState.isAttacking;
                    anim.SetTrigger("Attack");
                    anim.SetBool("IsMoving", false);
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;

                    attackCounter = timeBetweenattacks;
                }

                // If the player is outside of chase range, switch to idle state
                if (distanceToPlayer > chaseRange)
                {
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;
                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                }
                break;

            case AIState.isAttacking:
                // Look at the player and reset rotation to avoid tilting
                transform.LookAt(PlayerController.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                // Countdown for time between attacks
                attackCounter -= +Time.deltaTime;

                // If time between attacks is over, and the player is still in attack range, trigger attack animation
                if (attackCounter <= 0)
                {
                    if (distanceToPlayer < attackRange)
                    {
                        anim.SetTrigger("Attack");
                        attackCounter = timeBetweenattacks;
                    }
                    else
                    {
                        // If the player is out of attack range, switch to idle state
                        currentState = AIState.isIdle;
                        waitCounter = waitAtPoint;
                        agent.isStopped = false;
                    }
                }
                break;

            default:
                break;
        }
    }

    // Triggered when the enemy collides with another collider
    public void OnTriggerEnter(Collider other)
    {
        // Check if the collider has the tag "floor" and the enemy is not yet on the floor
        if ((other.tag == "floor") && (isonfloor == false))
        {
            // Reset NavMeshAgent to ensure proper ground navigation
            agent.enabled = false;
            agent.enabled = true;
            isonfloor = true;
        }
    }
}
