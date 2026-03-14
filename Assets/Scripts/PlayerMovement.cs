using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator anim; // ลากตัวละครมาใส่ที่ช่องนี้ใน Unity

    void Update()
    {
        // 1. รับค่าการกดปุ่ม (W A S D หรือ ลูกศร)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // 2. ทำให้ตัวละครเคลื่อนที่
        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // 3. ระบบอนิเมชั่น (เดิน กับ หยุด เท่านั้น)
        if (anim != null)
        {
            // ถ้าค่า moveX หรือ moveZ ไม่เท่ากับ 0 แปลว่าเรากำลังกดปุ่มอยู่
            if (moveX != 0 || moveZ != 0)
            {
                // กำลังกดปุ่มเดิน -> ส่งค่า Speed ให้เป็น 1 (เพื่อให้ท่าเดินทำงาน)
                anim.SetFloat("Speed", 1f);
            }
            else
            {
                // ปล่อยปุ่มแล้ว -> ส่งค่า Speed ให้เป็น 0 (เพื่อให้กลับไปท่ายืน)
                anim.SetFloat("Speed", 0f);
            }
        }
    }
}