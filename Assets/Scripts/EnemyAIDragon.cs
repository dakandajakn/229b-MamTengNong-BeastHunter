using UnityEngine;

public class EnemyAIDragon : MonoBehaviour
{
    public Transform player; // ตัวผู้เล่น

    public float attackDistance = 5f; // ระยะโจมตี
    public float attackCooldown = 2f; // คูลดาวน์ตี

    float attackTimer;

    public int damage = 10;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); // ดึง Animator
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position); // วัดระยะ

        attackTimer += Time.deltaTime; // นับเวลา

        // 👁 หันหน้าหาผู้เล่นตลอด
        transform.LookAt(player);

        // 🔥 ถ้าอยู่ในระยะ → โจมตี
        if (distance <= attackDistance)
        {
            if (attackTimer >= attackCooldown)
            {
                anim.SetTrigger("Attack"); // เล่นอนิเมชั่นโจมตี
                AttackPlayer(); // ทำดาเมจ
                attackTimer = 0f; // รีเซ็ตเวลา
            }
        }
    }

    void AttackPlayer()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage); // ลดเลือด
    }
}