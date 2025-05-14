using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject[] doors;  // Array con las puertas de la sala
    public GameObject enemySpawner;  // Referencia al EnemySpawner de la sala

    private bool isPlayerInside = false;

    public GameObject rewardDropPrefab;

    private bool rewardSpawned = false;

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

    private void Update()
    {
        if (!rewardSpawned && enemySpawner.GetComponent<EnemySpawner>().enemyCount == 0)
        {
            rewardDropPrefab.SetActive(true);
            rewardSpawned = true;  // Marcar que ya activamos la recompensa
        }
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

    void SpawnReward()
    {
        if (rewardDropPrefab == null) return;

        Vector3 spawnPos = transform.position; // O la posición deseada (puerta, centro, etc)
        GameObject drop = Instantiate(rewardDropPrefab, spawnPos, Quaternion.identity);

        // Asignar recompensa aleatoria
        RewardDrop reward = drop.GetComponent<RewardDrop>();
        if (reward != null)
        {
            int randomIndex = Random.Range(0, 4);
            reward.rewardType = (RewardDrop.RewardType)randomIndex;
        }
    }
}
