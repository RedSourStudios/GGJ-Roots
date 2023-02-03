using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    [SerializeField] private int totalHealth;
    [SerializeField] private int currentHealth;

    void Start()
    {
        currentHealth = PlayerPrefs.GetInt("currentHealth");
    }

    void Update()
    {
        
    }

    void TakeDamage(int damage) {
        currentHealth -= damage;
        PlayerPrefs.SetInt("currentHealth", currentHealth);
        //som
        //animação
        //invencibilidade
    }
}
