using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float forceMagnitude = 10.0f;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameEvents.OnRestartEvent += ResetBall;

        GameEvents.OnLevelCompleteEvent += ResetBall;

        GameEvents.OnLevelFailEvent += ResetBall;

        GameEvents.OnStartGame += ResetBall;
    }

    private void OnDestroy()
    {
        GameEvents.OnRestartEvent -= ResetBall;

        GameEvents.OnLevelCompleteEvent -= ResetBall;

        GameEvents.OnLevelFailEvent -= ResetBall;

        GameEvents.OnStartGame -= ResetBall;
    }

    private void ResetBall()
    {
        // Reset velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

  /*  private void OnCollisionEnter(Collision collision)
    {
        // Check if the ball has hit a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            AudioManager.Instance.PlaySFX(AudioClipName.Hit);
        }
    }*/

    void FixedUpdate()
    {
        // Create a force vector based on the arrow key inputs
        Vector3 force = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * forceMagnitude;

        // Apply the force to the ball in the direction of the arrow keys
        rb.AddForce(force);
    }
}
