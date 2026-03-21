using UnityEngine;
using UnityEngine.UI; // ใช้ควบคุม UI HP Bar

public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 100; // HP สูงสุด
    public float currentHP; // HP ปัจจุบัน

    public Slider hpBar; // UI HP Bar

    void Start()
    {
        currentHP = maxHP; // เริ่มเกม HP เต็ม

        hpBar.maxValue = maxHP; // ตั้งค่า Max HP Bar
        hpBar.value = currentHP; // ตั้งค่า HP ปัจจุบัน
    }

    // ฟังก์ชันลด HP
    public void TakeDamage(float damage)
    {
        currentHP -= damage; // ลด HP

        hpBar.value = currentHP; // อัพเดต HP Bar

        if (currentHP <= 0)
        {
            Die(); // ถ้า HP หมด
        }
    }

    void Die()
    {
        Debug.Log("Game Over"); // แสดง Game Over
    }
}