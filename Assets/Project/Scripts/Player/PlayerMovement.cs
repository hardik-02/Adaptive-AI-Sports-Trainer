using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public InputActionReference moveAction;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float rotationSpeed = 12f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        moveAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
    }

    void Update()
    {
        MoveRelativeToCamera();
        ApplyGravity();
    }

    void MoveRelativeToCamera()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // Forward direction from camera (ignore Y)
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        // Right direction from camera
        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        // Movement direction
        Vector3 moveDir = camForward * input.y + camRight * input.x;

        if (moveDir.magnitude < 0.1f) return;

        // Smooth rotation towards movement direction
        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
