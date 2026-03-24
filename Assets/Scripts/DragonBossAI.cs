using UnityEngine;

public class DragonBossAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 10f;
    public float attackCooldown = 2f;
    public int damage = 10;

    private Animator animator;
    private float attackTimer;
    private bool isAttacking;

    private Vector3 startPos; // ล็อกตำแหน่ง

    void Start()
    {
        animator = GetComponent<Animator>();
        startPos = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // หันหน้า
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        transform.forward = direction;

        // อยู่ในระยะ = เตรียมโจมตี
        if (distance <= attackRange)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown && !isAttacking)
            {
                StartAttack();
            }
        }
    }

    void LateUpdate()
    {
        // ล็อกตำแหน่งไม่ให้ขยับ
        transform.position = startPos;
    }

    void StartAttack()
    {
        isAttacking = true;
        attackTimer = 0f;

        int attackType = Random.Range(0, 3);

        animator.SetInteger("AttackType", attackType);
        animator.SetTrigger("Attack");

        // ⏱️ จังหวะตีโดน (ปรับตาม animation)
        Invoke(nameof(DealDamage), 0.7f);

        Invoke(nameof(StopAttack), 1.5f);
    }

    void DealDamage()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            Debug.Log("🐉 ตีโดน!");

            PlayerHealth hp = player.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }
        }
    }

    void StopAttack()
    {
        isAttacking = false;
    }
}