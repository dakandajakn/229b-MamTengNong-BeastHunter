using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // เอาไว้ใส่ตัวละครของเรา
    public Vector3 offset = new Vector3(0f, 2.5f, -4f); // ระยะห่างของกล้อง (Y=ความสูง, Z=ความห่างด้านหลัง)
    public float followSpeed = 5f; // ความเร็วที่กล้องจะวิ่งตาม

    // เราใช้ LateUpdate แทน Update สำหรับกล้อง เพื่อให้กล้องขยับตาม "หลังจาก" ที่ตัวละครเดินเสร็จแล้ว ภาพจะได้ไม่สั่น
    void LateUpdate()
    {
        if (target != null)
        {
            // 1. คำนวณตำแหน่งที่กล้องควรไปอยู่ (ด้านหลังเป้าหมาย ตามระยะ offset)
            Vector3 targetPosition = target.position + (target.rotation * offset);

            // 2. สั่งให้กล้องค่อยๆ ลอยไปที่ตำแหน่งนั้นแบบนุ่มนวล (Lerp)
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // 3. สั่งให้กล้องหันไปมองที่ตัวละครเสมอ (บวกความสูงขึ้นมาหน่อย จะได้มองเห็นตรงช่วงหัว ไม่ใช่มองที่เท้า)
            transform.LookAt(target.position + Vector3.up * 1.5f);
        }
    }
}