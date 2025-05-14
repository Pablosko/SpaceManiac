using UnityEngine;

public class RoomCameraController : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    // Tamaño de la cámara en unidades (half-height, half-width)
    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        targetPosition = transform.position;

        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 camPos = transform.position;

        // Definimos el rectángulo visible de la cámara
        float leftBound = camPos.x - camHalfWidth;
        float rightBound = camPos.x + camHalfWidth;
        float bottomBound = camPos.y - camHalfHeight;
        float topBound = camPos.y + camHalfHeight;

        Vector3 playerPos = player.position;

        // Verificamos si el jugador está fuera del rectángulo visible
        bool outsideHorizontal = playerPos.x < leftBound || playerPos.x > rightBound;
        bool outsideVertical = playerPos.y < bottomBound || playerPos.y > topBound;

        if ((outsideHorizontal || outsideVertical) && !isMoving)
        {
            // Calculamos nueva posición objetivo para la cámara
            Vector3 newCamPos = camPos;

            if (playerPos.x < leftBound)
                newCamPos.x -= camHalfWidth * 2; // Mover cámara a la sala izquierda
            else if (playerPos.x > rightBound)
                newCamPos.x += camHalfWidth * 2; // Mover cámara a la sala derecha

            if (playerPos.y < bottomBound)
                newCamPos.y -= camHalfHeight * 2; // Mover cámara a la sala abajo
            else if (playerPos.y > topBound)
                newCamPos.y += camHalfHeight * 2; // Mover cámara a la sala arriba

            MoveToRoom(newCamPos);
        }

        // Movimiento suave hacia la posición objetivo
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    public void MoveToRoom(Vector3 newPosition)
    {
        targetPosition = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        isMoving = true;
    }
}