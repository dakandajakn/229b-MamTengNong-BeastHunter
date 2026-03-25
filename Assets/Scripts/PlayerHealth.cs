using UnityEngine;
using UnityEngine.UI; 


public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 100f; // เลือดสูงสุดของผู้เล่น
    private float currentHP;   // เลือดปัจจุบัน

    public Image hpFill; // UI Image สำหรับแสดงหลอดเลือด (แบบ Fill)

    void Start()
    {
        currentHP = maxHP; // เริ่มต้นเลือดเต็ม

        // ถ้ามีการใส่หลอดเลือดใน Inspector
        if (hpFill != null)
            hpFill.fillAmount = 1f; // ทำให้หลอดเต็ม (100%)
    }

    //  ฟังก์ชันรับดาเมจ (เรียกจากศัตรู)
    public void TakeDamage(float damage)
    {
        currentHP -= damage; // ลดเลือด

        // กันค่าไม่ให้ติดลบ หรือเกิน max
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);

        float percent = currentHP / maxHP; // คำนวณ % เลือดที่เหลือ

        // อัปเดตหลอดเลือด
        if (hpFill != null)
            hpFill.fillAmount = percent;

        Debug.Log("Player HP: " + currentHP);

        //  ถ้าเลือดหมด → ตาย
        if (currentHP <= 0)
        {
            Die();
        }
    }

    //  ฟังก์ชันตอนผู้เล่นตาย
    void Die()
    {
        Debug.Log("Game Over");

        
    }
}