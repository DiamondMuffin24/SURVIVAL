using UnityEngine;
using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2.0f;
    public float waitTime = 2.0f;

    public Transform player; // Reference to the player
    public float chaseDistance = 5.0f;
    public float stopChasingDistance = 7.0f;

    private int currentWaypointIndex = 0;
    private bool isWaiting = false;
    private bool isChasing = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (waypoints.Length > 0 && player != null)
        {
            StartCoroutine(BehaviorLoop());
        }
    }

    IEnumerator BehaviorLoop()
    {
        while (true)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (isChasing)
            {
                if (distanceToPlayer > stopChasingDistance)
                {
                    isChasing = false;
                }
                else
                {
                    ChasePlayer();
                }
            }
            else
            {
                if (distanceToPlayer < chaseDistance)
                {
                    isChasing = true;
                }
                else
                {
                    if (!isWaiting)
                    {
                        Patrol();
                        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
                        {
                            StartCoroutine(WaitAtWaypoint());
                        }
                    }
                }
            }

            yield return null;
        }
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        Vector2 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        FlipSprite(direction);
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        FlipSprite(direction);
    }

    void FlipSprite(Vector2 direction)
    {
        if (direction.x != 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Sign(direction.x) * Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}
