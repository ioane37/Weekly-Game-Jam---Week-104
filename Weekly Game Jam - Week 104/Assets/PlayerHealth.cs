using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] RectTransform healthSlider = null;

    [SerializeField] int maxHealth = 100;

    public bool Alive { get; set; } = true;

    int health;

    void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        health = Mathf.Clamp(health, 0, maxHealth);

        Vector3 newhealth = healthSlider.localScale;
        newhealth.x = health / 100f;

        healthSlider.localScale = newhealth;

        if (health <= 0)
            StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        Debug.Log("Uh Dying");

        yield return new WaitForSecondsRealtime(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}