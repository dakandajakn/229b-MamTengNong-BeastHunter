using UnityEngine;

public class GunShoot : MonoBehaviour
{
 
    public float damage = 20f;
    public float range = 100f;
    public Camera fpsCam;
    public Animator anim;
    public GameObject Effects;
    public float knockbackForce = 2f; // เพิ่มแรงกระเด็น

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // คลิกซ้ายยิง
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        anim.Play("Attack01");
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);
            Instantiate(Effects, hit.point, Quaternion.identity);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            EnemyKnockback knockback = hit.transform.GetComponentInParent<EnemyKnockback>();
            if (knockback != null)
            {
                knockback.ApplyKnockback(fpsCam.transform.forward, knockbackForce);
            }
        
            EnemyKnockback monster = hit.collider.GetComponent<EnemyKnockback>();

            if (monster != null)
            {
                Vector3 hitDir = hit.collider.transform.position - transform.position;
                monster.ApplyKnockback(hitDir, knockbackForce);
            }

        }
       
        
    }
}