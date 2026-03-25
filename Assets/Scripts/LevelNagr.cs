using UnityEngine;
using UnityEngine.SceneManagement;

// คลาสนี้ใช้สำหรับเปลี่ยน Scene เมื่อ Player เดินชน Trigger
public class LevelNagr : MonoBehaviour
{
    // ชื่อ Scene ที่จะโหลด (ไปตั้งค่าใน Inspector)
    public string SceneName;

    // ฟังก์ชันสำหรับเรียกเปลี่ยน Scene แบบ manual (เช่น กดปุ่ม)
    public void Loadscene()
    {
        SceneManager.LoadScene(SceneName); // โหลด Scene ตามชื่อที่กำหนด
    }

    // ฟังก์ชันนี้จะทำงานเมื่อมี Object เข้ามาชน Trigger
    private void OnTriggerEnter(Collider other)
    {
        // เช็คว่า Object ที่ชนคือ Player หรือไม่
        if (other.gameObject.CompareTag("Player"))
        {
            // ถ้าใช่ → เปลี่ยน Scene ทันที
            SceneManager.LoadScene(SceneName);
        }
    }
}