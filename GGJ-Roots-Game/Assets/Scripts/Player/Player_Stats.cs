using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    [SerializeField] private int totalHealth;
    [SerializeField] private int currentHealth;
    private CapsuleCollider2D capsule;
    private GameObject uiController;

    void Awake() {
        PlayerPrefs.SetInt("currentHealth", totalHealth); //temos apenas uma fase então não tem problema
    }

    void Start()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        currentHealth = PlayerPrefs.GetInt("currentHealth");
        uiController = GameObject.FindGameObjectWithTag("UIController");
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        PlayerPrefs.SetInt("currentHealth", currentHealth);
        if(currentHealth > 0) {
            //som
            //animação
            //invencibilidade
        }
        else {
            Death();
        }
    }

    public void Death() {
        //animação
        //som
        capsule.enabled = false;
        Destroy(gameObject, 1f); //tempo da animação
        uiController.GetComponent<UIController>().DeathScreen();
    }
}
