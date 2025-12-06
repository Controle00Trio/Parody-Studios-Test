using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Script Refrences")]
    public AnimationController animationController;
    public GravityControle gravityControle;
    public Camera playerCamera;
    public Transform feetPos;
    public float moveSpeed = 8f;
    public float rotationSpeed = 10f;
    public float gravityrotationSpeed = 10f;
    public float groundDrag = 5f;
    public float jumpforce = 5f;

    private Rigidbody rb;
    private Vector3 moveDir;
    private bool isGrounded;
    private bool camJump = true;

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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && camJump)
            StartCoroutine("Jump");
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

        Vector3 camForward = playerCamera.transform.forward;
        camForward = Vector3.ProjectOnPlane(camForward, -gravityControle._gravityDirection).normalized;

        Vector3 camRight = playerCamera.transform.right;
        camRight = Vector3.ProjectOnPlane(camRight, -gravityControle._gravityDirection).normalized;


        moveDir = (camForward * v + camRight * h).normalized;

        float moveVal = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        animationController.SetAnimatorValue(moveVal);
    }

    void ApplyGravity()
    {
        if (camJump)
            rb.AddForce(gravityControle._gravityDirection * gravityControle._gravityIntensity, ForceMode.Acceleration);
        else
            rb.AddForce(gravityControle._gravityDirection * gravityControle._gravityIntensity * 2.5f, ForceMode.Acceleration);

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

        Quaternion targetRot =
            Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            gravityrotationSpeed * Time.deltaTime
        );
    }


    void RotateTowardsMovement()
    {
        if (moveDir.sqrMagnitude < 0.1f) return;

        Vector3 gravityUp = -gravityControle._gravityDirection;

        Vector3 flatDir = Vector3.ProjectOnPlane(moveDir, gravityUp).normalized;

        Quaternion targetRot = Quaternion.LookRotation(flatDir, gravityUp);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }

    IEnumerator Jump()
    {
        animationController.SetJumpingAnimation();
        yield return new WaitForSeconds(0.5f);
        camJump = false;
        rb.linearVelocity = Vector3.ProjectOnPlane(rb.linearVelocity, -gravityControle._gravityDirection);

        Vector3 dir = -gravityControle._gravityDirection;
        rb.AddForce(dir * jumpforce, ForceMode.VelocityChange);
        yield return new WaitForSeconds(1.5f);
        ResetJump();
    }
    void ResetJump()
    {
        camJump = true;
    }
    void GroundCheck()
    {
        isGrounded = Physics.Raycast(feetPos.transform.position, gravityControle._gravityDirection, 1.1f);
    }
}
