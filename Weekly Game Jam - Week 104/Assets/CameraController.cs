using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float sensetivity = 250f;

    float pitch = 0;
    float yaw = 0;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //transform.position = player.position;

        pitch -= Input.GetAxisRaw("Mouse Y") * sensetivity * Time.deltaTime;
        yaw += Input.GetAxisRaw("Mouse X") * sensetivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, -80f, 85f);

        transform.eulerAngles = new Vector3(pitch, yaw);
    }
}