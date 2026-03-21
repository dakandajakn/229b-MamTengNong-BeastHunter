using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    float currentHealth;

    public Image Fill;

    void Start()
    {
        currentHealth = maxHealth;
        Fill.fillAmount = 1f;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Fill.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}