using UnityEngine;
using UnityEngine.InputSystem;
//using static Unity.Cinemachine.InputAxisControllerBase<T>;

public class PlayerControls : MonoBehaviour
{
    public  bool controlsOn = true;

    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float velocityReservationRatio = 0.9f;
    [SerializeField] private float walkAcceleration = 0.25f;
    [SerializeField] private float runAcceleration = 0.6f;


    [SerializeField] private Animator animator;

    private Transform ts;
    private Transform cts;
    private Rigidbody rb;
    private CharacterController controller;

    private float movementSpeed;
    private float movementAcceleration;
    private float currentHorizontalVelocity;
    private float horizontalAccelerationInput;

    private Vector3 playerVelocity;
    private bool groundedPlayer = false;

    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        movementSpeed = walkSpeed;
        movementAcceleration = walkAcceleration;
        ts = transform;
        cts = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        animator.Play("Idle Walk Run Blend");
        animator.SetFloat("MotionSpeed", 1);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        horizontalAccelerationInput = context.ReadValue<Vector2>().x;
        if(context.canceled)
        {
            horizontalAccelerationInput = 0;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            animator.SetBool("Jump", true);
            animator.SetBool("FreeFall", false);
            animator.SetBool("Grounded", false);
            groundedPlayer = false;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        movementSpeed = runSpeed;
        movementAcceleration = runAcceleration;
        if (context.canceled)
        {
            movementSpeed = walkSpeed;
            movementAcceleration = walkAcceleration;
        }
    }

    void Update()
    {
        if (!controlsOn)
        {
            return;
        }
        playerVelocity.x += (horizontalAccelerationInput * movementAcceleration);
        float maxPlayerSpeedAbs = movementSpeed;
        playerVelocity.x = Mathf.Clamp(playerVelocity.x, -maxPlayerSpeedAbs, maxPlayerSpeedAbs);

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        
        animator.SetFloat("Speed", Mathf.Abs(playerVelocity.x + controller.velocity.x * 2) * 0.32f);
        controller.Move(playerVelocity * Time.deltaTime);
        playerVelocity.x *= velocityReservationRatio;

        if (groundedPlayer)
        {
            animator.SetBool("Jump", false);
            if (controller.isGrounded)
            {

                animator.SetBool("FreeFall", false);
                animator.SetBool("Grounded", true);
            }
            else
            {
                animator.SetBool("FreeFall", true);
                animator.SetBool("Grounded", false);
                groundedPlayer = controller.isGrounded;
            }
        }
        else if (controller.isGrounded)
        {
            groundedPlayer = true;
        }

        if (Mathf.Abs(horizontalAccelerationInput) >= 1)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180 -Mathf.Rad2Deg * Mathf.Asin((Mathf.Clamp(controller.velocity.x / walkSpeed, -1, 1))), 0);
        }
    }
}
