using UnityEngine;

public class PlayerMovement : MonoBehaviour, IGameLevelObserver
{
    private float forceMagnitude = 10.0f;
    private Rigidbody rb;
    private bool _canMove = true;
    private IGameDataManager _gameDataManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        _gameDataManager = ServiceLocator.Get<IGameDataManager>();

        _gameDataManager.RegisterObserver(this);

        GameEvents.OnStartGame += StartGame;

        GameEvents.OnRestartEvent += StartGame;

        GameEvents.OnLevelCompleteEvent += ResetBall;

        GameEvents.OnLevelFailEvent += ResetBall;
    }

    private void OnDestroy()
    {
        GameEvents.OnRestartEvent -= StartGame;

        GameEvents.OnLevelCompleteEvent -= ResetBall;

        GameEvents.OnLevelFailEvent -= ResetBall;

        GameEvents.OnStartGame -= StartGame;

        _gameDataManager.UnregisterObserver(this);
    }

    private void StartGame()
    {
        _canMove = true;
        rb.constraints = RigidbodyConstraints.None;
    }

    private void ResetBall()
    {
        _canMove = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //TODO Smoke Effect
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
        if (_canMove)
        {
            // Create a force vector based on the arrow key inputs
            Vector3 force = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * forceMagnitude;

            // Apply the force to the ball in the direction of the arrow keys
            rb.AddForce(force);
        }
    }

    public void OnGameLevelChanged(int level)
    {
        StartGame();
    }
}
