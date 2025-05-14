using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float rotationSpeed = 200f;

    [Header("Shooting")]
    public float minShootDelay = 1.5f;
    public float maxShootDelay = 3f;

    private Transform player;
    private Rigidbody2D rb;
    private ShootingCommand shooter;
    private float shootTimer;

    public EnemySpawner enemySpawner;

    void Start()
    {
        var players = FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
        if (players.Length > 0)
            player = players[0].transform;
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<ShootingCommand>();
        ResetShootTimer();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Movimiento hacia el jugador
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        // Rotaci�n hacia el jugador (forward = up en 2D)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float newAngle = Mathf.LerpAngle(rb.rotation, angle, rotationSpeed * Time.fixedDeltaTime / 360f);
        rb.MoveRotation(newAngle);
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f && shooter != null)
        {
            shooter.Fire();
            ResetShootTimer();
        }
    }

    void ResetShootTimer()
    {
        shootTimer = Random.Range(minShootDelay, maxShootDelay);
    }

}
