using UnityEngine;

// สคริปต์ปืน: ยิง → Raycast → ทำดาเมจ + เอฟเฟกต์ + เสียง + กระเด็น
public class GunShoot : MonoBehaviour
{
    public float damage = 20f; // ดาเมจต่อการยิง 1 ครั้ง
    public float range = 100f; // ระยะยิงไกลสุด

    public Camera fpsCam;      // กล้อง (ใช้เล็ง)
    public Animator anim;      // อนิเมชันยิง
    public GameObject effects; // เอฟเฟกต์ตอนกระสุนโดน (เช่น ระเบิด/แสง)

    public Transform firePoint;     // จุดยิง (ปลายปืน)
    public float knockbackForce = 2f; // แรงกระเด็น

    public AudioSource audioSource; // ตัวเล่นเสียง
    public AudioClip shootSound;    // เสียงยิง

    void Update()
    {
        //  กดคลิกซ้าย → ยิง
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //  เล่นเสียงยิง
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        //  เช็คว่ามีการใส่กล้องกับจุดยิงหรือยัง
        if (fpsCam == null || firePoint == null)
        {
            Debug.LogWarning("GunShoot: fpsCam หรือ firePoint ยังไม่ได้ใส่ใน Inspector");
            return;
        }

        //  เล่นอนิเมชันยิง
        if (anim != null)
        {
            anim.Play("Attack01");
        }

        //  ไม่ให้ยิงโดน Player (ตัด Layer Player ออก)
        int layerMask = ~LayerMask.GetMask("Player");

        RaycastHit hit;

        // ยิง Ray จาก "กลางหน้าจอ"
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;

        //  ยิงจากกล้องเพื่อหา "จุดที่เล็ง"
        if (Physics.Raycast(ray, out hit, range, layerMask))
        {
            targetPoint = hit.point; // เจอ → ใช้จุดที่โดน
        }
        else
        {
            targetPoint = ray.GetPoint(range); // ไม่เจอ → ยิงไปสุดระยะ
        }

        //  สร้างเอฟเฟกต์ตรงจุดที่ยิงโดน
        if (effects != null)
        {
            Instantiate(effects, targetPoint, Quaternion.identity);
        }

        //  คำนวณทิศทางยิงจากปลายปืนไปยังจุดเป้าหมาย
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        //  ยิงจริงจาก "ปลายปืน"
        if (Physics.Raycast(firePoint.position, direction, out hit, range, layerMask))
        {
            Debug.Log("Hit: " + hit.transform.name);

            //  ถ้าเป็น Enemy ปกติ → ลดเลือด
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            //  ถ้าเป็นมังกร (อยู่ parent) → ลดเลือด
            DragonHealth dragon = hit.transform.GetComponentInParent<DragonHealth>();
            if (dragon != null)
            {
                Debug.Log("โดนมังกรแล้ว!");
                dragon.TakeDamage(damage);
            }

            //  ใส่แรงกระเด็น (Knockback)
            EnemyKnockback knockback = hit.transform.GetComponentInParent<EnemyKnockback>();
            if (knockback != null)
            {
                knockback.ApplyKnockback(direction, knockbackForce);
            }
        }
    }
}