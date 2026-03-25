using UnityEngine;
using UnityEngine.UI;              // ใช้กับ UI (หลอดเลือด)
using UnityEngine.SceneManagement; // ใช้เปลี่ยน Scene


public class DragonHealth : MonoBehaviour
{
    public float maxHealth = 200f; // เลือดสูงสุดของมังกร
    private float currentHealth;   // เลือดปัจจุบัน

    public Image hpFill; // รูป UI (แบบ Fill) สำหรับแสดงหลอดเลือด

    void Start()
    {
        currentHealth = maxHealth; // ตั้งค่าเลือดเริ่มต้น = เต็ม

        // ถ้ามีการใส่หลอดเลือดใน Inspector
        if (hpFill != null)
        {
            hpFill.fillAmount = 1f; // ให้หลอดเต็ม (100%)
        }
    }

    //  ฟังก์ชันรับดาเมจ (เรียกจากปืน)
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // ลดเลือด

        // กันค่าไม่ให้ต่ำกว่า 0 หรือเกิน max
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // อัปเดตหลอดเลือด
        if (hpFill != null)
        {
            hpFill.fillAmount = currentHealth / maxHealth;
            // เช่น เลือดเหลือ 100/200 = 0.5 (ครึ่งหลอด)
        }

        Debug.Log("เลือดมังกร: " + currentHealth);

        //  ถ้าเลือดหมด → ตาย
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    //  ฟังก์ชันตอนมังกรตาย
    void Die()
    {
        Debug.Log("มังกรตาย!");

        //  ไปฉากจบ / เครดิต
        SceneManager.LoadScene("Eng");

        // (ไม่จำเป็นต้อง Destroy เพราะเปลี่ยนฉากแล้ว)
        // Destroy(transform.root.gameObject);
    }
}