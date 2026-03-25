using UnityEngine;
using UnityEngine.AI; 


public class EnemyKnockback : MonoBehaviour
{
    private Rigidbody rb;        // ใช้ฟิสิกส์ในการผลัก
    private NavMeshAgent agent;  // ใช้ควบคุมการเดิน AI

    void Awake()
    {
        rb = GetComponent<Rigidbody>();       // ดึง Rigidbody จากตัวศัตรู
        agent = GetComponent<NavMeshAgent>(); // ดึง NavMeshAgent (ถ้ามี)
    }

    //  ฟังก์ชันโดนยิงแล้วกระเด็น
    public void ApplyKnockback(Vector3 shootDirection, float force)
    {
        if (rb == null) return; // ถ้าไม่มี Rigidbody → ทำอะไรไม่ได้

        //  ปิด NavMesh ชั่วคราว
        // เพราะ NavMesh จะพยายาม "ดึงตัวกลับ" ทำให้ไม่กระเด็น
        if (agent != null)
        {
            agent.enabled = false;
        }

        //  รีเซ็ตความเร็วเดิมก่อน (กันแรงสะสม)
        rb.velocity = Vector3.zero;

        //  ใส่แรงกระเด็นไปตามทิศที่ยิง
        rb.AddForce(shootDirection.normalized * force, ForceMode.Impulse);

        //  รอ 0.5 วิ แล้วเปิด NavMesh กลับ
        Invoke(nameof(EnableAgent), 0.5f);
    }

    //  เปิด NavMesh กลับมาใช้งาน
    void EnableAgent()
    {
        if (agent != null)
        {
            agent.enabled = true;
        }
    }
}