/*
Box Collider with no IsTrigger:
Description: A Box Collider is a Unity component that defines a box-shaped 3D collision volume around an object. When "IsTrigger" is not checked, it means that the collider is used for physical interactions, and it will respond to collisions by applying forces or preventing objects from passing through.
Purpose: It is typically used for objects that need to interact with the physics system, such as detecting collisions, responding to forces, or being affected by gravity.

Box Collider with IsTrigger Checked:
Description: When "IsTrigger" is checked for a Box Collider, it means that the collider will act as a trigger zone rather than a physical collider. It won't cause physical interactions like bouncing off other colliders or blocking movement. Instead, it will trigger events in scripts when other colliders enter or exit the trigger zone.
Purpose: It is commonly used for creating areas that trigger specific actions or events in response to other objects entering or leaving the trigger zone. For example, it can be used to detect when a player enters a specific area without physically interacting with them.

Rigidbody:
Description: The Rigidbody is a Unity component that allows a GameObject to be affected by Unity's physics engine. It adds physics behavior such as gravity, forces, and collisions to the GameObject.
Purpose: It is used for making objects move and interact realistically in the physics simulation. When a GameObject has a Rigidbody component, it will respond to forces, gravity, and collisions, making it suitable for characters, projectiles, or any object that needs realistic physical behavior in the game world. 
*/

/*
OnTriggerEnter:
Description: OnTriggerEnter is a method in Unity that is automatically called when a Collider enters a trigger zone attached to a GameObject.
Purpose: This method is commonly used in Unity scripts to detect when another GameObject with a Collider enters a 
designated trigger area. It is often employed for implementing gameplay interactions, such as collecting items, triggering events,
or initiating specific behaviors when one GameObject comes into contact with another within a designated trigger zone. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().TakeDamage();
        }
    }
}