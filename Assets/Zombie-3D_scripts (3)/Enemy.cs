using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player object
    public float moveSpeed = 3f;  // Movement speed of the enemy
    public float rotationSpeed = 5f;  // Rotation speed of the enemy

    void Update()
    {
        // Calculate the direction from the enemy to the player
        Vector3 direction = player.position - transform.position;

        // Set y to 0 to restrict rotation to the Y axis
        direction.y = 0;

        // Calculate the target rotation of the enemy towards the player
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Rotate the enemy towards the player using Slerp interpolation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Move the enemy towards the player
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
    
    void OnCollisionEnter(Collision col)
    {
        // Debug.Log("OnCollisionEnter2D " + col.collider.tag);
        if (col.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        } 
    }
}