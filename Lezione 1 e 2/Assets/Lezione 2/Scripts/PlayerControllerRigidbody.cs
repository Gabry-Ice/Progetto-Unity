using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerRigidbody : MonoBehaviour {
    [Header("Movement")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float sprintMultiplier = 1.3f;
    [Tooltip("Accelerazione del personaggio. Maggiore è il valore, più velocemente il personaggio raggiungerà la velocità massima")]
    [SerializeField] private float acceleration = 10f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 500f;

    [Header("Jump")]
    [SerializeField] private bool allowJump = true;
    [SerializeField] private float jumpPower = 30;
    [SerializeField] private int maxNumberOfJumps = 2;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float groundCheckDistance = 0.35f;
    private float jumpCooldown = 0.01f;

    [Header("Physics")]
    [SerializeField] private float gravityMultiplier = 500f;
    [SerializeField] private float airControlFactor = 0.8f;

    public int numberOfJumps;
    private Vector2 input;
    private Rigidbody rb;
    private Vector3 direction;
    private float currentSpeed;
    public bool isSprinting;
    public bool isGrounded;
    private Camera _mainCamera;
    private float lastJumpTime;
    private bool wasGroundedLastFrame;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void FixedUpdate() {
        CheckGrounded();
        ApplyRotation();
        ApplyMovement();
        ApplyGravity();
    }

    private void CheckGrounded() {
        wasGroundedLastFrame = isGrounded;

        RaycastHit hit;
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * 0.5f), transform.position.z);
        bool sphereCastHit = Physics.SphereCast(spherePosition, groundCheckRadius, Vector3.down, out hit, groundCheckDistance, groundLayers);

        // Cooldown per evitare di errore di collisione con il terreno
        if (Time.time - lastJumpTime < jumpCooldown) {
            isGrounded = false;
        } else {
            isGrounded = sphereCastHit;
        }

        // Resetta il numero di salti se il personaggio è a terra
        if (isGrounded && rb.linearVelocity.y <= 0.01f && !wasGroundedLastFrame) {
            numberOfJumps = 0;
        }
    }

    private void ApplyGravity() {
        if (!isGrounded) {
            // Gravità extra per rendere il salto più realistico
            Vector3 extraGravity = Vector3.up * Physics.gravity.y * (gravityMultiplier - 1) * Time.fixedDeltaTime;
            rb.AddForce(extraGravity, ForceMode.Acceleration);
        }
    }

    private void ApplyRotation() {
        if (input.sqrMagnitude == 0) return;

        direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(input.x, 0.0f, input.y);
        direction.Normalize();

        var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    private void ApplyMovement() {
        var targetSpeed = isSprinting ? speed * sprintMultiplier : speed;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);

        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        Vector3 targetVelocity = direction * currentSpeed;

        // Controllo in volo ridotto
        float controlFactor = isGrounded ? 1f : airControlFactor;

        Vector3 velocityChange = (targetVelocity - horizontalVelocity) * controlFactor;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    public void Move(InputAction.CallbackContext context) {
        input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0.0f, input.y);
    }

    public void Jump(InputAction.CallbackContext context) {
        if (allowJump && context.started) {
            if (isGrounded || numberOfJumps < maxNumberOfJumps) {
                numberOfJumps++;
                lastJumpTime = Time.time;

                // Azzera la velocità verticale se già in salto per evitare accumulo di forza
                Vector3 velocity = rb.linearVelocity;
                if (velocity.y < 0)
                    velocity.y = 0;
                rb.linearVelocity = velocity;

                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }
    }

    public void Sprint(InputAction.CallbackContext context) {
        isSprinting = context.started || context.performed;
    }

    // Debug visivo nell'editor
    private void OnDrawGizmosSelected() {
        if (isGrounded) {
            Gizmos.color = Color.red;
        } else {
            Gizmos.color = Color.green;
        }
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * 0.5f), transform.position.z);
        Gizmos.DrawWireSphere(spherePosition - Vector3.up * groundCheckDistance, groundCheckRadius);
    }
}