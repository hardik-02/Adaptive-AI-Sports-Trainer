using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    [Header("Input")]
    public InputActionReference lookAction;

    [Header("Camera")]
    public Vector3 offset = new Vector3(0, 2.5f, -4f);
    public float sensitivity = 2.5f;

    private float xRotation = 15f;

    private void OnEnable()
    {
        lookAction.action.Enable();
    }

    private void OnDisable()
    {
        lookAction.action.Disable();
    }

    private void LateUpdate()
    {
        if (!target) return;

        Vector2 look = lookAction.action.ReadValue<Vector2>();

        float mouseX = look.x * sensitivity * Time.deltaTime;
        float mouseY = look.y * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -20f, 40f);

        transform.RotateAround(target.position, Vector3.up, mouseX * 100f);

        Vector3 desiredPosition =
            target.position +
            Quaternion.Euler(xRotation, transform.eulerAngles.y, 0) * offset;

        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}