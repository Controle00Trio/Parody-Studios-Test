using UnityEngine;

public class GravityControle : MonoBehaviour
{
    public Vector3 _gravityDirection = Vector3.down;
    public float _gravityIntensity;
    public CamerDirection _camDir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetGravityDirection(Vector3.up);
            _camDir.SetGravityDirection(_gravityDirection);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetGravityDirection(Vector3.down);
            _camDir.SetGravityDirection(_gravityDirection);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetGravityDirection(Vector3.right);
            _camDir.SetGravityDirection(_gravityDirection);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetGravityDirection(Vector3.left);
            _camDir.SetGravityDirection(_gravityDirection);
        }
    }

    public void SetGravityDirection(Vector3 newDir)
    {
        _gravityDirection = newDir.normalized;
    }
}
