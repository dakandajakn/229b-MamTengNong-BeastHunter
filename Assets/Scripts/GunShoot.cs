using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public float damage = 20f;
    public float range = 100f;

    public Camera fpsCam;
    public Animator anim;
    public GameObject Effects;

    public Transform firePoint; // จุดยิง (จุดเขียว)
    public float knockbackForce = 2f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        anim.Play("Attack01");

        // ❗ ไม่ให้ยิงโดน Player
        int layerMask = ~LayerMask.GetMask("Player");

        RaycastHit hit;

        // 🎯 ยิงจากกลางจอ
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, range, layerMask))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(range);
        }

        // 💥 เอฟเฟคที่จุดโดน
        Instantiate(Effects, targetPoint, Quaternion.identity);

        // 🔫 คำนวณทิศยิงจากตัวละคร
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        // 🔍 ยิงจริงอีกทีจาก firePoint
        if (Physics.Raycast(firePoint.position, direction, out hit, range, layerMask))
        {
            Debug.Log("Hit: " + hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            EnemyKnockback knockback = hit.transform.GetComponentInParent<EnemyKnockback>();
            if (knockback != null)
            {
                knockback.ApplyKnockback(direction, knockbackForce);
            }
        }
    }
}