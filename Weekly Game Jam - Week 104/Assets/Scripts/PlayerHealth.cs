using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IRegenerate
{
    [SerializeField] RectTransform healthSlider = null;

    [SerializeField] float maxHealth = 100f;
    [SerializeField] float regenSpeed = 25f;

    public bool Alive { get; set; } = true;

    float health;

    void Awake()
    {
        health = maxHealth;
    }

    void Update()
    {
        if(health < maxHealth)
        {
            health = Regenerate(health, maxHealth, regenSpeed * Time.deltaTime);

            UpdateHealthUI();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        health = Mathf.Clamp(health, 0, maxHealth);

        UpdateHealthUI();

        if (health <= 0)
            StartCoroutine(Die());
    }

    public float Regenerate(float valueToRegenerate, float maxAmount, float regenSpeed)
    {
        return Mathf.MoveTowards(valueToRegenerate, maxAmount, regenSpeed);
    }

    private void UpdateHealthUI()
    {
        Vector3 newhealth = healthSlider.localScale;
        newhealth.x = health / 100f;

        healthSlider.localScale = newhealth;
    }

    private IEnumerator Die()
    {
        Debug.Log("Uh Dying");

        yield return new WaitForSecondsRealtime(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}