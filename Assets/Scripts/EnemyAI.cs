using UnityEngine;
using UnityEngine.AI; 


public class EnemyAI : MonoBehaviour
{
    public Transform player; // อ้างอิงตำแหน่ง Player

    public float chaseDistance = 10f;   // ระยะที่ศัตรู "เริ่มเห็น" Player
    public float attackDistance = 1.5f; // ระยะที่สามารถโจมตีได้

    public float patrolRadius = 8f;     // รัศมีการเดินสุ่ม
    public float patrolDelay = 3f;      // เปลี่ยนจุดสุ่มทุกกี่วินาที

    public float attackCooldown = 1.5f; // เวลาหน่วงระหว่างการตี
    float attackTimer;                 // ตัวนับเวลาโจมตี

    NavMeshAgent agent; // ตัวควบคุมการเดิน (AI Pathfinding)
    Animator anim;      // ตัวควบคุม Animation

    float patrolTimer;   // ตัวนับเวลาการสุ่มเดิน
    Vector3 patrolPoint; // จุดหมายปลายทางตอนเดินสุ่ม

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // ดึง NavMeshAgent
        anim = GetComponent<Animator>();     // ดึง Animator

        patrolPoint = transform.position; // เริ่มต้นให้ยืนที่เดิมก่อน
    }

    void Update()
    {
        // คำนวณระยะห่างระหว่างศัตรูกับ Player
        float distance = Vector3.Distance(transform.position, player.position);

        attackTimer += Time.deltaTime; // นับเวลาสำหรับ cooldown การโจมตี

        //  ถ้า Player อยู่ในระยะมองเห็น
        if (distance <= chaseDistance)
        {
            // 🏃 ถ้ายังไม่ถึงระยะตี → วิ่งไล่
            if (distance > attackDistance)
            {
                agent.isStopped = false;              // ให้เดินได้
                agent.SetDestination(player.position); // วิ่งไปหา Player

                anim.SetBool("Run", true); // เล่นอนิเมชันวิ่ง
            }
            //  ถ้าเข้าใกล้แล้ว → โจมตี
            else
            {
                agent.isStopped = true; // หยุดเดิน

                transform.LookAt(player); // หันหน้าไปหา Player

                anim.SetBool("Run", false); // หยุดอนิเมชันวิ่ง

                // ถ้าครบเวลาคูลดาวน์
                if (attackTimer >= attackCooldown)
                {
                    anim.SetTrigger("Attack"); // เล่นอนิเมชันตี
                    AttackPlayer();            // ทำดาเมจ
                    attackTimer = 0f;          // รีเซ็ตเวลา
                }
            }
        }
        //  ถ้า "ไม่เห็น Player" → เดินสุ่ม
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        patrolTimer += Time.deltaTime; // นับเวลาเดินสุ่ม

        // ถ้าเดินถึงจุดแล้ว หรือ ครบเวลา → สุ่มจุดใหม่
        if (Vector3.Distance(transform.position, patrolPoint) < 1f || patrolTimer >= patrolDelay)
        {
            // สุ่มตำแหน่งรอบตัวในรัศมีที่กำหนด
            Vector3 randomPos = transform.position + Random.insideUnitSphere * patrolRadius;

            randomPos.y = transform.position.y; // ล็อกแกน Y ไม่ให้ลอย/จม

            patrolPoint = randomPos; // ตั้งเป็นจุดใหม่
            patrolTimer = 0f;        // รีเซ็ตเวลา
        }

        agent.isStopped = false;         // ให้เดินได้
        agent.SetDestination(patrolPoint); // เดินไปยังจุดสุ่ม

        anim.SetBool("Run", true); // เล่นอนิเมชันวิ่ง
    }

    void AttackPlayer()
    {
        // เรียกสคริปต์เลือดของ Player แล้วลดเลือด
        player.GetComponent<PlayerHealth>().TakeDamage(10);
    }
}