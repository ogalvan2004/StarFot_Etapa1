using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private float health = 100;

    private float maxHealth = 100;

    [SerializeField]
    Image healthBar;

    public void GainHealth(float healAmount)
    {
        health += healAmount;
        if(health < 100)
        {
            health = 100;
        }
        healthBar.fillAmount = health / 100;
    }
    
    public void LoseHealth(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            health = 0;
        }
        healthBar.fillAmount = health / 100;
    }

}
