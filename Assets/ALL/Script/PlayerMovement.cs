using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Camera")]
    public Transform playerCamera;
    public float mouseSensitivity = 150f;

    private CharacterController controller;
    private float xRotation = 0f;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
    }

    void HandleMovement()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Ground check
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f); // batas kepala tidak 360°

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
