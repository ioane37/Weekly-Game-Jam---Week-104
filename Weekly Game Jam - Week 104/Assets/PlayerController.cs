using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;

    CharacterController characterController;

    Transform camTransform;

    float speed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        camTransform = Camera.main.transform;
    }

    void Update()
    {
        speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Vector3 horizontalDir = camTransform.right * input.x;
        Vector3 verticalDir = camTransform.forward * input.z;

        verticalDir.y = 0;

        if (input != Vector3.zero)
        {
            //transform.Translate((horizontalDir + verticalDir) * speed * Time.deltaTime);
            characterController.SimpleMove((horizontalDir + verticalDir) * speed * Time.deltaTime);
        }
    }
}