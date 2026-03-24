using UnityEngine;

public class HPFollow : MonoBehaviour
{
    public Transform target; // มังกร
    public Vector3 offset = new Vector3(0, 4f, 0);

    void Update()
    {
        if (target == null) return;

        // ให้ HP bar อยู่บนหัวมังกร
        transform.position = target.position + offset;

        // หันเข้าหากล้อง
        transform.forward = Camera.main.transform.forward;
    }
}