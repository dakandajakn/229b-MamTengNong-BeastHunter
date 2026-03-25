using UnityEngine;

// สคริปต์ควบคุมการเดิน Player (เดินตามกล้อง + หมุน + อนิเมชัน)
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;          // ความเร็วในการเดิน
    public Transform cameraTransform;    // อ้างอิงกล้อง (ใช้กำหนดทิศทางเดิน)
    public Animator anim;                // Animator สำหรับเล่นอนิเมชัน

    void Update()
    {
        //  รับ input จากคีย์บอร์ด (WASD)
        float moveX = Input.GetAxis("Horizontal"); // A / D
        float moveZ = Input.GetAxis("Vertical");   // W / S

        //  รับการหมุนจากเมาส์ (แกน X)
        float mouseX = Input.GetAxis("Mouse X") * 200f * Time.deltaTime;

        //  หมุนตัว Player ซ้าย-ขวา
        transform.Rotate(Vector3.up * mouseX);

        //  เอาทิศทางของกล้องมาใช้ (เพื่อให้เดินตามมุมกล้อง)
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        //  ตัดแกน Y ออก (ไม่ให้ตัวละครลอยขึ้น/ลง)
        forward.y = 0;
        right.y = 0;

        //  คำนวณทิศทางการเดิน (หน้า/หลัง + ซ้าย/ขวา)
        Vector3 moveDirection = forward * moveZ + right * moveX;

        //  ขยับตำแหน่ง Player
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        //  ถ้ามีการเคลื่อนที่ → หมุนตัวไปตามทิศที่เดิน
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // หมุนแบบนุ่ม ๆ (Smooth)
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }

        //  ควบคุมอนิเมชัน
        if (anim != null)
        {
            // ส่งค่าความเร็วให้ Animator (ใช้เปลี่ยน Idle / Walk / Run)
            anim.SetFloat("Speed", moveDirection.magnitude);
        }
    }
}