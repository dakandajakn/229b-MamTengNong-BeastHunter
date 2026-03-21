using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyKnockback(Vector3 shootDirection, float force)
    {
        if (rb == null) return;

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(shootDirection.normalized * force, ForceMode.Impulse);
    }
}
