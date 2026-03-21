using UnityEngine; 

public class EnemyAI : MonoBehaviour // สร้าง Script EnemyAI ให้ใช้กับ Enemy
{
    public Transform player; // เก็บตำแหน่ง Player เพื่อให้มอนรู้ว่าผู้เล่นอยู่ตรงไหน

    public float speed = 3f; // ความเร็วเดินของมอน
    public float chaseDistance = 10f; // ระยะที่มอนจะเริ่มเดินตาม
    public float attackDistance = 1f; // ระยะที่มอนจะเริ่มโจมตี

    public float attackCooldown = 1.5f; // เวลาหน่วงการตี (ตีทุก 1.5 วินาที)
    float attackTimer; // ตัวแปรจับเวลา

    public int damage = 10; // ดาเมจที่มอนจะตีใส่ Player

    Animator anim; // ตัวแปรเก็บ Animator เพื่อควบคุม Animation

    void Start() // ทำงานครั้งเดียวตอนเริ่มเกม
    {
        anim = GetComponent<Animator>(); // หา Animator จาก Enemy แล้วเก็บไว้
    }

    void Update() // ทำงานทุกเฟรม
    {
        // คำนวณระยะห่างระหว่าง Enemy กับ Player
        float distance = Vector3.Distance(transform.position, player.position);

        attackTimer += Time.deltaTime; // นับเวลาเพิ่มทุกเฟรม

        // ถ้า Player อยู่ในระยะไล่
        if (distance <= chaseDistance)
        {
            transform.LookAt(player); // ให้มอนหันหน้าไปหาผู้เล่น

            // ถ้า Player ยังอยู่ไกลกว่าระยะตี
            if (distance > attackDistance)
            {
                // ให้มอนเดินไปหาผู้เล่น
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    player.position,
                    speed * Time.deltaTime
                );

                anim.SetBool("Run", true); // เล่น Animation วิ่ง
            }
            else
            {
                anim.SetBool("Run", false); // หยุดวิ่ง

                // ถ้าถึงเวลาตี
                if (attackTimer >= attackCooldown)
                {
                    anim.SetTrigger("Attack"); // เล่น Animation ตี

                    AttackPlayer(); // เรียกฟังก์ชันตี Player

                    attackTimer = 0f; // รีเซ็ตเวลา
                }
            }
        }
        else
        {
            anim.SetBool("Run", false); // ถ้า Player ไกลให้หยุด
        }
    }

    // ฟังก์ชันตี Player
    void AttackPlayer()
    {
        // ไปเรียก Script PlayerHealth
        // แล้วลด HP ตาม damage
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
}