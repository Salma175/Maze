using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 20.0f;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       // rb.maxDepenetrationVelocity = 10;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
