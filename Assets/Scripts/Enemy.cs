using UnityEngine;
using UnityEngine.UI; 



public class Enemy : MonoBehaviour
{
    public float maxHealth = 100; // เลือดสูงสุดของศัตรู
    float currentHealth;          // เลือดปัจจุบัน

    public Image Fill; // UI Image ที่ใช้แสดงหลอดเลือด (แบบ Fill)

    void Start()
    {
        currentHealth = maxHealth; // ตั้งค่าเลือดเริ่มต้น = เต็ม

        Fill.fillAmount = 1f; // ทำให้หลอดเลือดเต็ม (100%)
    }

    // ฟังก์ชันรับดาเมจ (เรียกจากปืน / การโจมตี)
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // ลดเลือดตามดาเมจที่โดน

        // อัปเดตหลอดเลือด (เช่น เหลือครึ่ง = 0.5)
        Fill.fillAmount = currentHealth / maxHealth;

        //  ถ้าเลือดหมดหรือน้อยกว่า 0
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            // ลบศัตรูออกจากเกมทันที
        }
    }
}