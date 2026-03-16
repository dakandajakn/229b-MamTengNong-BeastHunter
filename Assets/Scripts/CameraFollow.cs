using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float mouseSensitivity = 200f;
    public Vector3 offset = new Vector3(0, 3, -5);

    float mouseX;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(0, mouseX, 0);

        transform.position = target.position + rotation * offset;

        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}