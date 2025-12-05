using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Attributes")]
    public Camera playerCamera;
    public AnimationController animationController;
    public GravityControle gravityControle;
    public Transform feetPos;

    [Header("Player Movement Attributes")]
    public float moveSpeed = 8f;
    public float rotationSpeed = 10f;
    public float groundDrag = 5f;

    private Rigidbody rb;
    private Vector3 moveDir;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        GroundCheck();
        InputHandling();
        RotateWithGravity();
        RotateTowardsMovement();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        ApplyGravity();
    }



    void InputHandling()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 gravityUp = -gravityControle._gravityDirection;

        Vector3 camForward = Vector3.ProjectOnPlane(playerCamera.transform.forward, gravityUp).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(playerCamera.transform.right, gravityUp).normalized;

        moveDir = (camForward * v + camRight * h).normalized;

        float moveVal = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        animationController.SetAnimatorValue(moveVal);
    }

    void ApplyGravity()
    {
        rb.AddForce(gravityControle._gravityDirection * gravityControle._gravityIntensity, ForceMode.Acceleration);
    }

    void ApplyMovement()
    {
        Vector3 moveForce = moveDir * moveSpeed;

        rb.AddForce(moveForce, ForceMode.Acceleration);

        if (isGrounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0f;
    }

    void RotateWithGravity()
    {
        Vector3 gravityUp = -gravityControle._gravityDirection;

        Quaternion targetRot = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }

    void RotateTowardsMovement()
    {
        if (moveDir.sqrMagnitude < 0.01f) return;

        Quaternion lookRot = Quaternion.LookRotation(moveDir, -gravityControle._gravityDirection);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRot,
            rotationSpeed * Time.deltaTime
        );
    }


    void GroundCheck()
    {
        isGrounded = Physics.Raycast(feetPos.transform.position, gravityControle._gravityDirection, 1.1f);
    }
}
