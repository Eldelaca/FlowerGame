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

    // Gravity
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    // Ground Check
    public Transform groundCheck; // Transform position to check ground
    public float groundDistance = 0.2f; // Distance for ground check
    public LayerMask groundLayer; // Layer mask to define what is considered ground
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

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Keeps player grounded
        }

        // Movement
        float ad = Input.GetAxisRaw("Horizontal");
        float ws = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(ad, 0, ws).normalized;

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply movement with gravity
        controller.Move(velocity * Time.deltaTime);
    }
}
