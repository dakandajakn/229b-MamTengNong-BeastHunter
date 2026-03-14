using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 10f; // เพิ่มตัวแปรความเร็วในการหันหน้า
    public Animator anim;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // สร้างทิศทางจากปุ่มที่กด
        Vector3 movement = new Vector3(moveX, 0f, moveZ);

        // ถ้ามีการกดปุ่มเดิน (ค่าไม่เท่ากับ 0)
        if (movement != Vector3.zero)
        {
            // 1. สั่งให้ตัวละครค่อยๆ หมุนหน้าไปหาทิศที่จะเดินไป
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // 2. สั่งให้ตัวละครพุ่งไป "ข้างหน้า" (พุ่งไปตามหน้าของมันที่เพิ่งหันไป)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);

            // 3. สั่งเล่นท่าเดิน
            if (anim != null) anim.SetFloat("Speed", 1f);
        }
        else
        {
            // ถ้าปล่อยปุ่ม ให้หยุดแล้วเล่นท่ายืน
            if (anim != null) anim.SetFloat("Speed", 0f);
        }
    }
}
