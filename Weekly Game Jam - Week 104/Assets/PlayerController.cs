using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] RectTransform staminaSlider = null;

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
        bool running = Input.GetKey(KeyCode.LeftShift) ? canRun : false;

        speed = running ? currentRunSpeed : walkSpeed;

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Vector3 horizontalDir = camTransform.right * input.x;
        Vector3 verticalDir = camTransform.forward * input.z;

        verticalDir.y = 0;

        if (input != Vector3.zero)
        {
            if (running)
            {
                stamina -= staminaUsagePerSecond * Time.deltaTime;

                UpdateStamina();
            }

            characterController.SimpleMove((horizontalDir + verticalDir) * speed * Time.deltaTime);
        }

        if(stamina < maxStamina)
        {
            if (input == Vector3.zero)
            {
                stamina = Mathf.MoveTowards(stamina, maxStamina, staminaRegenSpeed * Time.deltaTime);
            }
            else if (!running)
            {
                stamina = Mathf.MoveTowards(stamina, maxStamina, staminaRegenSpeedWalking * Time.deltaTime);
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

    private void UpdateStamina()
    {
        Vector3 newStamina = staminaSlider.localScale;
        newStamina.x = stamina / 100f; //I Divede by 100, because to keep scale from 0 to 1
        staminaSlider.localScale = newStamina;
    }
}