using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public float damage = 20f;
    public float range = 100f;

    public Camera fpsCam;
    public Animator anim;
    public GameObject effects;

    public Transform firePoint;
    public float knockbackForce = 2f;
    public AudioSource audioSource;
    public AudioClip shootSound;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        if (fpsCam == null || firePoint == null)
        {
            Debug.LogWarning("GunShoot: fpsCam หรือ firePoint ยังไม่ได้ใส่ใน Inspector");
            return;
        }

        if (anim != null)
        {
            anim.Play("Attack01");
        }

        int layerMask = ~LayerMask.GetMask("Player");

        RaycastHit hit;
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, range, layerMask))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(range);
        }

        if (effects != null)
        {
            Instantiate(effects, targetPoint, Quaternion.identity);
        }

        Vector3 direction = (targetPoint - firePoint.position).normalized;

        if (Physics.Raycast(firePoint.position, direction, out hit, range, layerMask))
        {
            Debug.Log("Hit: " + hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            DragonHealth dragon = hit.transform.GetComponentInParent<DragonHealth>();
            if (dragon != null)
            {
                Debug.Log("โดนมังกรแล้ว!");
                dragon.TakeDamage(damage);
            }

            EnemyKnockback knockback = hit.transform.GetComponentInParent<EnemyKnockback>();
            if (knockback != null)
            {
                knockback.ApplyKnockback(direction, knockbackForce);
            }
        }
    }
}