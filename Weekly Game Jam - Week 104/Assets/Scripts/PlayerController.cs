using UnityEngine;

public class PlayerController : MonoBehaviour, IRegenerate
{
    [SerializeField] RectTransform staminaSlider = null;

    [SerializeField] float gravity = -2f;

    [SerializeField] float jumpHeight = 0.05f;

    [SerializeField] float maxStamina = 100f;
    [SerializeField] float staminaUsagePerSecond = 20f;
    [SerializeField] float staminaRegenSpeed = 20f;
    [SerializeField] float staminaRegenSpeedWalking = 8f;

    [SerializeField] float walkSpeed = 300f;
    [SerializeField] float runSpeed = 7000f;
    [SerializeField] float runSpeedLowStamina = 500f;

    CharacterController characterController;

    Transform camTransform;

    float stamina;
    float speed;
    float currentRunSpeed;

    float velocityY;

    bool canRun = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        camTransform = Camera.main.transform;

        currentRunSpeed = runSpeed;

        stamina = maxStamina;
        UpdateStamina();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);

            velocityY = jumpVelocity;
        }

        bool running = Input.GetKey(KeyCode.LeftShift) ? canRun : false;

        speed = running ? currentRunSpeed : walkSpeed;

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Vector3 horizontalDir = camTransform.right * input.x;
        Vector3 verticalDir = camTransform.forward * input.z;

        horizontalDir.y = 0;
        verticalDir.y = 0;

        velocityY += gravity * Time.deltaTime;

        Vector3 findalDir = horizontalDir + verticalDir;

        verticalDir.y = 0;

        if (input != Vector3.zero)
        {
            if (running)
            {
                stamina -= staminaUsagePerSecond * Time.deltaTime;

                UpdateStamina();
            }
        }

        characterController.Move((findalDir * speed * Time.deltaTime) + Vector3.up * velocityY);

        if (characterController.isGrounded)
            velocityY = 0;

        if (stamina < maxStamina)
        {
            if (input == Vector3.zero)
            {
                stamina = Regenerate(stamina, maxStamina, staminaRegenSpeed * Time.deltaTime);
            }
            else if (!running)
            {
                stamina = Regenerate(stamina, maxStamina, staminaRegenSpeedWalking * Time.deltaTime);
            }

            if (stamina >= 30f)
            {
                canRun = true;

                currentRunSpeed = runSpeed;
            }

            UpdateStamina();
        }

        if(stamina <= 30f)
            currentRunSpeed = runSpeedLowStamina;

        if (stamina <= 0)
        {
            canRun = false;
        }
    }

    public float Regenerate(float valueToRegenerate, float maxAmount, float regenSpeed)
    {
        return Mathf.MoveTowards(valueToRegenerate, maxAmount, regenSpeed);
    }

    private void UpdateStamina()
    {
        Vector3 newStamina = staminaSlider.localScale;
        newStamina.x = stamina / 100f; //I Divede by 100, because to keep scale from 0 to 1
        staminaSlider.localScale = newStamina;
    }
}