using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMortality : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float currentHealth = 100;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void ChangeHealth(float change)
    {
        currentHealth += change;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Die()
    {
        if (LoseMenu.instance != null)
        {
            LoseMenu.instance.Open();
        }
        else
        {
            Debug.LogWarning("Player lost, but LoseMenu not found");
        }
        if(TryGetComponent<PlayerControls>(out PlayerControls controls))
        {
            controls.controlsOn = false;
        }
    }
}
