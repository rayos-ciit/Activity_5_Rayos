using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float lateralSpeed = 7f;
    public float maxBankAngle = 30f;

    void Update()
    {
        // 1. Move forward constantly in world space (Up in 2D)
        transform.Translate(Vector3.up * forwardSpeed * Time.deltaTime, Space.World);

        // 2. Lateral left/right movement
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * lateralSpeed * Time.deltaTime, Space.World);

        // 3. 2D Banking (Rotating on Z using Euler angles)
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -horizontalInput * maxBankAngle);

        // 4. Smoothly tilt the plane using Slerp
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }
}