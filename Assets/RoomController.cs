using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject[] doors;  // Array con las puertas de la sala
    public GameObject enemySpawner;  // Referencia al EnemySpawner de la sala

    private bool isPlayerInside = false;

    void Start()
    {
        // Al inicio, desactivamos las puertas y el spawner
        foreach (var door in doors)
        {
            door.SetActive(false);
        }

        if (enemySpawner != null)
            enemySpawner.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerInside)
        {
            isPlayerInside = true;
            ActivateRoom();
            Debug.Log("HA ENTRADO");
        }
    }

    /*void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isPlayerInside)
        {
            isPlayerInside = false;
            DeactivateRoom();
        }
    }*/

    void ActivateRoom()
    {
        foreach (var door in doors)
        {
            door.SetActive(true);
        }

        if (enemySpawner != null)
            enemySpawner.SetActive(true);
    }

    void DeactivateRoom()
    {
        foreach (var door in doors)
        {
            door.SetActive(false);
        }

        if (enemySpawner != null)
            enemySpawner.SetActive(false);
    }
}
