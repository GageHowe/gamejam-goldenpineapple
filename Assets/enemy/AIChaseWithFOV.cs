using UnityEngine;

public class AIChaseWithFOV : MonoBehaviour
{
    public float speed = 3f;
    public float viewDistance = 7f;
    [Range(0, 360)] public float viewAngle = 60f;
    public float rotationSpeed = 200f;
    public float patrolRadius = 5f;
    public float patrolInterval = 3f;
    public float searchDuration = 5f;
    public float driftReduction = 0.1f;

    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    Rigidbody2D rb;

    enum State { Patrol, Search, Chase }
    State currentState = State.Patrol;

    Vector2 patrolTarget;
    Vector2? lastKnownPlayerPos = null;
    float stateTimer = 0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseNewPatrolTarget();
    }

    void FixedUpdate()
    {
        var playerCol = DetectPlayer();
        if (playerCol != null)
        {
            Vector2 playerPos = (Vector2)playerCol.transform.position;
            UpdateLastKnownPosition(playerPos);
            currentState = State.Chase;
            stateTimer = 0f;
        }
        else if (currentState == State.Chase)
        {
            currentState = State.Search;
            stateTimer = 0f;
        }

        switch (currentState)
        {
            case State.Chase:
                ChasePlayer();
                break;
            case State.Search:
                SearchLastKnownPosition();
                break;
            case State.Patrol:
                Patrol();
                break;
        }

        rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f, 10f * Time.fixedDeltaTime);
    }

    Collider2D DetectPlayer()
    {
        var playerCol = Physics2D.OverlapCircle(transform.position, viewDistance, playerLayer);
        if (playerCol != null)
        {
            Vector2 toPlayer = (Vector2)playerCol.transform.position - (Vector2)transform.position;
            if (Vector2.Angle(transform.up, toPlayer) < viewAngle * 0.5f)
            {
                var hit = Physics2D.Raycast(transform.position, toPlayer.normalized, viewDistance, playerLayer | obstacleLayer);
                if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & playerLayer) != 0)
                    return playerCol;
            }
        }
        return null;
    }

    void UpdateLastKnownPosition(Vector2 playerPos)
    {
        lastKnownPlayerPos = playerPos;
        stateTimer = 0f;
    }

    void ChasePlayer()
    {
        if (lastKnownPlayerPos == null) { currentState = State.Patrol; return; }

        Vector2 direction = lastKnownPlayerPos.Value - rb.position;
        if (direction.magnitude < 0.2f)
        {
            currentState = State.Search;
            stateTimer = 0f;
        }
        else
        {
            direction.Normalize();

            // Reduce velocity component not aligned with target direction (reduce drift)
            Vector2 velocityAlongDir = Vector2.Dot(rb.linearVelocity, direction) * direction;
            Vector2 correctionVelocity = rb.linearVelocity - velocityAlongDir;
            rb.linearVelocity -= correctionVelocity * driftReduction; // Adjust multiplier for smoothness

            MoveAndRotate(direction);
        }
    }


    void SearchLastKnownPosition()
    {
        if (lastKnownPlayerPos == null)
        {
            currentState = State.Patrol;
            return;
        }

        Vector2 targetDirection = lastKnownPlayerPos.Value - rb.position;

        if (targetDirection.magnitude > 0.2f)
        {
            MoveAndRotate(targetDirection.normalized);
        }
        else
        {
            rb.MoveRotation(rb.rotation + rotationSpeed * 0.5f * Time.fixedDeltaTime);
        }

        stateTimer += Time.fixedDeltaTime;

        if (stateTimer >= searchDuration)
        {
            lastKnownPlayerPos = null;
            currentState = State.Patrol;
            ChooseNewPatrolTarget();
        }
    }

    void Patrol()
    {
        Vector2 direction = patrolTarget - rb.position;

        if (direction.magnitude < 0.2f || stateTimer >= patrolInterval)
            ChooseNewPatrolTarget();
        else
            MoveAndRotate(direction.normalized);

        stateTimer += Time.fixedDeltaTime;
    }

    void ChooseNewPatrolTarget()
    {
        patrolTarget = rb.position + Random.insideUnitCircle * patrolRadius;
        stateTimer = 0f;
    }

    void MoveAndRotate(Vector2 moveDir)
    {
        // Check obstacles ahead with CircleCast
        float checkDistance = 1f;
        RaycastHit2D hit = Physics2D.CircleCast(rb.position, 0.2f, moveDir, checkDistance, obstacleLayer);
        if (hit.collider != null)
        {
            // Obstacle detected - try to steer around
            Vector2 perpDir = Vector2.Perpendicular(moveDir);
            if (Physics2D.CircleCast(rb.position, 0.2f, perpDir, checkDistance, obstacleLayer))
            {
                perpDir = -perpDir; // steer other way if blocked
            }
            moveDir = perpDir;
        }

        rb.AddForce(moveDir * speed, ForceMode2D.Force);
        RotateTowards(moveDir);
    }


    void RotateTowards(Vector2 targetDir)
    {
        float angleDiff = Vector2.SignedAngle(transform.up, targetDir);
        float maxRotation = rotationSpeed * Time.fixedDeltaTime;
        float angle = Mathf.Clamp(angleDiff, -maxRotation, maxRotation);
        rb.MoveRotation(rb.rotation + angle);
    }
}
