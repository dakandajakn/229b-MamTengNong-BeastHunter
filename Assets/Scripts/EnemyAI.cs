using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float chaseDistance = 10f;   // ระยะเห็นผู้เล่น
    public float attackDistance = 1.5f; // ระยะตี

    public float patrolRadius = 8f;     // เดินสุ่มไกลแค่ไหน
    public float patrolDelay = 3f;      // เปลี่ยนจุดทุกกี่วิ

    public float attackCooldown = 1.5f; // เวลารอการตี
    float attackTimer;

    NavMeshAgent agent;
    Animator anim;

    float patrolTimer;
    Vector3 patrolPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        patrolPoint = transform.position; // เริ่มจากจุดเดิม
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        attackTimer += Time.deltaTime;

        // 👁 เห็นผู้เล่น
        if (distance <= chaseDistance)
        {
            // 🏃 ไล่
            if (distance > attackDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);

                anim.SetBool("Run", true);
            }
            // 💥 ตี
            else
            {
                agent.isStopped = true;

                transform.LookAt(player);

                anim.SetBool("Run", false);

                if (attackTimer >= attackCooldown)
                {
                    anim.SetTrigger("Attack");
                    AttackPlayer();
                    attackTimer = 0f;
                }
            }
        }
        // 🌲 ไม่เห็น → เดินสุ่ม
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        patrolTimer += Time.deltaTime;

        // ถ้าถึงจุด หรือ ครบเวลา → สุ่มใหม่
        if (Vector3.Distance(transform.position, patrolPoint) < 1f || patrolTimer >= patrolDelay)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * patrolRadius;
            randomPos.y = transform.position.y;

            patrolPoint = randomPos;
            patrolTimer = 0f;
        }

        agent.isStopped = false;
        agent.SetDestination(patrolPoint);

        anim.SetBool("Run", true);
    }

    void AttackPlayer()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(10);
    }
}