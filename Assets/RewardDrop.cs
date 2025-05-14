using UnityEngine;

public class RewardDrop : MonoBehaviour
{
    public enum RewardType { Damage, FireRate, Heal, Speed }
    public RewardType rewardType;

    public float damageBonus = 1f;
    public float fireRateBonus = 0.2f;
    public float healAmount = 20f;
    public float speedBonus = 1f;

    private bool isInitialized = false;

   void OnEnable()
    {
        if (!isInitialized)
        {
            // Asignar recompensa aleatoria solo la primera vez que se active
            int randomIndex = Random.Range(0, 4);
            rewardType = (RewardType)randomIndex;
            isInitialized = true;

            // Aquí puedes actualizar visualmente el objeto según el tipo, si quieres
            UpdateVisuals();
        }
    }

    private void UpdateVisuals()
    {
        // Cambia color, sprite, o efectos para mostrar qué tipo es la recompensa
        // Opcional, según diseño artístico
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                ApplyReward(player);
                gameObject.SetActive(false);  // Desactivar objeto tras recoger
                Debug.Log("Picked");
            }
        }
    }

    void ApplyReward(PlayerController player)
    {
        switch (rewardType)
        {
            case RewardType.Damage:
                player.IncreaseDamage(damageBonus);
                break;
            case RewardType.FireRate:
                player.IncreaseFireRate(fireRateBonus);
                break;
            case RewardType.Heal:
                player.Heal(healAmount);
                break;
            case RewardType.Speed:
                player.IncreaseSpeed(speedBonus);
                break;
        }
    }
}