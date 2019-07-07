using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    Transform camTransform;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        camTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Vector3 horizontalDir = camTransform.right * input.x;
        Vector3 verticalDir = camTransform.forward * input.z;

        verticalDir.y = 0;
        
        if (input != Vector3.zero)
            transform.Translate((horizontalDir + verticalDir) * speed * Time.deltaTime);
    }
}