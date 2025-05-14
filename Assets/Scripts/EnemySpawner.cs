using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemies")]
    public List<GameObject> enemies;
    public GameObject enemyPrefab;

    [Header("Spawn Settings")]
    public int maxSpawns = 10;
    public float spawnInterval = 2f;
    public float spawnOffset = 2f; // cómo de lejos del borde
    public Vector2 spawnAreaPadding = new Vector2(1f, 1f); // margen interno

    public int enemyCount;

    private float timer;
    private Camera mainCam;
    int count;
    void Start()
    {
        mainCam = Camera.main;
        timer = spawnInterval;

        //enemyCount = maxSpawns;

        enemies = new List<GameObject>();

        // Crear y agregar los enemigos al inicio del nivel
        for (int i = 0; i < maxSpawns; i++)
        {
            SpawnEnemy();  // Llamamos a SpawnEnemy para crear e instanciar el enemigo
        }
    }

    void Update()
    {
        /* timer -= Time.deltaTime;

         if (timer <= 0f && count < maxSpawns)
         {
             SpawnEnemy();
             timer = spawnInterval;
         }*/

        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)  // El enemigo fue destruido
            {
                enemies.RemoveAt(i);
                enemyCount--;
                Debug.Log("Enemigo destruido detectado. Enemigos restantes: " + enemyCount);
            }
        }
    }

    void SpawnEnemy()
    {
       /* if (enemies.Count == 0) return;

        // Elegir un enemigo al azar
        GameObject prefab = enemies[Random.Range(0, enemies.Count)];

        // Elegir un lado (0 = arriba, 1 = abajo, 2 = derecha, 3 = izquierda)
        int side = Random.Range(0, 4);
        Vector2 spawnPos = GetSpawnPosition(side);

        Instantiate(prefab, spawnPos, Quaternion.identity);
        count++;*/

        if (enemies.Count < maxSpawns)
        {
            // Elegir un lado para spawn (puedes ajustarlo a tu necesidad)
            int side = Random.Range(0, 4);
            Vector2 spawnPos = GetSpawnPosition(side);

            // Instanciamos el enemigo en la escena
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            // Agregar el enemigo a la lista
            enemies.Add(enemy);

            // Incrementar el contador de enemigos actuales
            enemyCount++;
        }
    }

    Vector2 GetSpawnPosition(int side)
    {
        Vector3 camPos = mainCam.transform.position;
        float vertExtent = mainCam.orthographicSize;
        float horzExtent = vertExtent * mainCam.aspect;

        switch (side)
        {
            case 0: // arriba
                return new Vector2(
                    Random.Range(camPos.x - horzExtent - spawnAreaPadding.x, camPos.x + horzExtent + spawnAreaPadding.x),
                    camPos.y + vertExtent + spawnOffset
                );
            case 1: // abajo
                return new Vector2(
                    Random.Range(camPos.x - horzExtent - spawnAreaPadding.x, camPos.x + horzExtent + spawnAreaPadding.x),
                    camPos.y - vertExtent - spawnOffset
                );
            case 2: // derecha
                return new Vector2(
                    camPos.x + horzExtent + spawnOffset,
                    Random.Range(camPos.y - vertExtent - spawnAreaPadding.y, camPos.y + vertExtent + spawnAreaPadding.y)
                );
            case 3: // izquierda
                return new Vector2(
                    camPos.x - horzExtent - spawnOffset,
                    Random.Range(camPos.y - vertExtent - spawnAreaPadding.y, camPos.y + vertExtent + spawnAreaPadding.y)
                );
            default:
                return camPos;
        }
    }

}
