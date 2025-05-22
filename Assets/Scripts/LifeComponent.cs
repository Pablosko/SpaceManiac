using UnityEngine;
using UnityEngine.Events;

public class LifeComponent : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Events")]
    public UnityEvent onDeath;
    public UnityEvent onDamage;  // NUEVO evento para da�o

    public AudioClip dieSound;

    private AudioSource audioSource;

    void Awake()
    {
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0f) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        onDamage?.Invoke();  // Invocar evento da�o cada vez que recibe da�o

        if (currentHealth <= 0f)
        {
            DieSound();
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        onDeath?.Invoke();
        Destroy(gameObject);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
    }

    private void DieSound()
    {
        if (dieSound != null)
            audioSource.PlayOneShot(dieSound);
    }

    public bool IsAlive => currentHealth > 0f;
}