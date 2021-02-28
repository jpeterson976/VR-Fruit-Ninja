using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public HealthBar healthBar;
    public GameObject loseMenu;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetHealth(maxHealth);
    }

    public void Heal(int amt)
    {
        currentHealth += amt;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }

    public void Damage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth < 0)
            currentHealth = 0;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (gameObject.tag.Equals("Fruit"))
        {
            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
            ps.Play();
        }
    }

    public void ShowDamage()
    {
        if (!this.gameObject.GetComponent<Player>().slimed)
            if (!DamageIndicatorSystem.CheckIfObjectInSight(this.transform))
                DamageIndicatorSystem.CreateIndicator(this.transform);
    }

    public bool isDead()
    {
        return currentHealth <= 0;
    }


    void Update()
    {
        if (gameObject.tag.Equals("Player"))
            if (isDead())
            {
                Time.timeScale = 0;
                loseMenu.SetActive(true);
            }
    }
}
