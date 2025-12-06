using UnityEngine;

public class GravityControle : MonoBehaviour
{
    [Header("Script Refrences")]
    public PlayerMovement playerMovement;
    public CamerDirection CamDir;
    public Transform _playerTransfrom;
    public Vector3 _gravityDirection = Vector3.down;
    public float _gravityIntensity;
    public GameObject _hologramPlayer;
    public Transform _referenceObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            _hologramPlayer.SetActive(true);
            _hologramPlayer.transform.localEulerAngles = new Vector3(90, 0, 0);
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SetGravityDirection(-_playerTransfrom.forward);
                CamDir.SetGravityDirection(_gravityDirection);
                _hologramPlayer.SetActive(false);
            }
            return;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            _hologramPlayer.SetActive(true);
            _hologramPlayer.transform.localEulerAngles = new Vector3(-90, 0, 0);
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SetGravityDirection(_playerTransfrom.forward);
                CamDir.SetGravityDirection(_gravityDirection);
                _hologramPlayer.SetActive(false);

            }
            return;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            _hologramPlayer.SetActive(true);
            _hologramPlayer.transform.localEulerAngles = new Vector3(0, 0, 90);
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SetGravityDirection(_playerTransfrom.right);
                CamDir.SetGravityDirection(_gravityDirection);
                _hologramPlayer.SetActive(false);

            }
            return;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            _hologramPlayer.SetActive(true);
            _hologramPlayer.transform.localEulerAngles = new Vector3(0, 0, -90);
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SetGravityDirection(-_playerTransfrom.right);
                CamDir.SetGravityDirection(_gravityDirection);
                _hologramPlayer.SetActive(false);

            }
            return;
        }
        _hologramPlayer.SetActive(false);
        _hologramPlayer.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void SetGravityDirection(Vector3 newDir)
    {
        Vector3 dirLocal = _referenceObject.InverseTransformDirection(newDir);

        Vector3 SnapToAxis(Vector3 v)
        {
            Vector3 abs = new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));

            if (abs.x > abs.y && abs.x > abs.z)
                return new Vector3(Mathf.Sign(v.x), 0, 0);

            if (abs.y > abs.x && abs.y > abs.z)
                return new Vector3(0, Mathf.Sign(v.y), 0);

            return new Vector3(0, 0, Mathf.Sign(v.z));
        }
        Vector3 tempdirLocal = _referenceObject.InverseTransformDirection(newDir);
        Vector3 snappedLocal = SnapToAxis(dirLocal);
        Vector3 gravityDirection = _referenceObject.TransformDirection(snappedLocal);

        _gravityDirection = gravityDirection.normalized;
    }
}
