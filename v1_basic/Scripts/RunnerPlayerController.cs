using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RunnerPlayerController : MonoBehaviour
{
    [Header("Forward")]
    [SerializeField] private float forwardSpeed = 8f;

    [Header("Lane")]
    [SerializeField] private float laneOffset = 2.5f;
    [SerializeField] private float laneChangeSpeed = 10f;

    [Header("Jump")]
    [SerializeField] private float jumpLength = 5f;
    [SerializeField] private float jumpHeight = 2.5f;
    [SerializeField] private float groundY = 1f;

    private Rigidbody rb;
    private RunnerGameController gameController;
    private int currentLane = 1;
    private bool isJumping;
    private float jumpStartZ;
    private Vector3 motionTarget = new Vector3(0f, 1f, 0f);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        gameController = FindFirstObjectByType<RunnerGameController>();
    }

    private void Update()
    {
        if (gameController == null || gameController.State != RunnerGameController.GameState.Running)
        {
            return;
        }

        HandleInput();
        UpdateJumpArc();
        UpdateLanePosition();
    }

    private void FixedUpdate()
    {
        if (gameController == null || gameController.State != RunnerGameController.GameState.Running)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        rb.linearVelocity = Vector3.forward * forwardSpeed;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            BeginJump();
        }
    }

    private void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, 0, 2);
        motionTarget.x = (currentLane - 1) * laneOffset;
    }

    private void BeginJump()
    {
        if (isJumping)
        {
            return;
        }

        jumpStartZ = transform.position.z;
        isJumping = true;
    }

    private void UpdateJumpArc()
    {
        if (!isJumping)
        {
            motionTarget.y = Mathf.MoveTowards(motionTarget.y, groundY, 10f * Time.deltaTime);
            return;
        }

        float progress = (transform.position.z - jumpStartZ) / jumpLength;
        if (progress >= 1f)
        {
            isJumping = false;
            motionTarget.y = groundY;
            return;
        }

        motionTarget.y = groundY + Mathf.Sin(progress * Mathf.PI) * jumpHeight;
    }

    private void UpdateLanePosition()
    {
        Vector3 targetPosition = new Vector3(motionTarget.x, motionTarget.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Death"))
        {
            return;
        }

        if (gameController != null)
        {
            gameController.TriggerGameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Coin"))
        {
            return;
        }

        if (gameController != null)
        {
            gameController.AddCoin();
        }

        Destroy(other.gameObject);
    }
}
