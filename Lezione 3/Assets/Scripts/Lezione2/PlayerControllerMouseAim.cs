using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerMouseAim : MonoBehaviour {

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [Tooltip("Accelerazione del personaggio. Maggiore è il valore, più velocemente il personaggio raggiungerà la velocità massima")]
    [SerializeField] private float acceleration = 50f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 500f;
    [Tooltip("Se attivo, il player ruota sempre verso il cursore del mouse")]
    [SerializeField] private bool aimWithMouse = true;

    [Header("Jump")]
    [SerializeField] private bool allowJump = true;
    [SerializeField] private float jumpPower = 8;
    [SerializeField] private int maxNumberOfJumps = 1;
    [SerializeField] private float gravityMultiplier = 2f;

    private int numberOfJumps;

    private Vector2 input;
    private CharacterController characterController;
    private Vector3 direction;

    public float currentSpeed;
    private Vector3 moveDirection;
    private float velocity;

    private float gravity = -9.81f;

    public bool isSprinting;

    private Camera mainCamera;
    // public Animator animator;

    private Vector3 mouseWorldPosition;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        // animator = GetComponentInChildren<Animator>();
    }

    private void Update() {
        UpdateCameraRelativeDirection();
        UpdateMouseWorldPosition();
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
    }

    private void UpdateCameraRelativeDirection() {
        if (input.sqrMagnitude == 0) {
            moveDirection = Vector3.zero;
            return;
        }

        moveDirection = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f)
                        * new Vector3(input.x, 0f, input.y);
    }

    private void UpdateMouseWorldPosition() {
        if (!aimWithMouse) return;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float distance)) {
            mouseWorldPosition = ray.GetPoint(distance);
        }
    }

    private void ApplyGravity() {
        if (IsGrounded() && velocity < 0f) {
            velocity = -1f;
        } else {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }
    }

    private void ApplyRotation() {
        if (aimWithMouse) {
            // In aria con mouse aim: si ruota comunque verso il cursore
            Vector3 lookDir = mouseWorldPosition - transform.position;
            lookDir.y = 0f;
            if (lookDir.sqrMagnitude > 0.001f) {
                Quaternion targetRotation = Quaternion.LookRotation(lookDir, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        } else {
            // In aria senza mouse aim: nessuna rotazione (si mantiene quella pre-salto)
            if (!IsGrounded()) return;

            if (moveDirection.sqrMagnitude < 0.001f) return;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void ApplyMovement() {
        var targetSpeed = isSprinting ? speed * sprintMultiplier : speed;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        Vector3 horizontalMove = moveDirection * currentSpeed;
        Vector3 verticalMove = Vector3.up * velocity;

        characterController.Move((horizontalMove + verticalMove) * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context) {
        input = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context) {
        if (allowJump && context.started) {
            if (IsGrounded() || numberOfJumps < maxNumberOfJumps) {
                if (numberOfJumps == 0) StartCoroutine(WaitForLanding());

                numberOfJumps++;
                velocity = jumpPower;
            }
        }
    }

    public void Sprint(InputAction.CallbackContext context) {
        isSprinting = context.started || context.performed;
    }

    private IEnumerator WaitForLanding() {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);
        numberOfJumps = 0;
    }

    private bool IsGrounded() => characterController.isGrounded;
}