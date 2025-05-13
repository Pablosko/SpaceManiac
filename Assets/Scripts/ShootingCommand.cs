using UnityEngine;

public class ShootingCommand : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Shooting Config")]
    public float attackSpeed = 5f; // bullets per second
    public int bullets = 1;
    [Range(0f, 360f)]
    public float arc = 0f; // angle in degrees

    private float fireCooldown = 0f;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && fireCooldown <= 0f)
        {
            Fire();
            fireCooldown = 1f / attackSpeed;
        }
    }

    public void Fire()
    {
        if (bulletPrefab == null || firePoint == null)
            return;

        float angleStep = (bullets > 1) ? arc / (bullets - 1) : 0f;
        float startAngle = -arc / 2f;

        for (int i = 0; i < bullets; i++)
        {
            float angle = startAngle + i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector2 direction = rotation * firePoint.up; // ← usa forward de la nave

            GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            BulletProjectile bullet = bulletGO.GetComponent<BulletProjectile>();
            bullet.SetDirection(direction);
        }
    }
}
