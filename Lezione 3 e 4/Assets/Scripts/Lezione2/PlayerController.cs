using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [Tooltip("Accelerazione del personaggio. Maggiore è il valore, più velocemente il personaggio raggiungerà la velocità massima")]
    [SerializeField] private float acceleration = 50f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 500f;

    [Header("Jump")]
    [SerializeField] private bool allowJump = true;
    [SerializeField] private float jumpPower = 4;
    [SerializeField] private int maxNumberOfJumps = 2;
    [SerializeField] private float gravityMultiplier = 1.7f;

    private int numberOfJumps;

    private Vector2 input;
    private CharacterController characterController;
    private Vector3 direction;

    public float currentSpeed;
    private float velocity;
    private float gravity = -9.81f;

    public bool isSprinting;

    private Camera mainCamera;

    // public Animator animator;


    private void Awake() {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        // animator = GetComponentInChildren<Animator>();
    }

    private void Update() {
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
    }

    private void ApplyGravity() {
        if (IsGrounded() && velocity < 0.0f) {
            velocity = -1.0f;
        } else {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        direction.y = velocity;
    }

    private void ApplyRotation() {
        if (input.sqrMagnitude == 0) return;

        direction = Quaternion.Euler(0.0f, mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(input.x, 0.0f, input.y);
        var targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ApplyMovement() {
        var targetSpeed = isSprinting ? speed * sprintMultiplier : speed;

        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        characterController.Move(currentSpeed * Time.deltaTime * direction);
    }

    public void Move(InputAction.CallbackContext context) {
        input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0.0f, input.y);

        // animator.SetBool("Moving", input != Vector2.zero);
    }

    public void Jump(InputAction.CallbackContext context) {
        if (allowJump && context.started) {
            if (IsGrounded() || numberOfJumps < maxNumberOfJumps) {
                if (numberOfJumps == 0) {
                    StartCoroutine(WaitForLanding());
                }

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