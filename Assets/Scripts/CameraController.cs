using System.Collections;
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

    private Vector3 originalPosition;  // Para restaurar después del shake
    private Coroutine shakeCoroutine;

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

    public void ShakeCamera(float duration = 0.3f, float magnitude = 0.2f)
    {
        // Comprobar si el shake está habilitado (1 = activado, 0 = desactivado)
        if (PlayerPrefs.GetInt("ShakeEnabled", 1) == 0)
            return; // No hacer shake si está desactivado

        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        float elapsed = 0f;
        Vector3 originalPos = transform.position;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }
}