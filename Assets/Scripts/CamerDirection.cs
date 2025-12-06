using UnityEngine;

public class CamerDirection : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Gravity")]
    public Vector3 gravityDirection = Vector3.down;

    [Header("Smoothing")]
    public float rotationSpeed = 5f;

    void LateUpdate()
    {
        if (player == null) return;
        if (gravityDirection.sqrMagnitude < 0.0001f) return;


        Vector3 newUp = -gravityDirection.normalized;


        Vector3 forward = Vector3.ProjectOnPlane(player.forward, newUp);
        if (forward.sqrMagnitude < 0.0001f)
            forward = Vector3.ProjectOnPlane(player.right, newUp);

        Quaternion targetRot = Quaternion.LookRotation(forward, newUp);


        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }

    public void SetGravityDirection(Vector3 dir)
    {
        gravityDirection = dir;
    }
}
