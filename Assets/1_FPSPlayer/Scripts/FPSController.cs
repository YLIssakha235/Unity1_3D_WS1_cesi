using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    public float WalkSpeed = 6.0f;
    public float RunningSpeed = 12.0f;
    public float JumpSpeed = 8.0f;
    public float Gravity = 20.0f;

    public float LookSpeed = 2.0f;
    public float LookXLimit = 45.0f;

    private CharacterController m_characterController;
    private Camera m_cPlayerCamera;

    private Vector3 m_v3MoveDirection = Vector3.zero;
    private float m_fRotationX = 0;
    private Vector2 m_v2Look;
    private Vector2 m_v2Move;
    private Vector2 m_v2Rotation;

    private bool m_bCanMove = true;
    private bool m_bIsJump = false;
    private bool m_bIsRunning = false;

    public void OnLook(InputAction.CallbackContext context)
    {
        m_v2Look = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_v2Move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        m_bIsJump = context.ReadValueAsButton();
    }

    public void OnRunning(InputAction.CallbackContext context)
    {
        m_bIsRunning = context.ReadValueAsButton();
    }

    private void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_cPlayerCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = m_bCanMove ? (m_bIsRunning ? RunningSpeed : WalkSpeed) * m_v2Move.y : 0;
        float curSpeedY = m_bCanMove ? (m_bIsRunning ? RunningSpeed : WalkSpeed) * m_v2Move.x : 0;

        float movementDirectionY = m_v3MoveDirection.y;

        m_v3MoveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (m_bIsJump && m_bCanMove && m_characterController.isGrounded)
        {
            m_v3MoveDirection.y = JumpSpeed;
        }
        else
        {
            m_v3MoveDirection.y = movementDirectionY;
        }

        if (!m_characterController.isGrounded)
        {
            m_v3MoveDirection.y -= Gravity * Time.deltaTime;
        }

        m_characterController.Move(m_v3MoveDirection * Time.deltaTime);

        if (m_bCanMove)
        {
            m_fRotationX += -m_v2Look.y * LookSpeed * Time.deltaTime;
            m_fRotationX = Mathf.Clamp(m_fRotationX, -LookXLimit, LookXLimit);

            m_cPlayerCamera.transform.localRotation = Quaternion.Euler(m_fRotationX, 0, 0);

            transform.rotation *= Quaternion.Euler(0, m_v2Look.x * LookSpeed * Time.deltaTime, 0);
        }
    }
}