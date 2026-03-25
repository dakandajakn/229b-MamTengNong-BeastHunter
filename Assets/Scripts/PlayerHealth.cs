using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 100f;
    private float currentHP;

    public Image hpFill; // ✅ เปลี่ยนเป็น Image

    void Start()
    {
        currentHP = maxHP;

        if (hpFill != null)
            hpFill.fillAmount = 1f;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);

        float percent = currentHP / maxHP;

        if (hpFill != null)
            hpFill.fillAmount = percent;

        Debug.Log("❤️ Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("💀 Game Over");
    }
}