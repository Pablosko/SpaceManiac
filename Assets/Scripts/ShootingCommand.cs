using UnityEngine;
using UnityEngine.Audio;

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
    private readonly float fixedCooldown = 0.5f;  // Cooldown fijo de 0.5 segundos

    public AudioClip playerShootSound;

    private AudioSource audioSource;

    public AudioMixer audioMixer;
    public string audioMixerGroupName = "SFX";

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (audioMixer != null)
        {
            // Obtener el AudioMixerGroup con el nombre especificado
            AudioMixerGroup[] groups = audioMixer.FindMatchingGroups(audioMixerGroupName);
            if (groups.Length > 0)
            {
                audioSource.outputAudioMixerGroup = groups[0];
                Debug.Log($"AudioSource asignado al grupo '{audioMixerGroupName}'");
            }
        }
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && fireCooldown <= 0f)
        {
            Fire();
            fireCooldown = fixedCooldown; // Reiniciamos cooldown fijo
            PlayShootSound();
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

    private void PlayShootSound()
    {
        if (playerShootSound != null)
            audioSource.PlayOneShot(playerShootSound);
    }

}
