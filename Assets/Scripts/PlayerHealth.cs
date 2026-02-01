using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public Slider slider;

    void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageSource"))
        {
            ChangeHealth(-collision.gameObject.GetComponent<EnemyCombat>().damage);
        }
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        slider.value = currentHealth;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
