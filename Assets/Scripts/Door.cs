using UnityEngine;

public class Door : MonoBehaviour
{
    public EnemySpawner enemySpawner;  

    void Update()
    {
        if (enemySpawner != null && enemySpawner.enemyCount <= 0)
        {
            Destroy(gameObject);  // Destruye la puerta cuando no quedan enemigos
        }
    }
}