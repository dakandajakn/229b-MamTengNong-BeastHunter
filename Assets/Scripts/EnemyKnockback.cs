using UnityEngine;
using UnityEngine.AI; // ใช้ NavMesh

public class EnemyKnockback : MonoBehaviour
{
    private Rigidbody rb;
    private NavMeshAgent agent;

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // ดึง Rigidbody
        agent = GetComponent<NavMeshAgent>(); // ดึง NavMeshAgent (ถ้ามี)
    }

    public void ApplyKnockback(Vector3 shootDirection, float force)
    {
        if (rb == null) return;

        // 🔥 ปิด NavMesh ชั่วคราว (ไม่งั้นมันดึงกลับ)
        if (agent != null)
        {
            agent.enabled = false;
        }

        // รีเซ็ตความเร็วก่อน
        rb.velocity = Vector3.zero;

        // ใส่แรงกระเด็น
        rb.AddForce(shootDirection.normalized * force, ForceMode.Impulse);

        // เปิด NavMesh กลับหลังจาก 0.5 วิ
        Invoke(nameof(EnableAgent), 0.5f);
    }

    void EnableAgent()
    {
        if (agent != null)
        {
            agent.enabled = true;
        }
    }
}