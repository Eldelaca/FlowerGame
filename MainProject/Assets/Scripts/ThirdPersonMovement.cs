using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    // Movement speed
    public float speed = 6f;

    // Turning Smoothening
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Gravity and Jump
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    // Ground Check
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    // Dash & Movement
    public Vector3 moveDir;

    private Vector3 velocity; // For gravity effect

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        // If grounded, reset the vertical velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Helps keep the player on the ground
        }

        // Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            float jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            velocity.y = jumpVelocity;
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply movement with gravity
        controller.Move(velocity * Time.deltaTime);
    }
}
