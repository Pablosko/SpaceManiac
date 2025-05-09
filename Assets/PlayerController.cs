using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private Rigidbody2D rb;
    private Vector2 inputDirection;
    private Vector2 lastMoveDirection = Vector2.up; // Inicialmente hacia arriba

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Leer input
        inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Solo rota si hay movimiento
        if (inputDirection != Vector2.zero)
        {
            lastMoveDirection = inputDirection;
        }
    }

    void FixedUpdate()
    {
        // Movimiento con físicas
        rb.linearVelocity = inputDirection * moveSpeed;

        // Rotación suave hacia dirección de movimiento
        if (lastMoveDirection != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(lastMoveDirection.y, lastMoveDirection.x) * Mathf.Rad2Deg - 90f;
            float newAngle = Mathf.LerpAngle(rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newAngle);
        }
    }
}
