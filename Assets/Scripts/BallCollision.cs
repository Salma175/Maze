using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private float bounceForce = 25.0f;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Calculate the direction opposite to the collision
            Vector3 collisionNormal = collision.contacts[0].normal;
            Vector3 bounceDirection = -collisionNormal;

            // Apply force in the opposite direction
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
