using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float mouseSensitivity = 200f;
    public Vector3 offset = new Vector3(0, 2.5f, -2.5f);

    float yaw;
    float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 🔥 ล็อคมุมเริ่ม = ค่าจากรูปคุณ
        yaw = -24.36f;   // Y
        pitch = 1.81f;   // X
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;

        pitch = Mathf.Clamp(pitch, -30f, 60f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // ตำแหน่งกล้อง
        transform.position = target.position + rotation * offset;

        // มุมกล้อง
        transform.rotation = rotation;
    }
}