using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 20.0f;
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

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
