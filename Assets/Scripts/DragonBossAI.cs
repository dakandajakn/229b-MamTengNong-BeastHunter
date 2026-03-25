using UnityEngine;


public class DragonBossAI : MonoBehaviour
{
    public Transform player;          // อ้างอิงตำแหน่ง Player
    public float attackRange = 10f;   // ระยะที่มังกรจะเริ่มโจมตี
    public float attackCooldown = 2f; // เวลาหน่วงระหว่างการโจมตีแต่ละครั้ง
    public int damage = 10;           // ดาเมจที่ทำกับ Player

    private Animator animator;        // ใช้ควบคุม Animation
    private float attackTimer;        // ตัวนับเวลา cooldown
    private bool isAttacking;         // เช็คว่ากำลังโจมตีอยู่ไหม

    private Vector3 startPos;         // เก็บตำแหน่งเริ่มต้น (ไว้ล็อกไม่ให้ขยับ)

    void Start()
    {
        animator = GetComponent<Animator>(); // ดึง Animator จากตัวมังกร
        startPos = transform.position;       // จำตำแหน่งเริ่มต้น
    }

    void Update()
    {
        if (player == null) return; // ถ้าไม่มี Player → ไม่ทำงาน

        // คำนวณระยะห่างระหว่างมังกรกับ Player
        float distance = Vector3.Distance(transform.position, player.position);

        //  หันหน้าไปทาง Player ตลอดเวลา
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;            // ไม่ให้ก้ม/เงย (หมุนแค่แกน Y)
        transform.forward = direction;

        //  ถ้า Player อยู่ในระยะโจมตี
        if (distance <= attackRange)
        {
            attackTimer += Time.deltaTime; // นับเวลาเพิ่ม

            // ถ้าครบ cooldown และยังไม่ได้โจมตีอยู่
            if (attackTimer >= attackCooldown && !isAttacking)
            {
                StartAttack(); // เริ่มโจมตี
            }
        }
    }

    void LateUpdate()
    {
        //  ล็อกตำแหน่งไม่ให้มังกรขยับ (ยืนอยู่กับที่ตลอด)
        transform.position = startPos;
    }

    void StartAttack()
    {
        isAttacking = true; // ตั้งค่าว่ากำลังโจมตีอยู่
        attackTimer = 0f;   // รีเซ็ตเวลา cooldown

        //  สุ่มท่าโจมตี (0,1,2)
        int attackType = Random.Range(0, 3);

        // ส่งค่าไปที่ Animator เพื่อเล่น Animation
        animator.SetInteger("AttackType", attackType);
        animator.SetTrigger("Attack");

        //  หน่วงเวลาให้ตรงกับจังหวะที่ Animation ตีโดน
        Invoke(nameof(DealDamage), 0.7f);

        //  จบการโจมตี (ให้สามารถโจมตีรอบถัดไปได้)
        Invoke(nameof(StopAttack), 1.5f);
    }

    void DealDamage()
    {
        if (player == null) return;

        // เช็คระยะอีกครั้งตอนตี (กัน Player วิ่งหนี)
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            Debug.Log("ตีโดน!");

            // ดึงสคริปต์เลือดของ Player
            PlayerHealth hp = player.GetComponent<PlayerHealth>();

            // ถ้ามี → ลดเลือด Player
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }
        }
    }

    void StopAttack()
    {
        isAttacking = false; // ปลดล็อกให้โจมตีรอบใหม่ได้
    }
}