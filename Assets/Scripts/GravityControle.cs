using UnityEngine;

public class GravityControle : MonoBehaviour
{
    public Vector3 _gravityDirection = Vector3.down;
    public float _gravityIntensity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _gravityDirection = Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _gravityDirection = Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _gravityDirection = Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _gravityDirection = Vector3.left;
        }
    }

    public void SetGravityDirection(Vector3 newDir)
    {
        _gravityDirection = newDir.normalized;
    }
}
