using UnityEngine;
using UnityEngine.UI;

public class DragonHealth : MonoBehaviour
{
    public float maxHealth = 200f;
    private float currentHealth;

    public Image hpFill;

    void Start()
    {
        currentHealth = maxHealth;

        if (hpFill != null)
        {
            hpFill.fillAmount = 1f;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (hpFill != null)
        {
            hpFill.fillAmount = currentHealth / maxHealth;
        }

        Debug.Log("🐉 เลือดมังกร: " + currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("🐉 มังกรตาย!");
        Destroy(transform.root.gameObject);
    }
}