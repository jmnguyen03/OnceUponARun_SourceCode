using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 15f;
    private bool isGrounded = true;

    public float forwardSpeed = 10f;
    public float speedStep = 3f;
    public float minSpeed = 2f;
    public float maxSpeed = 25f;

    public float laneDistance = 3f; // Distance between lanes
    private int currentLane = 1;    // 0 = left, 1 = center, 2 = right

    private Rigidbody rb;

    public float fallMultiplier = 2.5f;      // Base fall multiplier
    public float fallSpeedScaling = 0.1f;    // Extra scaling with forwardSpeed
    public int maxScore;

    public MainGameManager gameManager;
    private int lastScoreMilestone = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Lane switching
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
            currentLane--;
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2)
            currentLane++;

        // Speed control
        /*if (Input.GetKeyDown(KeyCode.UpArrow))
            forwardSpeed = Mathf.Min(forwardSpeed + speedStep, maxSpeed);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            forwardSpeed = Mathf.Max(forwardSpeed - speedStep, minSpeed);
        */

        // Automatically increase speed every 20 points
        int currentScore = ScoreManager.instance.score;

        if (currentScore >= lastScoreMilestone + 20)
        {
            forwardSpeed = Mathf.Min(forwardSpeed + 3f, maxSpeed);
            lastScoreMilestone += 20;
            Debug.Log("Speed increased to: " + forwardSpeed);
        }

    }

    void FixedUpdate()
    {
        // Adjust fall multiplier based on forwardSpeed
        if (rb.linearVelocity.y < 0) // Falling
        {
            float dynamicFallMultiplier = fallMultiplier + (forwardSpeed * fallSpeedScaling);
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (dynamicFallMultiplier - 1) * Time.fixedDeltaTime;
        }

        // Calculate the target X position based on lane
        float targetX = (currentLane - 1) * laneDistance;
        float newX = Mathf.Lerp(rb.position.x, targetX, Time.fixedDeltaTime * 10f);

        // Move forward at variable speed
        Vector3 targetPosition = new Vector3(newX, rb.position.y, rb.position.z + forwardSpeed * Time.fixedDeltaTime);
        rb.MovePosition(targetPosition);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            //gameManager.LoseLife();
        }
    }
}
